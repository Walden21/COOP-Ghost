using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame(){
		SceneManager.LoadScene ("OpeningScene");
	}

	public void StartHouseScene(){
		SceneManager.LoadScene ("scene_house");
	}

	public void ExitGame(){
		Application.Quit ();
	}
}
