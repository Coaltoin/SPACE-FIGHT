using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    public VariableJoystick mJoy;
    public VariableJoystick lJoy;
    public float moveForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float xLook = lJoy.Horizontal;
        float yLook = lJoy.Vertical;
        Vector3 lookPos = new Vector3(xLook, yLook);
        //Vector3 currPos = new Vector3(transform.position.x, transform.position.y); 
        //transform.LookAt(transform.position + lookPos); BAD this is awful.
        if (xLook != 0 &&  yLook != 0)
        {
            transform.right = lookPos;
        }


        float xForce = mJoy.Horizontal;
        float yForce = mJoy.Vertical;

        Vector2 moveVector = new Vector2(xForce, yForce);
        //rb.AddForce(moveVector);
        //I'll figure out a better way for moving later. It's kinda rigid at the moment. ha. rigid.
        rb.velocity = moveVector * moveForce;
    }


}
