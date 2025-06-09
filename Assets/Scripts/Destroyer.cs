using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public int point = -15;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < point){
            Destroy(gameObject);
        }
    }
}
