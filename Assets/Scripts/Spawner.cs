using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    public float minSpawnDelay;
    public float maxSpawnDelay;
    [Header("References")]
    public GameObject[] gameObjects;

    public bool Check = true;
    private float spawnTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        spawnTimer = Random.Range(minSpawnDelay, maxSpawnDelay);
    }
    void Update()
    {
        if (!Check) return;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            Spawn();
            spawnTimer = Random.Range(minSpawnDelay, maxSpawnDelay);
        }
    }

    // Update is called once per frame
    void Spawn() {
        if (!Check) return;

        GameObject randomObject = null;

        // 무효화된 오브젝트가 없도록 반복해서 찾기
        for (int i = 0; i < gameObjects.Length; i++) {
            int randIndex = Random.Range(0, gameObjects.Length);
            if (gameObjects[randIndex] != null) {
                randomObject = gameObjects[randIndex];
                break;
            }
        }

        if (randomObject == null) {
            Debug.LogWarning("Spawn 실패: gameObjects 배열에 유효한 오브젝트가 없습니다.");
            return;
        }
        Instantiate(randomObject, transform.position, Quaternion.identity);
    }
}
