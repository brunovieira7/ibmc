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

	private EnemySkill fearSkill;
	private EnemySkill fireSkill;

	private Animator animator;


	// Use this for initialization
	void Start () {
		runningTimer = 0f;
		currentHealth = maxHealth;
		playerScript = player.GetComponent<Player> ();
		fearSkill = new EnemySkill (1f, 10f, 3f);
		fireSkill = new EnemySkill (2.9f, 3f, 1f);
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		if (isDead || playerScript.isPlayerDead()) {
			return;
		}

		bool skillStarted = false;
		if (fearSkill.canUseSkill (Time.deltaTime, out skillStarted)) {
			castFear ();
		}


		if (fireSkill.canUseSkill (Time.deltaTime, out skillStarted)) {
			castFires ();
		}

		if (skillStarted) {
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("enemy_walk_s")) {
				animator.SetTrigger ("stomp_s");
			} else {
				animator.SetTrigger ("stomp_n");
			}
		}

		if (!fearSkill.isCastingSkill () && !fireSkill.isCastingSkill ()) {
			followPlayer ();
		}
		runningTimer += Time.deltaTime;
	}
		
	public void takeDamage(int dmg) {
		if (isDead) {
			return;
		}

		currentHealth -= dmg;

		if (currentHealth <= 0) {
			isDead = true;
			animator.SetTrigger ("death");
		}
				
		RectTransform rect = healthBar.GetComponent<RectTransform> ();

		float newScale = (float) currentHealth / maxHealth;

		Vector3 currScale = rect.localScale;
		currScale.x = newScale;

		rect.localScale = currScale;
	}

	void castFires() {

		for (int i = 0; i <= 20; i++) {
			Vector3 position = new Vector3 (Random.Range (-7.5f, 7.5f), Random.Range (-7.5f, 7.5f), 0f);
			//Vector3 position = player.transform.position;
			GameObject instance = Instantiate (fire, position, Quaternion.identity) as GameObject;
		}
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

		float diffX = transform.position.x - position.x;
		if ( (transform.localScale.x > 0 && diffX > 0) || (transform.localScale.x < 0 && diffX < 0) ) {
			//Debug.Log(transform.localScale.x + " " + diff)
			Vector3 curr = transform.localScale;
			curr.x *= -1;
			transform.localScale = curr;
		}

		float diffY = transform.position.y - position.y;
		Debug.Log (animator.GetCurrentAnimatorStateInfo (0).IsName("enemy_walk_s"));
		if (diffY < 0 && animator.GetCurrentAnimatorStateInfo (0).IsName("enemy_walk_s")) {
			animator.SetTrigger ("walk_n");
		}
		if (diffY > 0 && animator.GetCurrentAnimatorStateInfo (0).IsName("enemy_walk_n")) {
			animator.SetTrigger ("walk_s");
		}
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
