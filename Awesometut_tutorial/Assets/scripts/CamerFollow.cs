using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour {
private Transform _pumpkinPos;
private float zDistance = 7f;
private float yDistance = 4f;

	// Use this for initialization
	void Awake () {
		_pumpkinPos = GameObject.Find("pumpkin").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = transform.position;
		temp.y = _pumpkinPos.position.y + yDistance;
		temp.z = _pumpkinPos.position.z - zDistance;
		transform.position = temp;
	}
}
