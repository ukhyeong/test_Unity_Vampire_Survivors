using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level;
    float timer;

    void Awake() 
    {
        this.spawnPoint = GetComponentsInChildren<Transform>();
    } // Awake

    void Update() 
    {
        this.timer += Time.deltaTime;
        // Mathf.FloorToInt : 소수점 아래는 버리고 Int 형 함수로 변환
        // Mathf.CeilToInt : 소수점 아래를 올리고 Int 형 함수로 변환
        // Mathf.Min : 첫매개변수의 값이 두번째 매개변수의 값을 초과하지 못함
        this.level = Mathf.Min( Mathf.FloorToInt(GameManager.instance.gameTime / 10f), this.spawnData.Length - 1);

        if (this.timer > this.spawnData[level].spawnTime) {
            this.timer = 0;
            this.Spawn();
        } // if

    } // Update

    void Spawn() 
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        // 왜 1 부터 인가?
        // GetComponentsInChildren 함수는 자기 자신을 0번째로 배열에 담는다. 우리는 자식 오브젝트인 Point 에서만 몹이 스폰되기를 원하므로
        // 0번째인 Spawner 객체는 제외해야하기 때문에 1부터 무작위값을 지정한다.
        enemy.transform.position = this.spawnPoint[Random.Range(1, this.spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(this.spawnData[this.level]);
    } // Spawn

} // end class

// inspecter 창에서 보이도록 하기위한 직렬화
[System.Serializable]
public class SpawnData
{
    public float spawnTime;

    public int spriteType;
    public int health;
    public float speed;
} // end class
