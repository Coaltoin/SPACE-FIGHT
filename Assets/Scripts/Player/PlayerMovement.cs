using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    public VariableJoystick mJoy;
    public VariableJoystick lJoy;
    public float moveForce, moveAmp = 1;


    //shooting
    public GameObject bulletPoint, bullet;
    public float shootCD, fireRate = 1, thrustCD, triShotCD, cdAmp = 1;
    public AudioSource fire;
    public float xLook, yLook;
    //altshooting
    internal bool trishot = false, fullThrust = false, canLook = true;
    internal float tempSpeed;
    private float triAngle = 10f;

    private float boostTime = 0;
    public Vector3 boostDir;

    public int health = 5;
    public bool killImpact = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        xLook = lJoy.Horizontal;
        yLook = lJoy.Vertical;
        Vector3 lookPos = new Vector3(xLook, yLook);
        //Vector3 currPos = new Vector3(transform.position.x, transform.position.y); 
        //transform.LookAt(transform.position + lookPos); BAD this is awful.
        if (xLook != 0 && yLook != 0 && canLook)
        {
            transform.up = lookPos;
        }


        float xForce = mJoy.Horizontal;
        float yForce = mJoy.Vertical;
        Vector3 moveVector = new Vector3(xForce, yForce);


        /*if (xForce != 0 && yForce != 0 && canLook)
        {
            transform.up = moveVector;

        }*/
        if (canLook)
        {
            rb.velocity = moveVector * moveForce * moveAmp;
        }

        //rb.AddForce(moveVector);
        //I'll figure out a better way for moving later. It's kinda rigid at the moment. ha. rigid.


        shootCD += Time.deltaTime * fireRate;
        thrustCD += Time.deltaTime * cdAmp;
        triShotCD += Time.deltaTime * cdAmp;
        //shoot bullets always
        Shoot();

        if (fullThrust)
        {
            Thrusting(xLook, yLook);
        }

    }

    public void Shoot()
    {

        if (shootCD > 1.0f && xLook != 0 && yLook != 0)
        //if (shootCD > 1.0f)
        {
            Instantiate(bullet, bulletPoint.transform.position, bulletPoint.transform.rotation);
            shootCD = 0;
            fire.Play();
            
           
            
            //trishot
            if (trishot == true)
            {
                
                float angle = bulletPoint.transform.rotation.eulerAngles.z;
                float leftAngle = angle + triAngle;
                float rightAngle = angle - triAngle;

                //shoot the left side
                Quaternion leftRotation = Quaternion.Euler(0f, 0f, leftAngle);
                Instantiate(bullet, bulletPoint.transform.position, leftRotation);

                //shoot the right side
                Quaternion rightRotation = Quaternion.Euler(0f, 0f, rightAngle);
                Instantiate(bullet, bulletPoint.transform.position, rightRotation);

            }
        }
        
    }




    public void DoTriShot()
    {
        if (triShotCD > 20.0f)
        {
            trishot = true;
            Invoke("UndoTriShot", 5f);
        }

    }
    private void UndoTriShot()
    {
        trishot = false;
    }



    public void DoFullThrust()
    {
        if (thrustCD > 20.0f)
        {
            fullThrust = true;
            tempSpeed = moveForce;
            //Invoke("UndoFullThrust", 5f);
        }


    }
    private void UndoFullThrust()
    {
        fullThrust = false;
    }

    void Thrusting(float xF, float yF)
    {
        if (moveForce > 0f)
        {
            moveForce -= Time.deltaTime * 1.5f;
            //you can set to play a charging sound here
        }
        else
        {
            moveForce = 0f;
            if (boostTime <= 0.8f)
            {
                killImpact = true;
                if (boostDir.x == 0 && boostDir.y == 0)
                {
                    boostDir = new Vector3(xF, yF);
                }

                canLook = false;
                boostTime += Time.deltaTime;

                rb.velocity = boostDir * 10;

            }
            else
            {
                UndoFullThrust();
                moveForce = tempSpeed;
                canLook = true;
                killImpact = false;
            }    

        }

    }



}
