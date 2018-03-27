using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerType
{
	player1, player2
}

public class Gamemanager : MonoBehaviour {
public static Gamemanager gamemanager;
public GameTypes.PlayerType _player;
	// Use this for initialization
	void Awake () {
		gamemanager = this;
		//pitää vielä testa toimiiko oikeasti
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
