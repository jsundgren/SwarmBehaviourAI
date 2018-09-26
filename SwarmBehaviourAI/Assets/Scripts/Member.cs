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
		acc = Combined ();
		acc = Vector3.ClampMagnitude (acc, conf.maxAcceleration);
		vel = vel + acc * Time.deltaTime;
		vel = Vector3.ClampMagnitude (vel, conf.maxVelocity);
		pos = pos + vel * Time.deltaTime;
		WrapAround (ref pos, -l.bounds, l.bounds);
		transform.LookAt (pos);
		transform.position = pos;

	}

	protected Vector3 Wander() {
		float jitter = conf.wanderJitter * Time.deltaTime;
		wanderTarget += new Vector3 (RandomBinomial () * jitter, 0, RandomBinomial () * jitter);
		wanderTarget = wanderTarget.normalized;
		wanderTarget *= conf.wanderRadius;
		Vector3 targetInLocalSpace = wanderTarget + new Vector3 (conf.wanderDistance, 0, conf.wanderDistance);
		Vector3 targetInWorldSpace = transform.TransformPoint (targetInLocalSpace);
		targetInWorldSpace -= this.pos;
		return targetInWorldSpace.normalized;
	}

	void WrapAround(ref Vector3 vec, float min, float max) {
		vec.x = WrapAroundFloat (vec.x, min, max);
		vec.y = WrapAroundFloat (vec.y, min, max);
		vec.z = WrapAroundFloat (vec.z, min, max);
	}

	float WrapAroundFloat(float value, float min, float max) {
		if (value > max)
			value = min;
		else if (value < min)
			value = max;

		return value;
	}

	protected Vector3 Cohesion() {
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

	virtual protected Vector3 Combined(){
		return conf.cohesionPriority * Cohesion () + conf.wanderPriority * Wander () 
			+ conf.alignmentPriority * Alignment() + conf.separationPriority * Separation()
			+ conf.avoidancePriority * Avoidance();
	}

	Vector3 Separation(){
		Vector3 sepVec = new Vector3 ();
		var members = l.findNeighbours (this, conf.separationRadius);
		if (members.Count == 0)
			return sepVec;

		foreach(Member m in members){
			Vector3 mTowards = this.pos - m.pos;
			if(mTowards.magnitude > 0){
				sepVec += (mTowards.normalized / mTowards.magnitude);
			}
		}

		return sepVec;
	}

	Vector3 Alignment() {
		Vector3 alignVec = new Vector3 ();
		var members = l.findNeighbours (this, conf.alignmentRadius);

		if (members.Count == 0) return alignVec;

		foreach(Member m in members){
			alignVec += m.vel;
		}

		return alignVec.normalized;
	}

	Vector3 Avoidance() { 
		Vector3 avoidVec = new Vector3 ();
		var eList = l.findEnemies (this, conf.avoidanceRadius);

		if (eList.Count == 0) return avoidVec;

		foreach (Enemy e in eList) {
			avoidVec += avoidTarget (e.pos);
		}

		return avoidVec.normalized;
	}

	Vector3 avoidTarget(Vector3 target){
		Vector3 neededVel = (pos - target).normalized * conf.maxVelocity;
		return neededVel - vel;
	}

	float RandomBinomial() {
		return Random.Range(0f, 1f) - Random.Range(0f, 1f);
	}
}
