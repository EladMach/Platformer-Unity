using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed;
    public bool moveRight;
    

    void Update()
    {
        if (moveRight)
        {
            transform.Translate(3 * Time.deltaTime * speed, 0, 0);
        }
        else
        {
            transform.Translate(-3 * Time.deltaTime * speed, 0, 0);
        }   
        
        if (transform.position.y < -11f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);   
        }

    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Turn"))
        {
            if (moveRight)
            {
                moveRight = false;
            }
            else
            {
                moveRight =  true;
            }
        }
    }
}
