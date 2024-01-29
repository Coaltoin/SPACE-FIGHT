using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float Speed; 
    public float MaxLifeTime; // Time to wait until bullet is destroyed
    AudioSource HitSoundEffect;
    private float currentTime;
    public Rigidbody2D rb;

    public GameObject scrapToSpawn;
    
    private void Awake()
    {
        HitSoundEffect = GameObject.Find("EnemyHit").GetComponent<AudioSource>();
        currentTime = 0;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        //transform.position = transform.position + (Vector3.up * Speed) * Time.deltaTime; // Moves the bullet upward at a given speed
        rb.velocity = transform.up * Speed;

        if (currentTime > MaxLifeTime) // Lifetime Condition
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.name);



        if (collision.gameObject.tag == "Enemy") //IMPORTANT FOR FUTURE: Make it so enemies have health, and shooting them removes health. They destroy themselves once they reach 0.
        {
            //creates stuff where the enemy dies
            Instantiate(scrapToSpawn, collision.gameObject.transform.position, Quaternion.identity);


            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            if (HitSoundEffect)
            {
                HitSoundEffect.Play();
            }

        }
    }

}
