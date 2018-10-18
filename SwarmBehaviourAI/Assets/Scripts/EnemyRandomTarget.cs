using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomTarget : Enemy {

	Member randomTarget;
	public Level l;
	public MemberConfig conf;

	// Use this for initialization
	public void Start () {
		targets = new List<Member> ();
		targets.AddRange (FindObjectsOfType<Member> ());
		randomTarget = getRandomTarget();
	}

	// Update is called once per frame
	public void Update () {
		followTarget(findRandomTarget());
	}

	Member getRandomTarget() {
		int random = Random.Range(0, targets.Count);
		return targets[random];
	}

	Transform findRandomTarget() {
		return randomTarget.transform;
	}
	
	public override string setSort() {
		return "Wolf (random)";
	}
}
