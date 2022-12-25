using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public int currentHealth;
    public int maxHealth = 3;
    public float speed = 2f;
    private float horizontal;
    private bool isFacingRight = true;
    private bool isDamageActive = true;
    private GameManager gameManager;
    private AudioSource audioSource;
    public AudioClip swordPickUpClip;
    public AudioClip damageSound;
    public AudioClip SwordPickUpMusic;
    
  
    
    public void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.position = new Vector2(-14f, -5f);
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        
    }

    
    void Update()
    {

        horizontal = Input.GetAxis("Horizontal");

        OnDeath();

        PlayerMovement();

        StartCoroutine(OffScreenDeath());
        
        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2 (horizontal * speed, rb.velocity.y);
    }


    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag == ("Enemy"))
        {
            audioSource.PlayOneShot(damageSound);
            TakeDamage(1);

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            audioSource.PlayOneShot(damageSound);
            TakeDamage(1);
        }

        if (collision.gameObject.tag == "Win")
        {
            Debug.Log("You Win!");
            SceneManager.LoadScene(0);
        }

        if (collision.gameObject.CompareTag("Sword"))
        {
            GodModePowerUp();
            Destroy(collision.gameObject);
            Debug.Log("GOD MODE ACTIVE FOR 5 SECONDS");
        }

        if (collision.gameObject.tag == "TimeBonus")
        {
            gameManager.timeRemaining = gameManager.timeRemaining + 10f;
            Destroy(collision.gameObject);
        }

    }


    void TakeDamage(int damage)
    {
        if (isDamageActive == true)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Max(0, currentHealth);
            
        }

    }

    void OnDeath()
    {
        if (currentHealth == 0)
        {
            Destroy(this.gameObject);
            
            SceneManager.LoadScene(2);
        }

        if (gameManager.timeRemaining == 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene(2);
        }
    }


    void PlayerMovement()
    {

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 6f);
        }

    }

    IEnumerator OffScreenDeath()
    {
        if (transform.position.y < -11)
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(1);
        }
        yield return null;
        
    }

    private void GodModePowerUp()
    {
        isDamageActive = false;
        audioSource.PlayOneShot(swordPickUpClip);
        audioSource.PlayOneShot(SwordPickUpMusic);
        gameManager.audioSource.Stop();
        Debug.Log("GOD MODE ACTIVE FOR 5 SECONDS");
        StartCoroutine(GodModePowerDown());
    }

    IEnumerator GodModePowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        audioSource.Stop();
        gameManager.audioSource.Play();
        isDamageActive = true;
    }
   
    
    
    

}
