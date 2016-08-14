using UnityEngine;
using System.Collections;

public class Weapon : Projectile {

	public WeaponScriptable weaponScript;
	public GameObject projectileSpawner;
	public Projectile projectile;
	private float ticksToNextShot;

	void Shoot(){

        Projectile b;

        gameObject.GetComponent<AudioSource>().Play();
		ticksToNextShot = WeaponGenericScriptable.fps / weaponScript.fireRate;

        for (int i = 0; i < weaponScript.angles.Count; i++)
        {
            //Ajusta ângulo
            float angle = gameObject.transform.eulerAngles.z;
            angle += weaponScript.angles[i];
            angle += Random.Range(-weaponScript.dispersion, weaponScript.dispersion);
            //Instancia
            b = (Projectile)Instantiate(projectile, projectileSpawner.transform.position, Quaternion.identity);
            b.GetShot(weaponScript.forceToProjectile, angle);
        }
	}

	public override void GetShot(float force, float angle){
		float speed = force / weaponScript.mass;
		angle = Mathf.Deg2Rad * angle;
		//print ("Angulo da bala" + angle + " Seno: " + Mathf.Sin(angle) + " Cos: " + Mathf.Cos(angle) );
		gameObject.GetComponent<Rigidbody2D> ().velocity = 
			new Vector3 (speed * Mathf.Cos(angle), speed * Mathf.Sin(angle), 0);
	}

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<SpriteRenderer> ().sprite = weaponScript.sprite;
		ticksToNextShot = 60/weaponScript.fireRate;
		Bullet bullet = projectile.GetComponent<Bullet> ();
		if (bullet != null) {
			//print ("Entrou aqui - Bullet");
			bullet.bulletScript = (BulletScriptable)(weaponScript.projectile);
		} else {
			//print ("Entrou aqui - Weapon");
			Weapon bomb = projectile.GetComponent<Weapon> ();
			if (bomb != null) {
				bomb.weaponScript = (WeaponScriptable)(weaponScript.projectile);
			}
		}
        ChangeSprite();
	}

    void OnEnable()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = weaponScript.sprite;
        ticksToNextShot = 60 / weaponScript.fireRate;
        Bullet bullet = projectile.GetComponent<Bullet>();
        if (bullet != null)
        {
            //print("Entrou aqui - Bullet");
            bullet.bulletScript = (BulletScriptable)(weaponScript.projectile);
        }
        else
        {
            //print("Entrou aqui - Weapon");
            Weapon bomb = projectile.GetComponent<Weapon>();
            if (bomb != null)
            {
                bomb.weaponScript = (WeaponScriptable)(weaponScript.projectile);
            }
        }
        ChangeSprite();
    }

    // Update is called once per frame
    void Update()
    {
        ticksToNextShot--;
        if (ticksToNextShot <= 0)
        {
            ticksToNextShot = 0;
        }
        //Explosao automatica de explosivos
        if (weaponScript.isExplosive)
        {
            if (ticksToNextShot <= 0)
            {
                Shoot();
                Destroy(gameObject);
            }
        }

        //		if (Input.GetKeyDown (KeyCode.Space) && !weaponScript.automatic) {
        //			DemandShot ();
        //		}
        //		if (Input.GetKey (KeyCode.Space) && weaponScript.automatic) {
        //			DemandShot ();
        //		}
    }

    public void DemandShot()
    {
        if (!weaponScript.isExplosive)
        {
            if (ticksToNextShot <= 0)
            {
                Shoot();
            }
        }
    }

    public override void ChangeSprite()
    {
        if (RealityController.cuteReality)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = weaponScript.spriteCute;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = weaponScript.sprite;
        }
        ChangeAudio();
    }

    private void ChangeAudio()
    {
        if (RealityController.cuteReality)
        {
            gameObject.GetComponent<AudioSource>().clip = weaponScript.audioCute;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().clip = weaponScript.audioGore;
        }
    }
}
