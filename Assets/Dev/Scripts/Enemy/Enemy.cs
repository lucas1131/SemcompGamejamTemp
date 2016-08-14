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
    public float money;

    public Sprite cuteSprite;
    public Sprite normalSprite;
    private SpriteRenderer sr;

    bool attacking;

    // Use this for initialization
    void Start()
    {
        // Spawna inimigos com os sprites certos
        this.sr = GetComponent<SpriteRenderer>();
        if (RealityController.cuteReality)
            this.sr.sprite = cuteSprite;
        else
            this.sr.sprite = normalSprite;

        // Inicializa
        hitpoints = 100;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bat")
        {
            TakeDamage(other.GetComponent<Bat>().damage);
        }
    }

    public void TakeDamage(float damage)
    {
        if (hitpoints <= 0)
        {
            AudioSource audio = this.GetComponent<AudioSource>();
            audio.Play();
            // esperar terminar de tocar o audio
            target.GetComponent<Player>().money += money;
            this.manager.GetComponent<GameManager>().enemyCounter--;
            gameObject.GetComponent<BloodSpiller>().SpillBlood();
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

    public void ChangeSprite()
    {
        if (this.sr.sprite == this.cuteSprite)
            this.sr.sprite = this.normalSprite;
        else
            this.sr.sprite = this.cuteSprite;
    }
}