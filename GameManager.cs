using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using LootLocker.Requests;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int score;
    private bool gameOver;
    private int best;
    public string gameMode;
    public TextMeshPro scoreText;
    // TODO
    // public TextMeshPro bestText;
    public TextMeshPro livesText;
    public GameObject timerText;
    public GameObject payloadText;
    private AudioSource source;
    public AudioClip loss;
    public AudioClip gameOverSound;
    public AudioClip point;
    public GameObject lightObject;
    public GameObject retry;
    private Light l;
    public int lives;
    public int leaderboardID;
    public string playerName;
    GameObject musicObj;

    void Awake() {
        Instance = this;
        gameOver = false;
        musicObj = GameObject.FindGameObjectWithTag("GameMusic");
        musicObj.GetComponent<AudioSource>().Stop();
    }
    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "";
        for (int i = 0; i < lives; i++)
        {
            livesText.text += "♥";
        }
        StartCoroutine(LoginRoutine());
        score = 0;
        // TODO
        // best = PlayerPrefs.GetInt(gameMode, 0);
        source = GetComponent<AudioSource>();
        l = lightObject.GetComponent<Light>();
        UpdateScore();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            LevelLoader.Instance.LoadNextLevel("MenuScene");
        }
        if(gameOver) {
            if(Input.GetKeyDown("space")) {
                LevelLoader.Instance.LoadNextLevel(SceneManager.GetActiveScene().name);
            } else if(Input.GetKeyDown(KeyCode.M)) {
                musicObj.GetComponent<AudioSource>().Play();
                LevelLoader.Instance.LoadNextLevel("MenuScene");
            }
        }
    }
    public void IncreaseScore() {
        if(gameOver) return;
        score++;
        source.PlayOneShot(point);
        Debug.Log("Booyah!");
        Debug.Log(score);
        if(score > best) {
            // PlayerPrefs.SetInt(gameMode, score);
            best = score;
        }
        UpdateScore();
    }

    public void LoseLife() {
        SetPlayerNameLocker();
        source.PlayOneShot(loss);
        if(livesText.text.Length > 0){
            livesText.text = livesText.text.Remove(livesText.text.Length-1);
        }
        if(--lives <= 0) {
            GameOver();
        }
    }

    private void GameOver() {
        timerText.SetActive(false);
        payloadText.SetActive(false);
        retry.SetActive(true);
        l.intensity = 0;
        scoreText.color = Color.black;
        // TODO
        // bestText.color = Color.black;
        source.Stop();
        source.PlayOneShot(gameOverSound);
        gameOver = true;
        StartCoroutine(DieRoutine());
    }
    public bool GetGameOver() {
        return gameOver;
    }

    private void UpdateScore() {
        scoreText.text = score.ToString();
        // bestText.text = "best: " + best.ToString();
    }

    private IEnumerator LoginRoutine()
    {
        
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response)=>
        {
            if(response.success) {
                Debug.Log("Logged in!!!");
                // TODO
                // PlayerPrefs.SetString("hiScoreID", response.player_id.ToString());
                done = true;
            } else {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    private IEnumerator SubmitScoreRoutine(int scoreToUpload) {
        bool done = false;
        // TODO
        // string id = PlayerPrefs.GetString("hiScoreID");
        string id = playerName;
        LootLockerSDKManager.SubmitScore(id, scoreToUpload, leaderboardID, (response) =>
        {
            if(response.success) {
                Debug.Log("Successfully uploaded score!");
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
    
    private IEnumerator DieRoutine() {
        yield return SubmitScoreRoutine(score);
    }

    private void SetPlayerNameLocker() {
        // LootLockerSDKManager.SetPlayerName(PlayerPrefs.GetString("playerID"), (response) => {
        LootLockerSDKManager.SetPlayerName(playerName, (response) => {
            if(response.success) {
                Debug.Log("Successfully set player name");
            }
            else {
                Debug.Log("Failed," + response.Error);
            }
        });
    }
}
