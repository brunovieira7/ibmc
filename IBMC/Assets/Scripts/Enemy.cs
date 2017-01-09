using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Enemy : MonoBehaviour {

	public Image healthBar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void takeDamage(int dmg) {
		RectTransform rect = healthBar.GetComponent<RectTransform> ();
		Vector3 currScale = rect.localScale;
		currScale.x -= 0.1f;

		rect.localScale = currScale;
	}
}
