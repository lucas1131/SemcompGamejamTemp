using UnityEngine;
using System.Collections;

public class WeaponGenericScriptable : ScriptableObject {
	public float fireRate;
	public bool automatic;
	public Sprite sprite;
	public Sprite spriteCute;

	public static float fps = 60; 
}
