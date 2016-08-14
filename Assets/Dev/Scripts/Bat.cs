using UnityEngine;
using System.Collections;

public class Bat : MonoBehaviour
{

    public float damage;

    public Sprite cuteSprite;
    public Sprite normalSprite;
    public SpriteRenderer sr;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSprite()
    {
        if (RealityController.cuteReality)
            this.sr.sprite = this.cuteSprite;
        else
            this.sr.sprite = this.normalSprite; 
    }
}
