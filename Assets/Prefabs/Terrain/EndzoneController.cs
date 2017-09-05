using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndzoneController : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll)
	{
		GameObject.FindGameObjectWithTag ("GameController").SendMessage ("LoadScene", "game over");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
