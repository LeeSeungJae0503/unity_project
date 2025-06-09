using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomUI : MonoBehaviour
{

    [SerializeField] Color32 LockedColor = new Color32(1, 1, 1, 1); // 잠금 상태일 때의 색상
    [SerializeField] Color32 UnlockedColor = new Color32(255, 255, 255, 255); // 잠금 해제 상태일 때의 색상
    public Button ReturnButton;
    public Button player1;
    public Button player2;
    public Button player3;
    public Button player4;
    public Button player5;
    public Button player6;

    public TMP_Text SavedMoney; // 현재 저장된 재화 가치를 표시하는 텍스트

    private bool c1;
    private bool c2;
    private bool c3;
    private bool c4;
    private bool c5;
    private bool c6;

    public GameObject playerCheck1;
    public GameObject playerCheck2;
    public GameObject playerCheck3;
    public GameObject playerCheck4;
    public GameObject playerCheck5;
    public GameObject playerCheck6;

    public GameObject playerLock1;
    public GameObject playerLock2;
    public GameObject playerLock3;
    public GameObject playerLock4;
    public GameObject playerLock5;
    public GameObject playerLock6;

    [SerializeField] TMP_Text player1Pricel;
    [SerializeField] TMP_Text player2Pricel;
    [SerializeField] TMP_Text player3Pricel;
    [SerializeField] TMP_Text player4Pricel;
    [SerializeField] TMP_Text player5Pricel;

    public string playernum; // 현재 선택된 플레이어의 키값, 기본값은 cc1
    // c1, c2, c3, c4는 각각 플레이어 1, 2, 3, 4의 구매 여부

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        PlayerPrefsLoad(); // PlayerPrefs에서 저장된 값 불러오기
        ReturnButton.onClick.AddListener(OnReturnButton); // Return 버튼 클릭 시 호출되는 함수 등록
        player1.onClick.AddListener(OnPlayer1Clicked); // 1번 플레이어 버튼 클릭 시 호출되는 함수 등록
        player2.onClick.AddListener(OnPlayer2Clicked); // 2번 플레이어 버튼 클릭 시 호출되는 함수 등록
        player3.onClick.AddListener(OnPlayer3Clicked); // 3번 플레이어 버튼 클릭 시 호출되는 함수 등록
        player4.onClick.AddListener(OnPlayer4Clicked); // 4번 플레이어 버튼 클릭 시 호출되는 함수 등록
        player5.onClick.AddListener(OnPlayer5Clicked); // 5번 플레이어 버튼 클릭 시 호출되는 함수 등록
        player6.onClick.AddListener(OnPlayer6Clicked); // 6번 플레이어 버튼 클릭 시 호출되는 함수 등록

        UIUpdate(); // UI 업데이트 함수 호출
        Debug.Log("CustomUI 스크립트 시작됨");
    }

    void UIUpdate()
    {
        check(); // 현재 선택된 플레이어의 UI 업데이트
        Lock(); // 플레이어의 구매 여부에 따라 잠금 상태 UI 업데이트
        Gray(); // 플레이어 버튼의 색상 업데이트
        DisappearPrice(); // 현재 선택된 플레이어의 구매 여부에 따라 버튼 색상 업데이트
        MoneyUpdate(); // 현재 저장된 재화 가치 표시
    }

    void Update()
    {
        SavedMoney.text = PlayerPrefs.GetInt("sumscore", 0).ToString(); // 현재 저장된 재화 가치 표시
    }

    void OnReturnButton()
    {
        Debug.Log("Return 버튼 클릭됨");
        SceneManager.LoadScene("MainUI");
    }
    void OnPlayer1Clicked()
    {
        int value = 0;
        int key = 0;
        string boolKey = "cc1";
        // 플레이어의 구매 여부 확인
        CheckBought(value, c1, key, boolKey);
    }
    void OnPlayer2Clicked()
    {
        int value = 300;
        int key = 1;
        string boolKey = "cc2";
        CheckBought(value, c2, key, boolKey);
    }
    void OnPlayer3Clicked()
    {
        int value = 400;
        int key = 2;
        string boolKey = "cc3";
        CheckBought(value, c3, key, boolKey);
    }
    void OnPlayer4Clicked()
    {
        int value = 500;
        int key = 3;
        string boolKey = "cc4";
        CheckBought(value, c4, key, boolKey);
    }
    void OnPlayer5Clicked()
    {
        int value = 600;
        int key = 4;
        string boolKey = "cc5";
        CheckBought(value, c5, key, boolKey);
    }
    void OnPlayer6Clicked()
    {
        int value = 700;
        int key = 5;
        string boolKey = "cc6";
        CheckBought(value, c6, key, boolKey);
    }

    void buy(int value, string boolKey)
    {
        int money = PlayerPrefs.GetInt("sumscore", 0); //현재 저장된 재화 가치 불러오기
        if (money - value >= 0)
        {
            PlayerPrefs.SetInt("sumscore", money - value); //재화 가치 업데이트
            PlayerPrefs.Save(); //저장
            switch (boolKey)
            {
                case "cc1": c1 = true; break;
                case "cc2": c2 = true; break;
                case "cc3": c3 = true; break;
                case "cc4": c4 = true; break;
                case "cc5": c5 = true; break;
                case "cc6": c6 = true; break;
            }
            Debug.Log("재화 업데이트 및 구매 완료");
        }
        else
        {
            Debug.Log("재화 부족으로 구매 실패");
        }
    }

    void CheckBought(int value, bool check, int Key, string boolKey)
    {
        if (check)
        {
            PlayerPrefs.SetInt("key", Key); // 구매한 아이템의 키값 저장장
            PlayerPrefs.SetString("playernum", boolKey); // 현재 선택된 플레이어의 키값 저장
            playernum = boolKey; // 현재 선택된 플레이어의 키값 업데이트
            PlayerPrefs.SetInt(boolKey, 1); // 구매 여부를 PlayerPrefs에 저장
            PlayerPrefs.Save(); // PlayerPrefs 저장
            Debug.Log("이미 구매한 아이템" + Key + "번 플레이어 선택됨됨");
            UIUpdate();
        }
        else
        {
            Debug.Log("구매 완료 또는 구매 실패");
            buy(value, boolKey); // 아이템 구매 함수 호출
            UIUpdate(); // UI 업데이트 함수 호출
        }
    }

    void PlayerPrefsLoad()
    {
        // PlayerPrefs에서 저장된 값 불러오기
        c1 = System.Convert.ToBoolean(PlayerPrefs.GetInt("cc1", 0));
        c2 = System.Convert.ToBoolean(PlayerPrefs.GetInt("cc2", 0));
        c3 = System.Convert.ToBoolean(PlayerPrefs.GetInt("cc3", 0));
        c4 = System.Convert.ToBoolean(PlayerPrefs.GetInt("cc4", 0));
        c5 = System.Convert.ToBoolean(PlayerPrefs.GetInt("cc5", 0));
        c6 = System.Convert.ToBoolean(PlayerPrefs.GetInt("cc6", 0));
        playernum = PlayerPrefs.GetString("playernum", "cc1"); // 기본값은 cc1

        if (c1 == false && c2 == false && c3 == false && c4 == false && c5 == false && c6 == false)
        {
            // 만약 모든 플레이어가 구매되지 않았다면 기본값으로 cc1 설정
            playernum = "cc1";
            PlayerPrefs.SetString("playernum", playernum); // PlayerPrefs에 저장
            PlayerPrefs.SetInt("key", 0); // 기본 플레이어의 키값 저장
            PlayerPrefs.Save(); // PlayerPrefs 저장
            Debug.Log("모든 플레이어가 구매되지 않았으므로 기본값 cc1로 설정");
            c1 = true; // 기본 플레이어 구매 상태를 true로 설정
            PlayerPrefs.SetInt("cc1", 1); // cc1 구매 상태 저장
            check(); // UI 업데이트
        }
    }

    void check()
    {
        switch (playernum)
        {
            case "cc1":
                playerCheck1.SetActive(true);
                playerCheck2.SetActive(false);
                playerCheck3.SetActive(false);
                playerCheck4.SetActive(false);
                playerCheck5.SetActive(false);
                playerCheck6.SetActive(false);
                break;
            case "cc2":
                playerCheck1.SetActive(false);
                playerCheck2.SetActive(true);
                playerCheck3.SetActive(false);
                playerCheck4.SetActive(false);
                playerCheck5.SetActive(false);
                playerCheck6.SetActive(false);
                break;
            case "cc3":
                playerCheck1.SetActive(false);
                playerCheck2.SetActive(false);
                playerCheck3.SetActive(true);
                playerCheck4.SetActive(false);
                playerCheck5.SetActive(false);
                playerCheck6.SetActive(false);
                break;
            case "cc4":
                playerCheck1.SetActive(false);
                playerCheck2.SetActive(false);
                playerCheck3.SetActive(false);
                playerCheck4.SetActive(true);
                playerCheck5.SetActive(false);
                playerCheck6.SetActive(false);
                break;
            case "cc5":
                playerCheck1.SetActive(false);
                playerCheck2.SetActive(false);
                playerCheck3.SetActive(false);
                playerCheck4.SetActive(false);
                playerCheck5.SetActive(true);
                playerCheck6.SetActive(false);
                break;
            case "cc6":
                playerCheck1.SetActive(false);
                playerCheck2.SetActive(false);
                playerCheck3.SetActive(false);
                playerCheck4.SetActive(false);
                playerCheck5.SetActive(false);
                playerCheck6.SetActive(true);
                break;
        }
    }

    void Lock()
    {
        // 플레이어의 구매 여부에 따라 잠금 상태 UI 업데이트
        playerLock1.SetActive(!c1);
        playerLock2.SetActive(!c2);
        playerLock3.SetActive(!c3);
        playerLock4.SetActive(!c4);
        playerLock5.SetActive(!c5);
        playerLock6.SetActive(!c6);
    }

    void Gray()
    {
        // 플레이어 버튼의 색상을 잠금 상태에 따라 업데이트
        SetButtonColor(player1, c1);
        SetButtonColor(player2, c2);
        SetButtonColor(player3, c3);
        SetButtonColor(player4, c4);
        SetButtonColor(player5, c5);
        SetButtonColor(player6, c6);
    }
    void SetButtonColor(Button button, bool isUnLocked)
    {
        button.GetComponent<Image>().color = isUnLocked ? UnlockedColor : LockedColor; // 버튼의 색상을 잠금 상태에 따라 업데이트  
    }

    void DisappearPrice()
    {
        // 플레이어의 구매 여부에 따라 가격 텍스트 비활성화
        player1Pricel.gameObject.SetActive(!c2);
        player2Pricel.gameObject.SetActive(!c3);
        player3Pricel.gameObject.SetActive(!c4);
        player4Pricel.gameObject.SetActive(!c5);
        player5Pricel.gameObject.SetActive(!c6);
    }

    void MoneyUpdate()
    {
        SavedMoney.text = PlayerPrefs.GetInt("sumscore", 0).ToString();
    }
}