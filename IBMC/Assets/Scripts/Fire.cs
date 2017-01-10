using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

	public float timeToDie;
	private float elapsedTime;

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

	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject hit = collision.gameObject;
		if (hit.tag == "Player") {
			Debug.Log ("PLAYERDMG");
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		GameObject hit = collision.gameObject;
		if (hit.tag == "Player") {
			Debug.Log ("COLLIDED");
		}
	}
}
