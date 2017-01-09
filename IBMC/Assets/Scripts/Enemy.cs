using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void takeDamage(int dmg) {
		Healthbar healthbar = GetComponent<Healthbar> ();
		healthbar.takeDamage (dmg);
	}
}
