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
    
    private void Awake()
    {
        HitSoundEffect = GameObject.Find("EnemyHit").GetComponent<AudioSource>();
        currentTime = 0;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        transform.position = transform.position + (Vector3.up * Speed) * Time.deltaTime; // Moves the bullet upward at a given speed

        if (currentTime > MaxLifeTime) // Lifetime Condition
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.name);

        if (HitSoundEffect)
        {
            HitSoundEffect.Play();
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

}
