using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
	public float distToClose = 2.0f;
	public float secondsDelay = 0.2f;

	public Transform playerTf;
	public float dist = 100f;

	public bool isClosed = false;
	public bool isClosing = false;

	// Use this for initialization
	void Start () {
		playerTf = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		dist = Vector3.Distance(transform.position, playerTf.position);

		if(dist <= distToClose)
		{
			//CloseDoor ();

			if (!isClosing) {
				isClosing = true;
				Invoke ("CloseDoor", secondsDelay);
			}
		}
		else
		{
			isClosing = false;
			OpenDoor ();
//			isClosing = false;
//			if(isClosed)
//			{
//				OpenDoor ();
//			}
		}
	}

	void CloseDoor()
	{
		if (isClosed || !isClosing)
			return;
		transform.position = new Vector3 (transform.position.x, transform.position.y - 0.5f);
		isClosed = true;
		isClosing = false;
	}

	void OpenDoor()
	{
		if (!isClosed)
			return;
		transform.position = new Vector3 (transform.position.x, transform.position.y + 0.5f);
		isClosed = false;
	}
}
