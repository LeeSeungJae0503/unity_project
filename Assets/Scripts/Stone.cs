using System;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public GameObject stonePrefab; // Stone 프리팹
    bool isActivated = true; // Stone 활성 여부
    void Update()
    {
        StoneDown();
        // Stone이 활성화된 상태에서 아래로 떨어지도록
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivated && collision.CompareTag("Player"))
        {
            // Player 충돌 시: Life 깎고, Stone 파괴
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.Hit();
            }
            Destroy(gameObject);
        }
    }

    void StoneDown()
    {
        if (stonePrefab.transform.position.y > -4f && isActivated)
        {
            stonePrefab.transform.Translate(Vector2.down * Time.deltaTime * 10f);
        }
    }
}