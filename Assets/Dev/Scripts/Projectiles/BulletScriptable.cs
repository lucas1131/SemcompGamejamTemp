using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Bullet", menuName = "Scriptable/Bullet", order = 1)]
public class BulletScriptable : ScriptableObject {

	public float damage;
	public float health; //para amortecer em inimigos e com o tempo
	public float healthDepleteRate; //reduz vida da bala com o tempo - para expirar
	public bool damageByHealth; //se o dano é proporcional à vida da bala
	public float mass;

	public Sprite sprite;
	public Sprite spriteCute;

}
