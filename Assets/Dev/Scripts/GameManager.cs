using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {

    public GameObject player;
    public GameObject enemy;
    public GameObject healthBar;
    public Text waveText;
    public Text money;
    public GameObject gameScreen;
    public GameObject gameOverScreen;
    public Text gameOverText;
    public float gameOverDelay;

    List<Vector3> spawnList = new List<Vector3>();

    float wave;
    bool startWave;
    bool waitingForWave;
    public float enemyCounter;
    float startWait;
    float spawnWait;
    public float minSpawnWait;
    public float maxSpawnWait;

    int enemyHitpoints;
    float enemySpeed;
    int enemyDamage;

	// Use this for initialization
	void Start () {
        wave = 1;
        waveText.text = "Wave " + wave.ToString();
        startWave = true;
        waitingForWave = false;
        enemyHitpoints = 100;
        enemySpeed = 3;
        enemyDamage = 10;

        startWait = 1;
             
        spawnList.Add(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, 0, 10)));
        spawnList.Add(Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height/2, 10)));
        spawnList.Add(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 10)));
        spawnList.Add(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height/2, 10)));
    }
	
	// Update is called once per frame
	void Update () {

        UpdatePlayerHealth();
        UpdateMoney();
        if (startWave == true)
        {
            startWave = false;
            enemyCounter = wave * 2;
            StartCoroutine(SpawnEnemies());
        }

        if(enemyCounter == 0 && waitingForWave == false)
        {
            waitingForWave = true;
            StartCoroutine(DelayWaves());
        }

        if (player.GetComponent<Player>().gameOver == true)
        {
            gameScreen.SetActive(false);
            StartCoroutine(DelayGameOver());
        }
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
        for (int i = 0; i < enemyCounter; i++)
        {
            spawnWait = Random.Range(minSpawnWait, maxSpawnWait);
            randomSpawn = Random.Range(0, 4);

            GameObject currEnemy = (GameObject)Instantiate(enemy, spawnList[randomSpawn], Quaternion.identity);
            Enemy enemyScript = currEnemy.GetComponent<Enemy>();

            enemyScript.hitpoints = enemyHitpoints;
            enemyScript.speed = enemySpeed;
            enemyScript.GetComponentInChildren<Claw>().damage = enemyDamage;
            enemyScript.target = player;
            enemyScript.manager = this.gameObject;
            yield return new WaitForSeconds(spawnWait);
                
        }
    }

    IEnumerator DelayWaves()
    {
        yield return new WaitForSeconds(3);
        wave++;
        waveText.text = "Wave " + wave.ToString();
        enemyHitpoints += 50;
        enemySpeed += 0.01f;
        enemyDamage += 1;
        startWave = true;
        waitingForWave = false;
    }

    IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(1);
        gameScreen.SetActive(false);
        gameOverScreen.SetActive(true);
        if (wave != 2)
        {
            gameOverText.text = "YOU SURVIVED " + (wave - 1) + " DAYS";
        }
        else
        {
            gameOverText.text = "YOU SURVIVED " + (wave - 1) + " DAY";
        }
    }
}
