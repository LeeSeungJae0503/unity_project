using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Player : MonoBehaviour
{
    public static Player player;
    [Header("Settings")]
    public float JumpForce;
    [Header("Reference")]
    public Rigidbody2D PlayerRigidBody;
    int cnt = 0;

    private bool Grounded = true;

    public CapsuleCollider2D PlayerCollider;

    private bool isInvincible = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Grounded && cnt < 2){
            PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
            cnt++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Road")
        {
            Grounded = true;
            cnt = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "enemy")
        {
            if(!isInvincible)
            {
                Destroy(collider.gameObject);
            }
            Hit();
        }
    }

    void KillPlayer()
    {
        PlayerCollider.enabled = false;
        PlayerCollider.enabled = false;
        PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
    }
    
    public void Hit()
    {
        GameManager.Instance.Lives -= 1;

        if (GameManager.Instance.Lives == 0)
        {
            KillPlayer();
        }
    }

    /*void StartInvincible()
    {
        isInvincible = true;
        Invoke("StopInvincible", 5f);
    }*/

    /*void StopInvincible()
    {
        isInvincible = false;
    }*/
}