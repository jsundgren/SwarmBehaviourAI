﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour {

	public Transform memberPrefab;
	public Transform enemyNearestTargetPrefab;
	public Transform enemySlowestTargetPrefab;
	public int numberOfMembers;
	public int numberOfEnemiesNearestTarget;
	public int numberOfEnemiesSlowestTarget;
	public List<Member> members;
	public List<Enemy> enemies;
	public List<Text> foodTexts;
	public float spawnRadius;
	public float bounds;
	public Text numberOfSheepLeft;
	Canvas countCanvas;

	// Use this for initialization
	public void StartGame () {
		members = new List<Member> ();
		enemies = new List<Enemy> ();
		foodTexts = new List <Text> ();

		Spawn (memberPrefab, numberOfMembers);
		Spawn(enemyNearestTargetPrefab, numberOfEnemiesNearestTarget);
		Spawn(enemySlowestTargetPrefab, numberOfEnemiesSlowestTarget);

		members.AddRange (FindObjectsOfType<Member> ());
		enemies.AddRange (FindObjectsOfType<Enemy> ());

		GameObject countObj = GameObject.Find("CountCanvas");
		countCanvas = countObj.GetComponent<Canvas>();
		Vector3 textPosition = new Vector3 (105,0,0);

		foreach(Enemy e in enemies){
			textPosition.y += 20;
			Text newText = Instantiate(numberOfSheepLeft, textPosition, Quaternion.identity, countCanvas.transform);
			newText.text = e.foodText() + e.countFood.ToString();
			foodTexts.Add(newText);
		}
	}

	void Update(){
		members.Clear ();
		members.AddRange (FindObjectsOfType<Member> ());
	}

	void Spawn(Transform prefab, int count) {
		for (int i = 0; i < count; i++) {
				Instantiate (prefab, new Vector3 (Random.Range (-spawnRadius, spawnRadius), 0f, Random.Range (-spawnRadius, spawnRadius)),
					Quaternion.identity);
		}
	}

    public List<Member> findNeighbours(AIObject o, float radius) {

		List<Member> neighbourFound = new List<Member>();

		foreach (Member otherMember in members) {
			if (otherMember == o) {
				continue;
			}

			if(Vector3.Distance(o.transform.position, otherMember.transform.position) <= radius){
				neighbourFound.Add (otherMember);
			}
		}
		return neighbourFound;
	}

    public List<Enemy> findEnemies(AIObject o, float radius){
		List<Enemy> getEnemies = new List<Enemy> ();

		foreach (Enemy e in enemies) {
			if (Vector3.Distance (o.pos, e.pos) <= radius) {
				getEnemies.Add (e);
			}
		}

		return getEnemies;
	}

	public void updateFoodText(){
		for(int i = 0; i<enemies.Count; i++){
			foodTexts[i].text = enemies[i].foodText() + enemies[i].countFood.ToString();
		}
	}
}
