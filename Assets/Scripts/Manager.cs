using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public int levelCount;
    public Text coin;
    public Text distance;
    public Text recordDistance;
    public Camera camera;
    public GameObject gameOverUI;
    public GameObject setingsButton;
    public GameObject startButton;
    public GameObject backButton;
    public bool gameOverCheck = false;
    private int currentDistance = 0;
    private bool play = false;
    private int record;
    private int allCoins;
    private int firstStart;

    private static Manager instance;
    public static Manager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < levelCount; i++)
        {
            LevelGenerator.Instance.LevelRandom();
        }

        distance.gameObject.SetActive(true);
        GetRecord();
        GetCoins();
    }

    public bool CheckFisrtStartGame()
    {
        firstStart = PlayerPrefs.GetInt("firstStart");

        if (firstStart == 0)
        {
            PlayerPrefs.SetInt("firstStart", 1);
            return true;
        }
        else
            return false;
    }

    public void UpdateCoinCount(int value)
    {
        allCoins += value;
        coin.text = allCoins.ToString();
    }

    public void UpdateDistanceRecord()
    {
        if (currentDistance > record)
        {
            record = currentDistance;
            SaveRecord();
        }
    }

    public void UpdateDistanceCount()
    {
        currentDistance += 1;
        distance.text = currentDistance.ToString();

        if (currentDistance > 10)
            PoolObjectPlatform.Instance.DisablePooledObject();

        LevelGenerator.Instance.LevelRandom();
    }

    public bool CanPlay()
    {
        return play;
    }

    public void StartGame()
    {
        play = true;
        gameOverCheck = false;
        distance.gameObject.SetActive(true);
        setingsButton.SetActive(false);
        startButton.SetActive(false);
        recordDistance.gameObject.SetActive(false);
    }

    private void GetRecord()
    {
        record = PlayerPrefs.GetInt("recordDistance");
        recordDistance.text = "Record " + record;
    }

    private void GetCoins()
    {
        allCoins = PlayerPrefs.GetInt("coins");
        coin.text = allCoins.ToString();
    }

    public void SaveRecord()
    {
        PlayerPrefs.SetInt("recordDistance", record);
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("coins", allCoins);
    }

    public void GameOver()
    {
        if (!gameOverCheck)
        {
            camera.GetComponent<CameraFollow>().enabled = false;
            UpdateDistanceRecord();
            GetRecord();
            recordDistance.gameObject.SetActive(true);
            setingsButton.SetActive(true);
            startButton.SetActive(false);
            gameOverCheck = true;
            SaveCoins();
            UIGameOver();
        }
    }

    public void SettingsOpen()
    {
        if (gameOverCheck)
            gameOverUI.SetActive(false);
        else
            startButton.SetActive(false);

        AudioManager.Instance.soundToggle.gameObject.SetActive(true);
        setingsButton.SetActive(false);
        recordDistance.gameObject.SetActive(false);
        backButton.SetActive(true);
    }

    public void BackInMenu()
    {
        if (gameOverCheck)
            gameOverUI.SetActive(true);
        else
            startButton.SetActive(true);

        AudioManager.Instance.soundToggle.gameObject.SetActive(false);
        recordDistance.gameObject.SetActive(true);
        setingsButton.SetActive(true);
        backButton.SetActive(false);
    }

    private void UIGameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.Instance.AudioListenerOn();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
