using UnityEngine;

public class RoketMover : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 3f;
    public GameObject Roket;
    public float yAmplitude = 1f;      // 위아래 진폭
    public float yFrequency = 1f;      // 위아래 속도

    private float startY;

    void Start()
    {
        startY = Roket.transform.position.y;
    }

    void Update()
    {
        // 좌로 이동
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // 위아래 진동 (sin 곡선으로)
        Vector3 pos = Roket.transform.position;
        pos.y = startY + Mathf.Sin(Time.time * yFrequency) * yAmplitude;
        Roket.transform.position = pos;
    }
}
