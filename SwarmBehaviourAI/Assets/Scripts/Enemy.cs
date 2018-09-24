using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Member {


	public List<Member> targets;
	Member currentTarget;
	Transform myTrans;
	int index;
	/*protected override Vector3 Combined ()
	{
		return conf.wanderPriority * Wander() + conf.cohesionPriority * Cohesion();
	}*/

	public void Start(){
		targets = new List<Member> ();
		targets.AddRange (FindObjectsOfType<Member> ());
		index = Random.Range (0, targets.Count);
		currentTarget = targets [index];
	}

	public void Update(){
		transform.LookAt (currentTarget.transform.position);
		transform.Translate (Vector3.forward * 5 * Time.deltaTime);
	}
}
