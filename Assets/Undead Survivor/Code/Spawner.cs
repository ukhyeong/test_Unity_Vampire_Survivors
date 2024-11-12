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
        // Mathf.FloorToInt : �Ҽ��� �Ʒ��� ������ Int �� �Լ��� ��ȯ
        // Mathf.CeilToInt : �Ҽ��� �Ʒ��� �ø��� Int �� �Լ��� ��ȯ
        // Mathf.Min : ù�Ű������� ���� �ι�° �Ű������� ���� �ʰ����� ����
        this.level = Mathf.Min( Mathf.FloorToInt(GameManager.instance.gameTime / 10f), this.spawnData.Length - 1);

        if (this.timer > this.spawnData[level].spawnTime) {
            this.timer = 0;
            this.Spawn();
        } // if

    } // Update

    void Spawn() 
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        // �� 1 ���� �ΰ�?
        // GetComponentsInChildren �Լ��� �ڱ� �ڽ��� 0��°�� �迭�� ��´�. �츮�� �ڽ� ������Ʈ�� Point ������ ���� �����Ǳ⸦ ���ϹǷ�
        // 0��°�� Spawner ��ü�� �����ؾ��ϱ� ������ 1���� ���������� �����Ѵ�.
        enemy.transform.position = this.spawnPoint[Random.Range(1, this.spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(this.spawnData[this.level]);
    } // Spawn

} // end class

// inspecter â���� ���̵��� �ϱ����� ����ȭ
[System.Serializable]
public class SpawnData
{
    public float spawnTime;

    public int spriteType;
    public int health;
    public float speed;
} // end class
