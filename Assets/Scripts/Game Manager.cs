using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Dead
    }
    public static GameManager Instance;
    
    public float PlayStartTime;
    public TMP_Text scoreText;

    public int Lives = 3;

    [Header("References")]
    public GameObject IntroUI;
    public GameObject EnemySpawner;
    public GameState State = GameState.Playing;
    public GameObject ReturnButton;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    void Start()
    {
        State = GameState.Playing;
        PlayStartTime = Time.time;
        ReturnButton.SetActive(false);
    }

    // Update is called once per frameZ
    void Update()
    {
        if (State == GameState.Playing)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(CalculateScore());
            if (Lives <= 0)
            {
                SaveScore();
                State = GameState.Dead;
                Debug.Log("Player is dead");
            }
        }
        if (State == GameState.Dead)
        {
            ReturnButton.SetActive(true);
        }
    }

    float CalculateScore()
    {
        return Time.time - PlayStartTime;
    }

    void SaveScore() 
    {
        int currentScore = Mathf.FloorToInt(CalculateScore());
        int previousSumScore = PlayerPrefs.GetInt("sumscore", 0); // 기존 값 없으면 0

        int newSumScore = previousSumScore + currentScore;
        PlayerPrefs.SetInt("sumscore", newSumScore); // 저장
        PlayerPrefs.Save(); // 디스크에 강제 저장

        Debug.Log("총 점수 저장됨: " + newSumScore);
    }
}
