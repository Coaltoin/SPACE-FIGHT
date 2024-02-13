using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed = 2f;
    private Transform player;
    private AudioSource deathsound;
    void Start()
    {
        deathsound = GameObject.Find("PlayerHit").GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();

            if (playerMovement.killImpact)
            {
                /*
                 
                if (deathsound)
                {
                    deathsound.PlayOneShot(deathsound.clip);
                }
                Destroy(this.gameObject);
                */
            }
            else if (!playerMovement.killImpact)
            {
                collision.gameObject.GetComponent<HealthManager>().TakeDamage(5);
                if (deathsound)
                {
                    deathsound.PlayOneShot(deathsound.clip);
                }
                Destroy(this.gameObject);
            }

        }
    }
}
