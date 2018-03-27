using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PoolManager : MonoBehaviour {
public static PoolManager current;
public GameObject bulletprefab;
private List<GameObject> bullets = new List<GameObject>();
	// Use this for initialization
	void Awake () {
		current = this;
		for(int i = 0; i < 32; i++)
		{
		GameObject bullet = GameObject.Instantiate(bulletprefab);
		bullet.transform.parent = this.transform;
		bullet.SetActive(false);
		bullets.Add(bullet);
		}
		
	}
	
	// Update is called once per frame
	public GameObject GetBullet()
	{
		return bullets.FirstOrDefault(x => !x.activeInHierarchy);
	}
	
}
