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
    
    private GameManager gameManager;
    
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
      //  print(collision.gameObject.name);

        if (collision.gameObject.tag == "Enemy") 
        {
            //creates stuff where the enemy dies
            Instantiate(scrapToSpawn, collision.gameObject.transform.position, Quaternion.identity);

            if (gameManager)
            {
                gameManager.AddCurrentAmount(1); // Player has defeated an enemy, so add to the current amount to get closer to win condition
            }

            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            if (HitSoundEffect)
            {
                HitSoundEffect.Play();
            }

        }
    }

   

}
