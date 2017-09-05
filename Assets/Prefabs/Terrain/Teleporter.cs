using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
	public Transform dest;

	void OnTriggerEnter2D(Collider2D coll)
	{
		//teleport
		coll.transform.position = dest.position;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
