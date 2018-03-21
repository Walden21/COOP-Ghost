using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public int playNum;

	// Use this for initialization
	void Start () {
		playNum = -1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getNumber(){
		playNum++;
		if (playNum > 2)
			playNum = 1;
		return playNum;
	}
}
