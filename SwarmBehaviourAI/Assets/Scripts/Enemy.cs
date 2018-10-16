using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AIObject {

	public List<Member> targets;

	public void Start(){
		pos = transform.position;
		targets = new List<Member> ();
		targets.AddRange (FindObjectsOfType<Member> ());

    l = FindObjectOfType<Level>();
		conf = FindObjectOfType<MemberConfig> ();
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "SheepWhite (1)(Clone)") {
			Destroy (col.gameObject);
		}
	}

	protected void clearTargets() {
		targets.Clear();
		targets.AddRange (FindObjectsOfType<Member> ());
	}

	protected void followTarget(Transform t)
	{
			transform.LookAt(t.transform.position);
			transform.Translate(Vector3.forward * 10 * Time.deltaTime);
	}
}
