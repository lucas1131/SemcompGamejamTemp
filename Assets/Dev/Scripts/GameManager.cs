using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {

    public GameObject player;
    public GameObject enemy;
    public GameObject healthBar;
    public Text money;

    List<Vector3> spawnList = new List<Vector3>();

    float wave;
    float startWait;
    float spawnWait;
    public float minSpawnWait;
    public float maxSpawnWait;


    // DEBUG
    public Vector3 healthbarpos;
    public Vector3 healthbarshadowpos;
    public Vector3 moneypos;

	// Use this for initialization
	void Start () {
        wave = 3;
        startWait = 1;
             
        spawnList.Add(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, 0, 10)));
        spawnList.Add(Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height/2, 10)));
        spawnList.Add(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 10)));
        spawnList.Add(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height/2, 10)));

        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {

        UpdatePlayerHealth();
        UpdateMoney();

    }

    void UpdatePlayerHealth()
    {
        Player playerScript = player.GetComponent<Player>();

        RectTransform rt =  healthBar.GetComponent<RectTransform>();
        rt.localScale = new Vector3(playerScript.hitpoints / 100, rt.localScale.y, rt.localScale.z);

    }

    void UpdateMoney()
    {
        Player playerScript = player.GetComponent<Player>();
        money.text = playerScript.money.ToString();

    }

    IEnumerator SpawnEnemies()
    {
        int randomSpawn;

        yield return new WaitForSeconds(startWait);
        for (int i = 0; i < 2 * wave; i++)
        {
            spawnWait = Random.Range(minSpawnWait, maxSpawnWait);
            randomSpawn = Random.Range(0, 4);

            GameObject currEnemy = (GameObject) Instantiate(enemy, spawnList[randomSpawn], Quaternion.identity);
            currEnemy.GetComponent<Enemy>().target = player;
            yield return new WaitForSeconds(spawnWait);
                
        }
    }
}
