using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    public VariableJoystick mJoy;
    public VariableJoystick lJoy;
    public float moveForce;


    //shooting
    public GameObject bulletPoint, bullet;
    public float shootCD = 0;
    public AudioSource fire;
    public float xLook, yLook;
    //altshooting
    internal bool trishot = false;
    private float triAngle = 10f;

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
        if (xLook != 0 &&  yLook != 0)
        {
            transform.up = lookPos;
        }


        float xForce = mJoy.Horizontal;
        float yForce = mJoy.Vertical;

        Vector2 moveVector = new Vector2(xForce, yForce);
        //rb.AddForce(moveVector);
        //I'll figure out a better way for moving later. It's kinda rigid at the moment. ha. rigid.
        rb.velocity = moveVector * moveForce;


        //shoot bullets always
        Shoot();
    }

    public void Shoot()
    {
        shootCD += Time.deltaTime;
        if (shootCD > 0.5f && xLook != 0 && yLook != 0)
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
}
