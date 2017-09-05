using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour {

	private Canvas canvas;

	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas> ();
		canvas.enabled = false;
		//gameObject.SetActive (false);
	}

	public void Pause()
	{
		canvas.enabled = true;
		Time.timeScale = 0.0f;
	}

	public void Resume()
	{
		canvas.enabled = false;
		Time.timeScale = 1.0f;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(canvas.enabled)
			{
				Resume ();
			}
			else
			{
				Pause ();
			}
		}
	}
}
