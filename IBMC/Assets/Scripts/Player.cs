using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	enum statusEnum { IDLE, WALKING, AIMING }

	private Rigidbody2D rb2D;
	public float playerSpeed;

	private Vector2 walkDestination;

	public GameObject bullet;
	public Texture2D castCursor;

	private bool isMoving = false;
	private bool isAiming = false;
	private bool isFeared = false;

	public GameObject fearIcon;
	private GameObject fearIconInstance;
	private float fearTimer;

	public Image healthBar;

	public int maxHealth;
	private int currentHealth;

	private bool isDead = false;

	private GameObject skillSelect;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent <Rigidbody2D> ();
		currentHealth = maxHealth;
		skillSelect = GameObject.Find ("skillSelect");
		skillSelect.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) {
			return;
		}

		if (!isIncapacitated ()) {
			if (Input.GetMouseButtonDown (0)) {
				if (isAiming) {
					skillSelect.SetActive (false);
					fire ();
				} else {
					move ();
				}
			}

			if (Input.GetKeyDown ("1")) {
				isAiming = true;
				selectButton (-0.8f);
				//Cursor.SetCursor (castCursor, Vector2.zero, CursorMode.Auto);
			}
			if (Input.GetKeyDown ("2")) {
				selectButton(0f);
				//Cursor.SetCursor (castCursor, Vector2.zero, CursorMode.Auto);
			}
			if (Input.GetKeyDown ("3")) {
				selectButton(0.8f);
				//Cursor.SetCursor (castCursor, Vector2.zero, CursorMode.Auto);
			}
		}

		if (isFeared) {
			fearTimer += Time.deltaTime;

			if (fearTimer < 2f) {
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

	void selectButton(float loc) {
		Vector3 currPos = skillSelect.transform.position;
		currPos.x = loc + transform.position.x;

		skillSelect.transform.position = currPos;
		skillSelect.SetActive (true);
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

	public void takeDamage(int dmg) {
		if (isDead) {
			return;
		}

		currentHealth -= dmg;

		if (currentHealth <= 0) {
			isDead = true;
		}

		RectTransform rect = healthBar.GetComponent<RectTransform> ();

		float newScale = (float) currentHealth / maxHealth;

		Vector3 currScale = rect.localScale;
		currScale.x = newScale;

		rect.localScale = currScale;
	}

	public bool isPlayerDead() {
		return isDead;
	}
}
