using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    Rigidbody2D rigid;
    Animator anim;
    public Animator batAnim;

    public GameObject bat;

    public float speed;
    public float hitpoints;
    public float money;
    public float delayBetweenSwings;

    bool canSwing;

    public Sprite cuteSprite;
    public Sprite normalSprite;
    public SpriteRenderer sr;

    int currentWeapon;
    public List<Weapon> weaponList = new List<Weapon>();

	// Use this for initialization
	void Start () {

        hitpoints = 100;
        currentWeapon = 1;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canSwing = true;

        foreach (Weapon w in weaponList)
        {
            w.gameObject.SetActive(false);
        }

        weaponList[currentWeapon - 1].gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {

        float actualSpeed = speed;
        if(Input.GetAxisRaw("Horizontal")* Input.GetAxisRaw("Vertical") != 0)
        {
            actualSpeed /= Mathf.Sqrt(Mathf.Pow(Input.GetAxisRaw("Horizontal"), 2) + Mathf.Pow(Input.GetAxisRaw("Vertical"), 2));
        }
        rigid.velocity = new Vector2(actualSpeed*Input.GetAxisRaw("Horizontal"), actualSpeed*Input.GetAxisRaw("Vertical"));

        if(rigid.velocity.magnitude > 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        LookAtMouse();
        ChangeWeapons();

        if (Input.GetMouseButtonDown(0))
        {
            //Shoot();
            GetComponentInChildren<Weapon>().DemandShot();
        }

        if (Input.GetMouseButton(0) && GetComponentInChildren<Weapon>().weaponScript.automatic)
        {
            GetComponentInChildren<Weapon>().DemandShot();
        }

        else if (Input.GetMouseButtonDown(1))
        {
            if (canSwing == true)
            {
                StartCoroutine(Melee());
            }
        }





    }

    void LookAtMouse()
    {
        float theta = 0;
        float deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        float deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;

        if (deltaX == 0)
        {
            if (deltaY > 0)
            {
                theta = 90;
            }
            else if (deltaY < 0)
            {
                theta = 270;
            }
        }
        else
        {
            if (deltaX > 0)
            {
                theta = Mathf.Atan(deltaY / deltaX);
            }
            else if (deltaX < 0)
            {
                theta = Mathf.Atan(deltaY / deltaX) + Mathf.PI;
            }
        }
        transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg*theta);

    }

    void ChangeWeapons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponList[currentWeapon - 1].gameObject.SetActive(false);
            weaponList[0].gameObject.SetActive(true);
            currentWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponList[currentWeapon - 1].gameObject.SetActive(false);
            weaponList[1].gameObject.SetActive(true);
            currentWeapon = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponList[currentWeapon - 1].gameObject.SetActive(false);
            weaponList[2].gameObject.SetActive(true);
            currentWeapon = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            weaponList[currentWeapon - 1].gameObject.SetActive(false);
            weaponList[3].gameObject.SetActive(true);
            currentWeapon = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            weaponList[currentWeapon - 1].gameObject.SetActive(false);
            weaponList[4].gameObject.SetActive(true);
            currentWeapon = 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            weaponList[currentWeapon - 1].gameObject.SetActive(false);
            weaponList[5].gameObject.SetActive(true);
            currentWeapon = 6;
        }


        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            if(currentWeapon >= 6)
            {
                weaponList[currentWeapon - 1].gameObject.SetActive(false);
                weaponList[0].gameObject.SetActive(true);
                currentWeapon = 1;
            }
            else
            {
                weaponList[currentWeapon - 1].gameObject.SetActive(false);
                currentWeapon++;
                weaponList[currentWeapon-1].gameObject.SetActive(true);
            }

        }
        else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            if (currentWeapon <= 1)
            {
                weaponList[currentWeapon - 1].gameObject.SetActive(false);
                weaponList[5].gameObject.SetActive(true);
                currentWeapon = 6;
            }
            else {
                weaponList[currentWeapon - 1].gameObject.SetActive(false);
                currentWeapon--;
                weaponList[currentWeapon - 1].gameObject.SetActive(true);
            }
        }
    }

    IEnumerator Melee()
    {
        bat.SetActive(true);
        batAnim.SetBool("Swing", true);
        canSwing = false;
        yield return new WaitForSeconds(10*Time.deltaTime);
        batAnim.SetBool("Swing", false);
        bat.SetActive(false);
        yield return new WaitForSeconds(delayBetweenSwings);
        canSwing = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Claw")
        {
            TakeDamage(other.GetComponent<Claw>().damage);
        }
    }

    public void TakeDamage(float damage)
    {
        //if(RealityController.cuteReality)
        hitpoints -= damage;
        // Game Over quando vida chegar em 0
        //if (hitpoints <= 0) ;
    }

    public void ChangeSprite()
    {
        // TROCAR ANIMAÇÕES
        if (RealityController.cuteReality)
            this.sr.sprite = this.cuteSprite;
        else
            this.sr.sprite = this.normalSprite;
    }
}
