using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // .. 프리팹들을 보관할 변수
    public GameObject[] prefabs;

    // .. 풀 담당을 하는 리스트들
    List<GameObject>[] pools;

    void Awake() 
    {
        this.pools = new List<GameObject>[prefabs.Length];

        for (int i=0; i<prefabs.Length; i++) {
            this.pools[i] = new List<GameObject>();
        } // for

    } // Awake

    public GameObject Get(int index) 
    {
        GameObject select = null;

        // ... 선택한 풀의 놀고 (비활성화 된) 있는 게임오브젝트 접근. 
        foreach (GameObject item in pools[index]) {
            // item 이 비활성화(대기상태) 인지 확인
            if (!item.activeSelf) {
                // ..발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);

                break;
            } // if

        } // foreach

        // .. 못 찾으면? 
        if (!select) {
            // .. 새롭게 생성하고 select 변수에 할당 // 여기서 'transform' 의 의미: 부모 오브젝트에 자식으로 넣겠다
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        } // if

        return select;
    } // Get

} // end class
