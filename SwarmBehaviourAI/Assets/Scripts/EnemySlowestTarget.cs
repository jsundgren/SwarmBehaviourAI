using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlowestTarget : Enemy
{
    readonly float enemyRadius = 20;
    Vector3 wanderTarget;

    public void Update()
    {
        followTarget(findSlowestTarget(targets));
    }

   Transform findSlowestTarget(List<Member> L)
    {
        Transform slowestTarget = null;
        List<Member> n = l.findNeighbours(this, enemyRadius);

        float slowestTargetVelocity = Mathf.Infinity;
        float targetVelocity = 0;

        Member target = null;

        if(n.Count < 1) {
          return this.transform;
        }

        foreach (Member m in n)
        {
            targetVelocity = m.vel.sqrMagnitude;
            if(targetVelocity < slowestTargetVelocity) {
                slowestTargetVelocity = targetVelocity;
                slowestTarget = m.transform;
                target = m;
            }
        }
        return slowestTarget;
    }
}
