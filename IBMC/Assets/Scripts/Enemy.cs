using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Enemy : MonoBehaviour {

	public Image healthBar;
	public GameObject fire;
	public GameObject player;
	private Player playerScript;

	private float runningTimer;
	public float enemySpeed;

	private bool isDead = false;

	public int maxHealth;
	private int currentHealth;

	public int meleeDmg;

	// Use this for initialization
	void Start () {
		runningTimer = 0f;
		currentHealth = maxHealth;
		playerScript = player.GetComponent<Player> ();
	}

	// Update is called once per frame
	void Update () {
		if (isDead || playerScript.isPlayerDead()) {
			return;
		}
		
		followPlayer ();
		runningTimer += Time.deltaTime;

		if (runningTimer > 3f) {
			castFires ();
			//castFear ();
			runningTimer = 0f;
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

	void castFires() {
		Vector3 position = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0f);
		GameObject instance = Instantiate (fire, position, Quaternion.identity) as GameObject;
	}

	void castFear() {
		var script = player.GetComponent<Player>();
		if (script != null) {
			script.takeSpell ();
		}
	}

	//ajustar para tentar parar sobrepondo?
	void followPlayer() {
		Vector3 position = player.transform.position;
		position.y -= player.transform.localScale.y;
		transform.position = Vector3.MoveTowards (transform.position, position, enemySpeed * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject hit = collision.gameObject;
		if (hit.tag == "Player") {
			Player player = hit.GetComponent<Player> ();
			player.takeDamage(meleeDmg);
		}
	}

}
