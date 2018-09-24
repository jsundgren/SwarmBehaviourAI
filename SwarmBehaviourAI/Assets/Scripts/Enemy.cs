using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


	public List<Member> targets;
	Transform myTrans;
	public Vector3 pos;

	public void Start(){
		pos = transform.position;
		targets = new List<Member> ();
		targets.AddRange (FindObjectsOfType<Member> ());
	}

	public void Update(){
		followNearestTarget (findNearestTarget (targets));
	}

	Transform findNearestTarget(List<Member> L){
		Transform nearestTarget = null;

		float closestDistSqr = Mathf.Infinity;
		Vector3 currentPos = transform.position;

		foreach (Member m in L) {
			Vector3 directionToTarget = m.transform.position - currentPos;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if (dSqrToTarget < closestDistSqr) {
				closestDistSqr = dSqrToTarget;
				nearestTarget = m.transform;
			}
		}

		return nearestTarget;
	}

	void followNearestTarget(Transform t){
		transform.LookAt (t.transform.position);
		transform.Translate (Vector3.forward * 5 * Time.deltaTime);
	}
}
