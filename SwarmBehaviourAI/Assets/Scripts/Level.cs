using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour {

	public Transform memberPrefab;
	public Transform enemyPrefab;
	public int numberOfMembers;
	public int numberOfEnemies;
	public List<Member> members;
	public List<Enemy> enemies;
	public float spawnRadius;
	public float bounds;
	public Text numberOfSheepLeft;

	// Use this for initialization
	void Start () {
		members = new List<Member> ();
		enemies = new List<Enemy> ();

		Spawn (memberPrefab, numberOfMembers);
		Spawn (enemyPrefab, numberOfEnemies);

		members.AddRange (FindObjectsOfType<Member> ());
		enemies.AddRange (FindObjectsOfType<Enemy> ());
	}

	void Update(){
		members.Clear ();
		members.AddRange (FindObjectsOfType<Member> ());
		numberOfSheepLeft.text = "Number of sheep left: " + (members.Count).ToString();
	}

	void Spawn(Transform prefab, int count) {
		for (int i = 0; i < count; i++) {
			Instantiate(prefab, new Vector3(Random.Range(-spawnRadius, spawnRadius), 0f, Random.Range(-spawnRadius, spawnRadius)),
				Quaternion.identity);
		}
	}

	public List<Member> findNeighbours(Member m, float radius) {

		List<Member> neighbourFound = new List<Member>();

		foreach (Member otherMember in members) {
			if (otherMember == m) {
				continue;
			}

			if(Vector3.Distance(m.transform.position, otherMember.transform.position) <= radius){
				neighbourFound.Add (otherMember);
			}
		}
		return neighbourFound;
	}
		
	public List<Enemy> findEnemies(Member m, float radius){
		List<Enemy> getEnemies = new List<Enemy> ();

		foreach (Enemy e in enemies) {
			if (Vector3.Distance (m.pos, e.pos) <= radius) {
				getEnemies.Add (e);
			}
		}

		return getEnemies;
	}
}
