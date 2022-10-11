using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    private float spawnCD;
    [SerializeField] private float session, spawnCDTime;
    public static int score;
    [SerializeField] private GameObject chaser, shooter;
    [SerializeField] private Text sessionText;
    [SerializeField] private Text[] scores;
    public static bool survived;
    // Start is called before the first frame update
    void Start()
    {
        session = MenuManager.gameTime;
        spawnCDTime = MenuManager.spawnTime;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //PLAY AGAIN
        if(MenuManager.playAgain == true)
        {
            MenuManager.playAgain = false;
            session = MenuManager.gameTime;
            spawnCD = MenuManager.spawnTime;
            Spawn();
        }

        //SPAWN
        if (spawnCD > 0)
        {
            spawnCD -= Time.deltaTime;
        }
        if(spawnCD <= 0 && Player.isDead == false && session > 0)
        {
            Spawn();
        }

        // UI TEXT
        sessionText.text = "Time left: " + session.ToString("F2");

        for(int n = 0; n < scores.Length; n++)
        {
            scores[n].text = "Score: " + score.ToString();
        }

        //SESSION TIME
        if (session <= 0)
        {
            session = 0;
        }

        if (Player.isDead == false && session > 0)
        {
            session -= Time.deltaTime;
        }

        //WIN CONDITION
        else if (Player.isDead == false && session <= 0)
        {
            survived = true;
        }
    }

    private void Spawn()
    {
        int enemiesN = Random.Range(3, 6 + 1);
        for (int i = 0; i < enemiesN; i++)
        {
            int randomSpawn = Random.Range(0, spawnPoints.Length);
            int enemyType = Random.Range(1, 2 + 1);
            if(enemyType == 1)
            {
                Instantiate(chaser, spawnPoints[randomSpawn].position, transform.rotation);
            }
            else if (enemyType == 2)
            {
                Instantiate(shooter, spawnPoints[randomSpawn].position, transform.rotation);
            }
        }
        spawnCD = spawnCDTime;
    }
}
