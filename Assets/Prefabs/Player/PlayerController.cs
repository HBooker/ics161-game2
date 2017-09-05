using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float acceleration = 1.0f;
	public float jumpForce = 1000.0f;
	public float magDist = 500.0f;
	public float maxSpeed = 5.0f;

	public float magForce = 5.0f;
	public float magImpulse = 300.0f;
	public float selfForce = 5.0f;
	public float selfImpulse = 25.0f;

	public float secondsImpulseCooldown = 0.3f;

	public Sprite highPowerSprite;
	public Sprite lowPowerSprite;

	private float currentMag;
	private float currentSelf;
	private bool impulseReady = true;
	private bool impulseWait = false;

	private bool highPower = false;

	private bool inAir = false;
	private Rigidbody2D body;
	private Transform tf;
	private Camera cam;
	private Transform magTf;
	private LayerMask magLayer;
	//private bool polarity = true;

	private SpriteRenderer sRender;

	// Use this for initialization
	void Start () 
	{
		magLayer = LayerMask.NameToLayer("Magnetable");
		body = GetComponent<Rigidbody2D> ();
		tf = GetComponent<Transform> ();

		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera> ();
		magTf = GameObject.FindGameObjectWithTag ("Magnet").GetComponent<Transform> ();
		Physics.IgnoreLayerCollision (LayerMask.NameToLayer("Magnetable"), LayerMask.NameToLayer("Player"), true);

		currentMag = magForce;
		currentSelf = selfForce;

		sRender = GameObject.FindGameObjectWithTag ("Magnet").GetComponent<SpriteRenderer> ();
	} 
		

	void OnTriggerEnter2D(Collider2D coll)
	{
		inAir = false;
		//body.velocity = new Vector2 (body.velocity.x, 0.0f);
	}

	private void ProcessKeyInputs()
	{
		//body.velocity = new Vector2((Input.GetAxis ("Horizontal") * Time.deltaTime * velocity), body.velocity.y);
		//body.AddForce();

		if (Input.GetKey (KeyCode.D)) {
			if(body.velocity.x < maxSpeed)
				body.velocity = new Vector2 (Mathf.Min(maxSpeed, body.velocity.x + acceleration), body.velocity.y);
		} else if (Input.GetKey(KeyCode.A)) {
			if(body.velocity.x > maxSpeed * -1)
				body.velocity = new Vector2 (Mathf.Max(maxSpeed * -1, body.velocity.x - acceleration), body.velocity.y);
		}
			

		if (!inAir && Input.GetButtonDown ("Jump")) 
		{
			body.AddForce (new Vector2(0.0f, jumpForce));
			inAir = true;
		}

		if(Input.GetKeyDown(KeyCode.E))
		{

			highPower = !highPower;
//			magForce *= -1;
//			magImpulse *= -1;
//			selfForce *= -1;
//			selfImpulse *= -1;
			if(currentMag == magForce)
			{
				currentMag = magImpulse;
				currentSelf = selfImpulse;
				sRender.sprite = highPowerSprite;
			}
			else
			{
				currentMag = magForce;
				currentSelf = selfForce;
				sRender.sprite = lowPowerSprite;
			}
		}

		AttractObjects ();
	}

	private void RotateMagnet()
	{
		magTf.rotation = Quaternion.LookRotation (Vector3.forward, getMousePos() - magTf.position);
	}

	private Vector3 getMousePos()
	{
		return cam.ScreenToWorldPoint (Input.mousePosition);
	}

	private void ImpulseRecharge()
	{
		impulseReady = true;
		impulseWait = false;
	}

	private void AttractObjects()
	{
		Vector3 mouse = getMousePos();
		float angle = Mathf.Atan2(mouse.y, mouse.x) * Mathf.Rad2Deg;
		RaycastHit2D[] rays = Physics2D.BoxCastAll(tf.position, new Vector2(1.0f, 1.0f), angle, mouse - tf.position, magDist, 1 << magLayer);

		foreach(RaycastHit2D ray in rays)
		{
			if (highPower) 
			{
				if (impulseReady) {
					if (ray.rigidbody.mass < 100.0f) {
						if (Input.GetButtonDown ("Fire2")) {
							impulseReady = false;
							ray.rigidbody.AddForce ((body.position - ray.rigidbody.position).normalized * -currentMag);
						} else if (Input.GetButtonDown ("Fire1")) {
							impulseReady = false;
							ray.rigidbody.AddForce ((body.position - ray.rigidbody.position).normalized * currentMag);
						}
					} else {
						if (Input.GetButtonDown ("Fire2")) {
							impulseReady = false;
							body.AddForce ((ray.rigidbody.position - body.position).normalized * -currentSelf);
						} else if (Input.GetButtonDown ("Fire1")) {
							impulseReady = false;
							body.AddForce ((ray.rigidbody.position - body.position).normalized * currentSelf);
						}
					}

					if (!impulseReady && !impulseWait) {
						impulseWait = true;
						Invoke ("ImpulseRecharge", secondsImpulseCooldown);
					}
				}
			}
			else
			{
				if (ray.rigidbody.mass < 100.0f) {
					if (Input.GetButton ("Fire2")) {
						ray.rigidbody.AddForce ((body.position - ray.rigidbody.position).normalized * -currentMag);
					} else if (Input.GetButton ("Fire1")) {
						ray.rigidbody.AddForce ((body.position - ray.rigidbody.position).normalized * currentMag);
					}
				} else {
					if (Input.GetButton ("Fire2")) {
						body.AddForce ((ray.rigidbody.position - body.position).normalized * -currentSelf);
					} else if (Input.GetButton ("Fire1")) {
						body.AddForce ((ray.rigidbody.position - body.position).normalized * currentSelf);
					}
				}
			}
		}
	}

	void Update ()
	{
		if (Time.timeScale == 0.0f)
			return;

		RotateMagnet ();

		ProcessKeyInputs ();

		 
	}
}
