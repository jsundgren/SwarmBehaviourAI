using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNearestTarget : Enemy
{

    public void Update(){
        clearTargets();
        followTarget(findNearestTarget(targets));
    }

    Transform findNearestTarget(List<Member> L)
    {
        Transform nearestTarget = null;

        float closestDistSqr = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (Member m in L)
        {
            Vector3 directionToTarget = m.transform.position - currentPos;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistSqr)
            {
                closestDistSqr = dSqrToTarget;
                nearestTarget = m.transform;
            }
        }

        return nearestTarget;
    }
}
