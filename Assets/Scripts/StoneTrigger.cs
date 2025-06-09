using UnityEngine;

public class Stone1 : MonoBehaviour
{
    public GameObject stonePrefab;
    public Transform spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void SpawnStone()
    {
        Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : transform.position;
        Instantiate(stonePrefab, spawnPosition, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Trigger")
        {
            Debug.Log("Trigger");
            SpawnStone();
        }
    }
}
