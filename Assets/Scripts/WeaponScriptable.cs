using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable/Weapon", order = 2)]
public class WeaponScriptable : WeaponGenericScriptable {
	//public int baseDamage;
	public float forceToProjectile;
	public float dispersion; //dispersão das balas (oposto de precisão)
	public List<int> angles = new List<int>();
	public ScriptableObject projectile;
	public float mass;

	public bool isExplosive; //single use - explodes

	//public Projectile projectilePrefab;
}
