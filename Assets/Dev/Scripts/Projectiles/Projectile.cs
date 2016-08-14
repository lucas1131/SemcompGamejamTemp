using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour {

	public abstract void GetShot (float force, float angle);

    public abstract void ChangeSprite();
}
