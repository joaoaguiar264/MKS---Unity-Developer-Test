using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuUI, mainMenuUI, sessionUI, optionsUI, gameOverUI, survivedUI;
    [SerializeField] private GameObject session;

    [SerializeField] private Slider gameTimeSlider;
    [SerializeField] private Text slider1Text;

    [SerializeField] private Slider spawnTimeSlider;
    [SerializeField] private Text slider2Text;

    public static float gameTime = 60, spawnTime = 10;
    public static bool playAgain = false;
    // Start is called before the first frame update
    void Start()
    {
        //RESET
        Player.isDead = false;
        Player.health = 3;
        GameManager.survived = false;
        GameManager.score = 0;

        //UI TEXT
        gameTimeSlider.onValueChanged.AddListener((v) =>
        {
            gameTime = v * 60;
            slider1Text.text = v.ToString() + " Minute(s)";
        });

        spawnTimeSlider.onValueChanged.AddListener((v) =>
        {
            spawnTime = v;
            slider2Text.text = v.ToString() + " Seconds";
        });
    }

    // Update is called once per frame
    void Update()
    {
        //GAME OVER CONDITION
        if (Player.isDead == true)
        {
            GameOver();
        }

        if(GameManager.survived == true)
        {
            Survived();
        }

        
    }
    public void Play()
    {
        optionsUI.SetActive(false);
        mainMenuUI.SetActive(false);
        menuUI.SetActive(false);
        sessionUI.SetActive(true);
        session.SetActive(true);
    }

    public void Options()
    {
        mainMenuUI.SetActive(false);
        optionsUI.SetActive(true);
    }
    public void Survived()
    {
        menuUI.SetActive(true);
        survivedUI.SetActive(true);
    }

    public void GameOver()
    {
        menuUI.SetActive(true);
        gameOverUI.SetActive(true);
    }

    public void PlayAgain()
    {
        playAgain = true;
        Player.isDead = false;
        Player.health = 3;
        GameManager.survived = false;
        GameManager.score = 0;

        menuUI.SetActive(false);
        gameOverUI.SetActive(false);
        survivedUI.SetActive(false);

        GameObject[] chasers = GameObject.FindGameObjectsWithTag("ChaserObj");
        foreach (GameObject chaser in chasers)
        {
            GameObject.Destroy(chaser);
        }

        GameObject[] shooters = GameObject.FindGameObjectsWithTag("ShooterObj");
        foreach (GameObject shooter in shooters)
        {
            GameObject.Destroy(shooter);
        }


    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
