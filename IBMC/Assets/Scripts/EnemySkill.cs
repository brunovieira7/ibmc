using UnityEngine;
using System.Collections;

public class EnemySkill
{
	public float cast;
	public float cd;
	public float rng;

	private float nextCast;
	private float actualCD;
	private float castingTime;
	private bool isCasting = false;

	public EnemySkill (float cast, float cd, float rng) {
		this.cast = cast;
		this.cd = cd;
		this.rng = rng;

		nextCast = cd + Random.Range (0f, rng);
		actualCD = 0f;
		castingTime = 0f;
	}

	public bool canUseSkill(float delta) {
		actualCD += delta;

		if (isCasting) {
			castingTime += delta;

			if (castingTime > cast) {
				isCasting = false;
				castingTime = 0f;
				actualCD = 0f;

				return true;
			}
		}

		if (actualCD > nextCast) {
			isCasting = true;
		}

		return false;
	}

	public bool isCastingSkill() {
		return isCasting;
	}

}

