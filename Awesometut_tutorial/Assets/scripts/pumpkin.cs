using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pumpkin : MonoBehaviour {
public float moveForce = 10f;
private Rigidbody _rb;
	// Use this for initialization
	void Awake () {
		_rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	MovePumkin();
	}

	void MovePumkin()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		_rb.AddForce(new Vector3(h * moveForce, 0f, v * moveForce));
	}

	void OnTriggerEnter(Collider target)
	{
		if(target.tag == "Golem")
		{
			Time.timeScale = 0f;
		}
	}
}
