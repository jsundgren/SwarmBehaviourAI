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

	void OnTriggerStay(Collider col){
		if (col.gameObject.name == "Running_Sheep(Clone)") {
			// if needed
		}
	}



	void WrapAround(ref Vector3 vec, float min, float max) {
		vec.x = WrapAroundFloat (vec.x, min, max);
		vec.y = WrapAroundFloat (vec.y, min, max);
		vec.z = WrapAroundFloat (vec.z, min, max);
	}

	float WrapAroundFloat(float value, float min, float max) {
		if (value > max)
			value = max;
		else if (value < min)
			value = min;

		return value;
	}

	protected void clearTargets() {
		targets.Clear();
		targets.AddRange (FindObjectsOfType<Member> ());
	}

	protected void followTarget(Transform t)
	{
		transform.LookAt(t.transform.position);
		WrapAround (ref pos, -l.bounds, l.bounds);
		transform.position = new Vector3 (transform.position.x, 0.75f, transform.position.z);
		transform.Translate(Vector3.forward * 10 * Time.deltaTime);
	}
}
