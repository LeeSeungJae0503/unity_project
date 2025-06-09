using UnityEngine;

public class ScaleOscillator : MonoBehaviour
{
    public float scaleSpeed = 0.1f;
    public float maxScale = 5f;
    public float minScale = 4f;

    private bool shrinking = true;

    void Update()
    {
        Vector3 scale = transform.localScale;

        if (shrinking)
        {
            scale -= Vector3.one * scaleSpeed * Time.deltaTime;
            if (scale.x <= minScale)
            {
                scale = Vector3.one * minScale;
                shrinking = false;
            }
        }
        else
        {
            scale += Vector3.one * scaleSpeed * Time.deltaTime;
            if (scale.x >= maxScale)
            {
                scale = Vector3.one * maxScale;
                shrinking = true;
            }
        }

        transform.localScale = scale;
    }
}