using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class MainGameUI : MonoBehaviour
{
    public Button ReturnButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ReturnButton.onClick.AddListener(ReturnClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ReturnClicked()
    {
        Debug.Log("return 버튼 클릭됨");
        SceneManager.LoadScene("MainUI");
    }
}
