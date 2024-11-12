using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;

    void Awake() 
    {
        this.player = GetComponentInParent<Player>();
    } // Awake

    void Start() 
    {
        this.Init();
    } // Start

    void Update()
    {
        switch (id) {
            case 0:
                this.transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if (this.timer > this.speed) {
                    this.timer = 0f;
                    Fire();
                } // if

                break;
        } // switch case

        // .. Test Code
        if (Input.GetButtonDown("Jump")) LevelUp(10f, 1);

    } // Update

    public void LevelUp(float damage, int count) 
    {
        this.damage = damage;
        this.count += count;

        if (this.id == 0) Batch(); // if

    } // LevelUp

    public void Init() 
    {
        switch (id) {
            case 0:
                this.speed = 150;
                this.Batch();
                break;
            default:
                this.speed = 0.3f;

                break;
        } // switch case

    } // Init

    void Batch() 
    {
        for (int i=0; i<count; i++) {
            // 부모 변경
            Transform bullet;

            if (i<transform.childCount) {
                bullet = transform.GetChild(i);
            }
            else {
                bullet = GameManager.instance.pool.Get(this.prefabId).transform;
                bullet.parent = this.transform;
            } // if else
            

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / this.count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(this.damage, -1, Vector3.zero); // -1 is Infinity Per.
        } // for

    } // Batch

    void Fire() 
    {
        // .. 가장 가까운 Enemy 가 존재 하지 않으면? return;
        if (!this.player.scanner.nearestTarget) {
            return;
        } // if

        Vector3 targetPos = this.player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(this.prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(this.damage, this.count, dir);
    } // Fire

} // end class
