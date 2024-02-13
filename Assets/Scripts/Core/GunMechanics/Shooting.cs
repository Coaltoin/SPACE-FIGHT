using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject Bullet;
    public Transform FirePoint;
    public float fireRate = 1;
    public AudioSource ShootSound;
    public float ShootCooldown;
    private float Shoot_CT;
    //private Button ShootButton;

    [Header("Tri-Shot Settings")]
    public Button TriShotButton;
    public AudioSource TriShotSound;
    private float TriShotCooldown = 5f;
    private float TriShot_CT;
    private float triAngle = 10f;

    private bool CanShoot = true;
    private bool CanTriShot = true;
    private bool TriShotActivated = false;

    ButtonManager buttonManager;

    private void Start()
    {
        // ShootButton = GameObject.Find("Shoot Button").GetComponent<Button>();
        buttonManager = GameObject.Find("GameManager").GetComponent<ButtonManager>();
    }

    private void Update()
    {
        CooldownHandler();
    }

    void CooldownHandler()
    {
        // Shooting
        Shoot_CT += Time.deltaTime * fireRate;
        if (Shoot_CT >= ShootCooldown)
        {
            CanShoot = true;
        }

        // Tri-Shot
        TriShot_CT += Time.deltaTime;
        if (TriShot_CT >= TriShotCooldown)
        {
            CanTriShot = true;
        }
    }

    public void Shoot()
    {
        if (CanShoot == true)
        {
            CanShoot = false;
            Shoot_CT = 0;

            if (ShootSound && TriShotActivated == false)
            {
                ShootSound.PlayOneShot(ShootSound.clip);
            } 
            Instantiate(Bullet, FirePoint.position, FirePoint.transform.rotation);

            if (TriShotActivated == true)
            {
                if (TriShotSound)
                {
                    TriShotSound.PlayOneShot(TriShotSound.clip);
                }

                float angle = FirePoint.transform.rotation.eulerAngles.z;
                float leftAngle = angle + triAngle;
                float rightAngle = angle - triAngle;

                //shoot the left side
                Quaternion leftRotation = Quaternion.Euler(0f, 0f, leftAngle);
                Instantiate(Bullet, FirePoint.transform.position, leftRotation);

                //shoot the right side
                Quaternion rightRotation = Quaternion.Euler(0f, 0f, rightAngle);
                Instantiate(Bullet, FirePoint.transform.position, rightRotation);

            }
        }
    }

    public void TriShot()
    {
        if (CanTriShot)
        {
            Debug.Log("TriShot Activated");
            TriShotActivated = true;
            CanTriShot = false;
            TriShot_CT = 0;
            if (buttonManager)
            {
                buttonManager.DisableButton(true, TriShotButton);
            }
            Invoke("UndoTriShot", TriShotCooldown);
        }
    }
    private void UndoTriShot()
    {
        Debug.Log("TriShot DeActivated");
        TriShotActivated = false;
        CanTriShot = false;
        TriShot_CT = 2; // 3 second extra cd (Total 5 second wait)
        if (buttonManager)
        {
            buttonManager.DisableButton(false, TriShotButton);
        }
    }

   
    
}

