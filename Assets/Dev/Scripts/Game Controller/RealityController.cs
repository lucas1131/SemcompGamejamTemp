using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RealityController : MonoBehaviour
{

    public static float rs = 100;
    public float rsFillSpeed;
    public float rsDepleteSpeed;
    [SerializeField]
    float RSMAX = 10;
    [Range(0.0f, 1.0f)]
    public float rsMinToGoCute; // 0-1 em porcentagem

    public static bool cuteReality;

    // Player references
    public Player player;

    // Background references
    public GameObject activeBg;
    public Sprite cuteBg;
    public Sprite normalBg;

    // Sound reference
    public AudioSource cuteBGM;
    public AudioSource normalBGM;

    // Use this for initialization
    void Start()
    {
        RealityShift();
    }

    // Update is called once per frame
    void Update()
    {
        // Ajusta contador da barra de realidade
        if (cuteReality == false)
        {
            rs += rsFillSpeed;
            if (rs >= RSMAX)
                rs = RSMAX;
        }
        else
        {
            rs -= rsDepleteSpeed;
            if (rs <= 0 && RealityController.cuteReality)
            {
                rs = 0;
                RealityShift();
            }
        }
        //Troca de realidade
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Se estiver ativo, desativa
            if (RealityController.cuteReality)
            {
                RealityShift();
            }
            // Se nao estiver ativo, checa se ja passou do minimo necessario para ativar
            else
            {
                if (rs >= RSMAX * rsMinToGoCute)
                {
                    // Ativa
                    RealityShift();
                }
            }
        }

        //player.canAttack = cuteReality;
    }

    // Converte todos os objetos da cena para o modo fofo :3 ou reverte ao normal
    public void RealityShift()
    {
        // Inverte a flag
        RealityController.cuteReality = !RealityController.cuteReality;

        // Shiftar inimigos
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        //print(objects.Length + " enemies found!");
        foreach (GameObject i in objects)
        {
            Enemy e = i.GetComponent<Enemy>();
            e.ChangeSprite();
        }

        // Shiftar tiros - inimigos
        objects = GameObject.FindGameObjectsWithTag("EProjectile");
        //print(objects.Length + " eprojectiles found!");
        foreach (GameObject i in objects)
        {
            Projectile b = i.GetComponent<Projectile>();
            b.ChangeSprite();
        }

        // Shiftar sangue 
        objects = GameObject.FindGameObjectsWithTag("Blood");
        //print(objects.Length + " eprojectiles found!");
        foreach (GameObject i in objects)
        {
            BloodSpill bs = i.GetComponent<BloodSpill>();
            bs.ChangeSprite();
        }

        // Shiftar player
        player.ChangeSprite();

        // Shiftar tiros - player
        objects = GameObject.FindGameObjectsWithTag("PProjectile");
        //print(objects.Length + " pprojectiles found!");
        foreach (GameObject i in objects)
        {
            Projectile b = i.GetComponent<Projectile>();
            b.ChangeSprite();
        }

        // Shiftar armas - player
        objects = GameObject.FindGameObjectsWithTag("PWeapon");
        //print(objects.Length + " pprojectiles found!");
        foreach (GameObject i in objects)
        {
            Weapon w = i.GetComponent<Weapon>();
            w.ChangeSprite();
        }


        // Shiftar activeBg
        Sprite bg = activeBg.GetComponent<SpriteRenderer>().sprite;
        if (RealityController.cuteReality)
            bg = cuteBg;
        else
            bg = normalBg;

        activeBg.GetComponent<SpriteRenderer>().sprite = bg;

        // Shiftar bgm
        if (RealityController.cuteReality)
        {
            normalBGM.Stop();
            cuteBGM.Play();
        }
        else
        {
            cuteBGM.Stop();
            normalBGM.Play();
        }
    }
}
