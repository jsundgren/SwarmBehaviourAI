using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartGame : MonoBehaviour {

	public GameObject menuUI;
	public static bool GameMenu = true;
	private Level l;
	public InputField nrOfSlow;
	public InputField nrOfNear;
	public InputField nrOfSheep;

	void Start(){
		l = FindObjectOfType<Level>();
		l.numberOfMembers = 100;
		l.numberOfEnemiesNearestTarget = 1;
		l.numberOfEnemiesSlowestTarget = 1;
		Menu();
	}

	void Update() {
	}

	void Menu(){
		menuUI.SetActive(true);
		Time.timeScale = 0f;
		GameMenu = true;

	}

	public void StartingGame(){
		l.numberOfMembers = nrOfSheep.text == "" ? 100 : int.Parse(nrOfSheep.text) ;
		l.numberOfEnemiesNearestTarget = nrOfNear.text == "" ? 1 : int.Parse(nrOfNear.text);
		l.numberOfEnemiesSlowestTarget = nrOfSlow.text == "" ? 1 : int.Parse(nrOfSlow.text);
		menuUI.SetActive(false);
		Time.timeScale = 1f;
		GameMenu = false;
		l.StartGame();
	}
}
