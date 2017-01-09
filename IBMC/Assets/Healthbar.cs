using UnityEngine;
using System.Collections;

public class Healthbar : MonoBehaviour {

	public void takeDamage(int dmg) {
			Vector3 currScale = transform.localScale;
			currScale.x -= 0.1f;

			transform.localScale = currScale;

	}
}
