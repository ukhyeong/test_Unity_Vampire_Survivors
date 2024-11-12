using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Enemy : MonoBehaviour
{
    public float speed;
    public RuntimeAnimatorController[] animCon;
    public float health;
    public float maxHealth;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    } // Awake

    void FixedUpdate()
    {
        // Enemy �� ��Ȱ��ȭ �����϶� �Ǵ� Hit �����϶� return;
        if (!isLive || this.anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

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
        this.isLive = true;
        this.coll.enabled = true;
        this.rigid.simulated = true;
        this.spriter.sortingOrder = 2;
        this.anim.SetBool("Dead", false);
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

    void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Bullet") || !this.isLive) return; // if

        this.health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(this.KnockBack());

        if (health > 0) {
            this.anim.SetTrigger("Hit");
        }
        else {
            this.isLive = false;
            this.coll.enabled = false;
            this.rigid.simulated = false;
            this.spriter.sortingOrder = 1;
            this.anim.SetBool("Dead", true);

            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        } // if else

    } // OnTriggerEnter2D

    IEnumerator KnockBack() 
    {
        yield return this.wait; // ���� �ϳ��� ���� �������� ������
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        this.rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    } // KnockBack

    void Dead() 
    {
        this.gameObject.SetActive(false);
    } // Dead

} // end class
