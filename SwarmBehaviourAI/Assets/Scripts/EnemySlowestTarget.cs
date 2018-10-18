using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlowestTarget : Enemy
{
    public float enemyRadius = 20;

    public void Update()
    {
      clearTargets();
      if(targets.Count > 0) {
        followTarget(findSlowestTarget(targets));
      }
    }

    public override string setSort(){
      return "Wolf (slowest)";
    }

   Transform findSlowestTarget(List<Member> L)
    {
        Transform slowestTarget = null;
        List<Member> n = l.findNeighbours(this, enemyRadius);

        float slowestTargetVelocity = Mathf.Infinity;
        float targetVelocity = 0;

		if (n.Count < 1) {
			enemyRadius = 1000;
			n = l.findNeighbours(this, enemyRadius);
		} else {
			enemyRadius = 20;
		}

        foreach (Member m in n)
        {
            targetVelocity = m.vel.sqrMagnitude;
            if(targetVelocity < slowestTargetVelocity) {
                slowestTargetVelocity = targetVelocity;
                slowestTarget = m.transform;
            }
        }
        return slowestTarget;
    }
}
