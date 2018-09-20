using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Member : MonoBehaviour {

	public Vector3 pos;
	public Vector3 vel;
	public Vector3 acc;

	public Level l;
	public MemberConfig conf;

	Vector3 wanderTarget;

	// Use this for initialization
	void Start () {
		l = FindObjectOfType<Level> ();
		conf = FindObjectOfType<MemberConfig> ();
		pos = transform.position;
		vel = new Vector3(Random.Range(-3,3), 0, Random.Range(-3,3));
	}
	
	// Update is called once per frame
	void Update () {
		acc = Cohesion ();
		acc = Vector3.ClampMagnitude (acc, conf.maxAcceleration);
		vel = vel + acc * Time.deltaTime;
		vel = Vector3.ClampMagnitude (vel, conf.maxVelocity);
		pos = pos + vel * Time.deltaTime;
		transform.position = pos;
	}

	protected Vector3 Wander() {
		float jitter = conf.wanderJitter * Time.deltaTime;
		wanderTarget += new Vector3 (RandomBinomial () * jitter, 0, RandomBinomial () * jitter);
		wanderTarget = wanderTarget.normalized;
		wanderTarget *= conf.wanderRadius;
		Vector3 targetInLocalSpace = wanderTarget + new Vector3 (0, 0, conf.wanderDistance);
		Vector3 targetInWorldSpace = transform.TransformPoint (targetInLocalSpace);
		targetInWorldSpace -= this.pos;
		return targetInWorldSpace.normalized;
	}

	Vector3 Cohesion() {

		Vector3 cohesionVector = new Vector3 ();
		int CountMembers = 0;
		List<Member> n = l.findNeighbours (this, conf.cohesionRadius);
		if (n.Count == 0) {
			return cohesionVector;
		}

		foreach (Member m in n) {
			cohesionVector += m.pos;
			CountMembers++;
		}

		if (CountMembers == 0)
			return cohesionVector;

		cohesionVector /= CountMembers;
		cohesionVector = cohesionVector - this.pos;
		cohesionVector = Vector3.Normalize (cohesionVector);
		return cohesionVector;
	}

	float RandomBinomial() {
		return Random.Range(0f, 1f) - Random.Range(0f, 1f);
	}
}
