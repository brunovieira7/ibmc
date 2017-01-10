using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

	public float timeToDie;
	private float elapsedTime;

	public int fireDmg;

	// Use this for initialization
	void Start () {
		elapsedTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;

		if (elapsedTime > timeToDie) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		GameObject hit = collision.gameObject;
		if (hit.tag == "Player") {
			Player player = hit.GetComponent<Player> ();
			player.takeDamage(fireDmg);
		}
	}
}
