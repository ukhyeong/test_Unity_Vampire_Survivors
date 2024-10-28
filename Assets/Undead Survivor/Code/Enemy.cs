using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public RuntimeAnimatorController[] animCon;
    public float health;
    public float maxHealth;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    } // Awake

    void FixedUpdate()
    {
        if (!isLive) return;

        Vector2 dirVec = this.target.position - this.rigid.position;
        Vector2 nextVec = dirVec.normalized * this.speed * Time.fixedDeltaTime;

        this.rigid.MovePosition(this.rigid.position + nextVec);
        this.rigid.velocity = Vector2.zero;
    } // FixedUpdate

    void LateUpdate() 
    {
        if (!isLive) return;

        spriter.flipX = this.target.position.x < this.rigid.position.x;
    } // LateUpdate

    // Enemy ������Ʈ�� Ȱ��ȭ �ɶ�, ���� ����(��Ȱ��ȭ) ���¿��� ����(Ȱ��ȭ) �ɶ�
    void OnEnable() {
        this.target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    } // OnEnable

    // Enemy ������Ʈ�� ���� ���� �ɶ�
    public void Init(SpawnData data) 
    {
        this.anim.runtimeAnimatorController = animCon[data.spriteType];
        this.speed = data.speed;
        this.maxHealth = data.health;
        this.health = data.health;
    } // Init

} // end class
