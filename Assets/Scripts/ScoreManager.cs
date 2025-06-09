using System.Net.Sockets;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameManager gameManager;
    public float Score = 0f;
    public float ScorePerSecond = 10f;

    public Text scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager != null && gameManager.State == GameManager.GameState.Playing)
        {
            Score += ScorePerSecond * Time.deltaTime;
        }
    }
}
