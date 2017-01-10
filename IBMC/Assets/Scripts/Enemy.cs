using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Enemy : MonoBehaviour {

	public Image healthBar;
	public GameObject fire;
	public GameObject player;

	private float runningTimer;
	public float enemySpeed;

	// Use this for initialization
	void Start () {
		runningTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		followPlayer ();
		runningTimer += Time.deltaTime;

		if (runningTimer > 3f) {
			//castFires ();
			castFear ();
			runningTimer = 0f;
		}
	}

	public void takeDamage(int dmg) {
		RectTransform rect = healthBar.GetComponent<RectTransform> ();
		Vector3 currScale = rect.localScale;
		currScale.x -= 0.1f;

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
		transform.position = Vector3.MoveTowards (transform.position, player.transform.position, enemySpeed * Time.deltaTime);
	}
}
