using UnityEngine;
using System.Collections;

public class BloodSpill : MonoBehaviour {

	public Sprite spillSpriteGore;
	public Sprite spillSpriteCute;
	public int expireTicks = 600;

	// Use this for initialization
	void Start () {
		expireTicks += Random.Range (-120, 120);
		if (RealityController.cuteReality) {
			GetComponent<SpriteRenderer> ().sprite = spillSpriteCute;
		} else {
			GetComponent<SpriteRenderer> ().sprite = spillSpriteGore;
		}
	}
	
	// Update is called once per frame
	void Update () {
		expireTicks--;
		if (expireTicks <= 0) {
			Destroy (gameObject);
		}
	}

	public void ChangeSprite(){
		if (RealityController.cuteReality) {
			GetComponent<SpriteRenderer> ().sprite = spillSpriteCute;
		} else {
			GetComponent<SpriteRenderer> ().sprite = spillSpriteGore;
		}
	}
}
