using UnityEngine;
using System.Collections;

public class Bullet : Projectile {

	public BulletScriptable bulletScript;
	float speed;
	float health;
	float moveAngle;

	// Use this for initialization
	void Start () {
		Sprite sprt = bulletScript.sprite;
		if (RealityController.cuteReality)
			sprt = bulletScript.spriteCute;
		gameObject.GetComponent<SpriteRenderer>().sprite = sprt;
		gameObject.GetComponent<Rigidbody2D> ().mass = bulletScript.mass;
		health = bulletScript.health;
	}
		
	public override void GetShot(float force, float angle){
		speed = force / bulletScript.mass;
		angle = Mathf.Deg2Rad * angle;
		moveAngle = angle;
		//print ("Angulo da bala" + angle + " Seno: " + Mathf.Sin(angle) + " Cos: " + Mathf.Cos(angle) );
		gameObject.GetComponent<Rigidbody2D> ().velocity = 
			new Vector3 (speed * Mathf.Cos(angle), speed * Mathf.Sin(angle), 0);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		health -= bulletScript.healthDepleteRate;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D (Collision2D col){
//		print ("Collided");
//		gameObject.GetComponent<Collider2D> ().enabled = false;
	}

	void OnCollisionExit2D(Collision2D col){
//		gameObject.GetComponent<Collider2D> ().enabled = false;
	}

	void OnTriggerEnter2D (Collider2D other) {

        // Empurrar todos os objetos que nao sao projeteis ou paredes
        Rigidbody2D otherRigidBody = other.gameObject.GetComponent<Rigidbody2D> ();
		if (otherRigidBody != null && 
            otherRigidBody.gameObject.tag != "PProjectile" &&
            otherRigidBody.gameObject.tag != "EProjectile" &&
            other.gameObject.GetComponent<Rigidbody2D>().mass > 0.01) {

			float otherSpeed = speed * bulletScript.mass
			                   / other.gameObject.GetComponent<Rigidbody2D> ().mass;
			//otherSpeed /= 2;
			otherRigidBody.velocity = 
				new Vector3 (otherSpeed*Mathf.Cos(moveAngle) + 
					otherRigidBody.velocity.x, 
					otherSpeed*Mathf.Sin(moveAngle) + 
					otherRigidBody.velocity.y, 0);
			health -= 1;
		}

        // Checar colisao com inimigos
        if(other.gameObject.tag == "Enemy" && gameObject.tag == "PProjectile")
        {
            Enemy e = other.GetComponent<Enemy>();
            e.TakeDamage(bulletScript.damage);
        }

        // Checar bala com player
        if (other.gameObject.tag == "Player")
        {
            Player p = other.GetComponent<Player>();
            p.TakeDamage(bulletScript.damage);

        }
    }

	public override void ChangeSprite (){
		if (RealityController.cuteReality) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = bulletScript.spriteCute;
		} else {
			gameObject.GetComponent<SpriteRenderer> ().sprite = bulletScript.sprite;
		}
	}
}
