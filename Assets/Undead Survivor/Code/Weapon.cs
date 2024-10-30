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

                break;
        } // switch case

        // .. Test Code
        if (Input.GetButtonDown("Jump")) LevelUp(20f, 5);

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
            bullet.GetComponent<Bullet>().Init(this.damage, -1); // -1 is Infinity Per.

        } // for

    } // Batch

} // end class
