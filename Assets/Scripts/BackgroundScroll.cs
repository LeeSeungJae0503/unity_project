using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollspeed;
    [Header("Settings")]

    [Header("Reference")]
    public MeshRenderer meshRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(scrollspeed * Time.deltaTime, 0);
    }
}
