using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private bool stopSpawning = false;
    public AudioSource audioSource;
    public float timeRemaining = 60f;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public GameObject timeBonusPrefab;
    public Image livesImage3, livesImage2, livesImage1;
    private Player player;
     


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        
        livesImage1.enabled = true;
        livesImage2.enabled = true;
        livesImage3.enabled = true;
        player = GameObject.Find("HeroKnight").GetComponent<Player>();
        timerIsRunning = true;
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(EnemySpawn());
        StartCoroutine(TimeBonusSpawn());
        
    }

    void Update()
    {

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
             
        
        if (player.currentHealth == 0 || timeRemaining == 0)
        {
            StopAllCoroutines();
        }
        

        DisplayTime(timeRemaining);
        LivesDisplay();

    }


    IEnumerator EnemySpawn()
    {
        while (stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(-14.6f, 8f, 0);
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }

    }

    IEnumerator TimeBonusSpawn()
    { 
        while (stopSpawning == false)
        {
            Vector2 spawnPos2 = new Vector2(Random.Range(12f, -17f), 0);
            Destroy(Instantiate(timeBonusPrefab, spawnPos2, Quaternion.identity), 5f);
            yield return new WaitForSeconds(8.0f);
        }
    }

    

    void DisplayTime(float timeToDispaly)
    {
        
        float minutes = Mathf.FloorToInt(timeToDispaly / 60);
        float seconds = Mathf.FloorToInt(timeToDispaly % 60);
        
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timeRemaining >= 0)
        {
            if (PlayerPrefs.GetFloat("TimeRemaining") > timeRemaining)
            {
                PlayerPrefs.SetFloat("TimeRemaining", timeRemaining);
            }
        }
    }


    public void LivesDisplay()
    {
        if (player.currentHealth == 2)
        {
            livesImage1.enabled = false;
        }

        if (player.currentHealth == 1)
        {
            livesImage2.enabled = false;
        }
    }

    

    
}
