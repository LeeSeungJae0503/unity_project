using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    public Button playGameButton;
    public Button tutorialButton;
    public Button customizeButton;
    public Button EXIT_button;

    public TMP_Text SavedScore;

    private void Start()
    {
        playGameButton.onClick.AddListener(OnPlayGameClicked);
        customizeButton.onClick.AddListener(OnCustomizeClicked);
        EXIT_button.onClick.AddListener(OnExitClicked);


        SavedScore.text = "" + GetSavedScore().ToString();
    }
    public void Update()
    {
        SavedScore.text = "" + GetSavedScore().ToString();
    }

    private void OnPlayGameClicked()
    {
        Debug.Log("Play Game 버튼 클릭됨");
        SceneManager.LoadScene("MainGame");
    }

    private void OnCustomizeClicked()
    {
        Debug.Log("Customize 버튼 클릭됨");
        SceneManager.LoadScene("CustomUI");
    }
    
    public void OnExitClicked()
    {
        Debug.Log("Exit 버튼 클릭됨");
        // SceneManager.LoadScene("CustomizeScene");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


    int GetSavedScore()
    {
        return PlayerPrefs.GetInt("sumscore", 0);
    }
}
