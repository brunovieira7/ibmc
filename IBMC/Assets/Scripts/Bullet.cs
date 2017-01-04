﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private Rigidbody2D rb2D;
	private Vector2 end;
	public float speed;

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
}
