using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip Stage1;
    public AudioClip Stage2;
    public AudioClip Stage3;
    private AudioSource audioSource;
    public GameObject point;

    private bool check1 = true;
    private bool check2 = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.05f;

        audioSource.clip = Stage1;
        audioSource.Play();
    }

    void Update()
    {
        if (point.transform.position.x > 40f && check1)
        {
            audioSource.clip = Stage2;
            audioSource.Play();
            check1 = false;
        }
        
        if (point.transform.position.x > 60f && check2)
        {
            audioSource.clip = Stage3;
            audioSource.Play();
            check2 = false;
        }
    }
}
