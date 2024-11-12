using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        this.rigid = GetComponent<Rigidbody2D>();
        this.spriter = GetComponent<SpriteRenderer>();
        this.anim = GetComponent<Animator>();
        this.scanner = GetComponent<Scanner>();
    } // Awake

    // Input System 으로 대체됨
    void Update() {
        this.inputVec.x = Input.GetAxisRaw("Horizontal");
        this.inputVec.y = Input.GetAxisRaw("Vertical");
    } // Update

    private void FixedUpdate() 
    {
        // 다른 프레임 환경에서도 이동거리가 같아야함
        Vector2 nextVec = this.inputVec.normalized * this.speed * Time.fixedDeltaTime;

        // 1. 힘을 주기
        //rigid.AddForce(inputVec);

        // 2. 속도 제어
        //rigid.velocity = inputVec;

        // 3. 위치 이동
        this.rigid.MovePosition(this.rigid.position + nextVec);
    } // FixedUpdate

    // Input System 으로 대체시 필요
    //void OnMove(InputValue value)
    //{
    //    this.inputVec = value.Get<Vector2>();
    //} // OnMove

    void LateUpdate()
    {
        this.anim.SetFloat("Speed", this.inputVec.magnitude);

        if(this.inputVec.x != 0) {
            this.spriter.flipX = this.inputVec.x < 0;
        } // if

    } // LateUpdate
    
} // end class
