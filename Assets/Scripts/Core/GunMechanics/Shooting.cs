using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject Bullet;
    public Transform FirePoint;
    public AudioSource ShootSound;
    public float ShootCooldown;

    private float CurrentTime;
    private bool CanShoot = true;
    private Button ShootButton;

    private void Start()
    {
       // ShootButton = GameObject.Find("Shoot Button").GetComponent<Button>();
    }

    private void Update()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime >= ShootCooldown)
        {
            CanShoot = true;
            //DisableShootButton(false);
        }
    }

    public void Shoot()
    {
        if (CanShoot == true)
        {
            //DisableShootButton(true);
            CanShoot = false;
            CurrentTime = 0;

            if (ShootSound)
            {
                ShootSound.PlayOneShot(ShootSound.clip);
            }
            Instantiate(Bullet, FirePoint.position, Quaternion.identity);
        }
    }

    /*
     
    private void DisableShootButton(bool Status)
    {
        if (Status == true)
        {
            if (ShootButton != null)
            {
                ShootButton.enabled = false;
            }
        } else
        {
            ShootButton.enabled = true;
        }
    }
    */
}

