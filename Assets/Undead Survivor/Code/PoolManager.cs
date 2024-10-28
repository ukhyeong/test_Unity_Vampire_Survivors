using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // .. �����յ��� ������ ����
    public GameObject[] prefabs;

    // .. Ǯ ����� �ϴ� ����Ʈ��
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

        // ... ������ Ǯ�� ��� (��Ȱ��ȭ ��) �ִ� ���ӿ�����Ʈ ����. 
        foreach (GameObject item in pools[index]) {
            // item �� ��Ȱ��ȭ(������) ���� Ȯ��
            if (!item.activeSelf) {
                // ..�߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);

                break;
            } // if

        } // foreach

        // .. �� ã����? 
        if (!select) {
            // .. ���Ӱ� �����ϰ� select ������ �Ҵ� // ���⼭ 'transform' �� �ǹ�: �θ� ������Ʈ�� �ڽ����� �ְڴ�
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        } // if

        return select;
    } // Get

} // end class
