using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    void FixedUpdate() 
    {
        this.targets = Physics2D.CircleCastAll(this.transform.position, this.scanRange, Vector2.zero, 0, this.targetLayer);
    } // FixedUpdate

    Transform GetNearest() 
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in targets) {
            Vector3 myPos = this.transform.position;
            Vector3 targetPos = target.transform.position;

            // Vetor3 ������ �Ÿ��� ����
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff) {

            } // if

        } // foreach, inhanced for �� ����

        return result;
    } // GetNearest


} // end class
