using TMPro;
using UnityEngine;

public class MainUimanager : MonoBehaviour
{
    public TMP_Text Score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "" + PlayerPrefs.GetInt("sumscore");
    }
}
