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
	private bool isFeared = false;

	public GameObject fearIcon;
	private GameObject fearIconInstance;
	private float fearTimer;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent <Rigidbody2D> ();
		playerSpeed = 5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isIncapacitated ()) {
			if (Input.GetMouseButtonDown (0)) {
				if (isAiming) {
					fire ();
				} else {
					move ();
				}
			}

			if (Input.GetKeyDown ("1")) {
				isAiming = true;
				//Cursor.SetCursor (castCursor, Vector2.zero, CursorMode.Auto);
			}
		}

		if (isFeared) {
			fearTimer += Time.deltaTime;

			if (fearTimer < 1f) {
				isMoving = true;

			} else {
				isFeared = false;
				isMoving = false;
				fearTimer = 0f;
				Destroy (fearIconInstance);
			}
		}

		if (isMoving) { 
			rb2D.position = Vector3.MoveTowards (rb2D.position, walkDestination, playerSpeed * Time.deltaTime);
			if (rb2D.position == walkDestination) {
				if (!isFeared) {
					isMoving = false;
				} else {
					walkDestination = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0f);
				}
					
			}
		}
	}

	bool isIncapacitated() {
		return isFeared;
	}

	void fire() {
		isAiming = false;
		//Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
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

	public void takeSpell() {
		if (!isFeared) {
			isFeared = true;
			fearTimer = 0f;

			Vector3 start = new Vector3 (rb2D.position.x, rb2D.position.y + 0.8f, 0f);
			fearIconInstance = Instantiate (fearIcon, start, Quaternion.identity) as GameObject;
			fearIconInstance.transform.parent = gameObject.transform;

			walkDestination = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0f);
		}
	}
}
