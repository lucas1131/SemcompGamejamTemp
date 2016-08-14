using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    Animator anim;
    public Animator clawAnim;

    public GameObject manager;
    public GameObject target;
    Rigidbody2D rigid;
    public float hitpoints;
    public float speed;

    bool attacking;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = target.transform.position.x - transform.position.x;
        float deltaY = target.transform.position.y - transform.position.y;

        rigid.velocity = new Vector2(deltaX / Vector2.Distance(target.transform.position, transform.position), deltaY / Vector2.Distance(target.transform.position, transform.position));
        LookAtPlayer(deltaX, deltaY);
    }

    void UpdateHitpoints(float damage)
    {
        if(hitpoints <= 0)
        {
            manager.GetComponent<GameManager>().enemyCounter--;
            Destroy(gameObject);
        }
        else
        {
            hitpoints -= damage;
        }
    }

    void LookAtPlayer(float deltaX, float deltaY)
    {
        float theta = 0;

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
        transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * theta);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bat")
        {
            UpdateHitpoints(other.GetComponent<Bat>().damage);
        }
    }
    
}