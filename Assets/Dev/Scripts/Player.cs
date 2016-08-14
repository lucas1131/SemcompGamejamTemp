using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Rigidbody2D rigid;
    Animator anim;
    public Animator batAnim;

    public GameObject bat;

    public float speed;
    public float hitpoints;
    public float money;
    public float delayBetweenSwings;

    public bool gameOver;
    bool canSwing;

    float currentWeapon;

	// Use this for initialization
	void Start () {

        hitpoints = 100;
        currentWeapon = 1;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canSwing = true;
        gameOver = false;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (gameOver == false)
        {
            float actualSpeed = speed;
            if (Input.GetAxisRaw("Horizontal") * Input.GetAxisRaw("Vertical") != 0)
            {
                actualSpeed /= Mathf.Sqrt(Mathf.Pow(Input.GetAxisRaw("Horizontal"), 2) + Mathf.Pow(Input.GetAxisRaw("Vertical"), 2));
            }
            rigid.velocity = new Vector2(actualSpeed * Input.GetAxisRaw("Horizontal"), actualSpeed * Input.GetAxisRaw("Vertical"));

            if (rigid.velocity.magnitude > 0)
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
                Shoot();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (canSwing == true)
                {
                    StartCoroutine(Melee());
                }
            }
        }
        else
        {
            rigid.velocity = Vector2.zero;
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
            currentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentWeapon = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentWeapon = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            currentWeapon = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            currentWeapon = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            currentWeapon = 8;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            currentWeapon = 9;
        }

        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            if(currentWeapon == 9)
            {
                currentWeapon = 1;
            }
            else
            {
                currentWeapon++;
            }

        }
        else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            if (currentWeapon == 1)
            {
                currentWeapon = 9;
            }
            else {
                currentWeapon--;
            }
        }
    }

    void Shoot()
    {
        
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
        if(tag != "Bat" && other.tag == "Claw")
        {
            hitpoints -= other.GetComponent<Claw>().damage;
            if(hitpoints <= 0)
            {
                gameOver = true;
            }
        }
    }
}
