using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake() {
        coll = GetComponent<Collider2D>();
    } // Awake

    void OnTriggerExit2D(Collider2D collision) 
    {
        if (!collision.CompareTag("Area")) return; // if

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = this.transform.position;

        // Mathf.Abs : 절대값 함수
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.inputVec;

        float dirX = playerDir.x >= 0 ? 1 : -1;
        float dirY = playerDir.y >= 0 ? 1 : -1;

        switch (this.transform.tag) {
            case "Ground":
                if (diffX > diffY) {
                    this.transform.Translate(Vector3.right * dirX * 40);
                } 
                else if (diffY > diffX) {
                    this.transform.Translate(Vector3.up * dirY * 40);
                } 
                else if (diffY == diffX) {
                    this.transform.Translate(dirX * 40, dirY * 40, 0);
                } // if else 

                break;
            case "Enemy":
                if (this.coll.enabled) {
                    this.transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f)));
                } // if

                break;
        } // switch case

    } // OnTriggerExit2D

} // end class
