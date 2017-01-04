using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	enum statusEnum { IDLE, WALKING, AIMING }

	private Rigidbody2D rb2D;
	private float playerSpeed;

	private Vector2 walkDestination;

	public GameObject bullet;
	public Texture2D castCursor;

	private bool isMoving = false;
	private bool isAiming = false;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent <Rigidbody2D> ();
		playerSpeed = 5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (isAiming) {
				fire ();
			} else {
				move ();
			}
		}

		if (Input.GetKeyDown ("1")) {
			isAiming = true;
			Cursor.SetCursor (castCursor, Vector2.zero, CursorMode.Auto);
		}

		if (isMoving) { 
			rb2D.position = Vector3.MoveTowards (rb2D.position, walkDestination, playerSpeed * Time.deltaTime);
			if (rb2D.position == walkDestination) {
				isMoving = false;
			}
		}
	}

	void fire() {
		isAiming = false;
		Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
		Vector3 start = new Vector3 (rb2D.position.x, rb2D.position.y, 0f);
		GameObject instance = Instantiate (bullet, start, Quaternion.identity) as GameObject;

		Physics2D.IgnoreCollision (instance.GetComponent <Collider2D> (), GetComponent <Collider2D> ());

		Vector3 position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		position.z = 0f;

		Vector2 direction = position - this.transform.position;
		direction.Normalize();

		var script = instance.GetComponent <Bullet> ();
		script.fireBullet (direction);
	}

	void move() {
		isMoving = true;
		walkDestination = Camera.main.ScreenToWorldPoint (Input.mousePosition);
	}
}
