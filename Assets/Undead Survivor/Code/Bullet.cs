using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigid;

    void Awake() 
    {
        this.rigid = GetComponent<Rigidbody2D>();
    } // Awake

    public void Init(float damage, int per, Vector3 dir) 
    {
        this.damage = damage;
        this.per = per;

        if (per > -1) {
            this.rigid.velocity = dir * 15f;
        } // if

    } // Init

    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (!collision.CompareTag("Enemy") || per == -1) {
            return;
        } // if

        per--;

        if (per == -1) {
            this.rigid.velocity = Vector2.zero;
            this.gameObject.SetActive(false);
        } // if

    } // OnTriggerEnter2D

} // end class
