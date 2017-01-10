using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private Rigidbody2D rb2D;
	private Vector2 end;
	public float speed;

	public int bulletDmg;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent <Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (end != null) {
			rb2D.velocity = end * speed;
		}
	}

	public void fireBullet(Vector2 end) {
		this.end = end;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject hit = collision.gameObject;
		if (hit.tag == "Enemy") {
			Debug.Log (hit);
			Enemy enemy = hit.GetComponent<Enemy> ();
			enemy.takeDamage(bulletDmg);
		}

		//Debug.Log("collided" );
		Destroy(gameObject);
	}
}
