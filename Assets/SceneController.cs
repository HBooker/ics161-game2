using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void LoadScene(string sceneName)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (sceneName);
	}

	public void QuitApplication()
	{
		Application.Quit ();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
