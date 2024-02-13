using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    Rigidbody2D rb;
    public VariableJoystick mJoy;
    public VariableJoystick lJoy;
    public float moveForce, moveAmp = 1;
    public float fullThrustCD = 5f;
    public float xLook, yLook;

    [Header("Full Thrust Settings")]
    public float thrustCD, cdAmp = 1;
    private bool CanFullThurst;
    internal bool fullThrust = false, canLook = true, boosting = false;
    internal float tempSpeed;
    private float boostTime = 0;
    public Vector3 boostDir;
    public bool killImpact = false;

    
    private Shooting shooting;
    ButtonManager buttonManager;
    public GameObject scrapToSpawn;
    private GameManager gameManager;
    AudioSource HitSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shooting = GameObject.Find("GameManager").GetComponent<Shooting>();
        buttonManager = GameObject.Find("GameManager").GetComponent<ButtonManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        HitSoundEffect = GameObject.Find("EnemyHit").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        thrustCD += Time.deltaTime * cdAmp;

        if (thrustCD >= fullThrustCD)
        {
            CanFullThurst = true;
        }

      //  Debug.Log(thrustCD);

        xLook = lJoy.Horizontal;
        yLook = lJoy.Vertical;

        Vector3 lookPos = new Vector3(xLook, yLook);
        if (xLook != 0 && yLook != 0 && canLook)
        {
            transform.up = lookPos;
        }

        float xForce = mJoy.Horizontal;
        float yForce = mJoy.Vertical;
        Vector3 moveVector = new Vector3(xForce, yForce);

        if (canLook)
        {
            rb.velocity = (moveVector * moveForce * moveAmp);
        }

        if (xLook != 0 && yLook != 0)
        {
            shooting.Shoot();
        }

        if (fullThrust)
        {
            Thrusting(xLook, yLook);
        }
    }

    public void DoFullThrust()
    {
        if (thrustCD >= fullThrustCD && CanFullThurst)
        {
            thrustCD = 0;
            CanFullThurst = false;
            Debug.Log("THRUST TIME!!");
            buttonManager.DisableButton(true, GameObject.Find("Full Thrust").GetComponent<Button>());
            fullThrust = true;
            tempSpeed = moveForce;
            Invoke("UndoThrustButton", fullThrustCD);
        } else
        {
            Debug.Log("Thrust CD Not over");
        }
    }
    private void UndoFullThrust()
    {
        fullThrust = false;  
    }

    private void UndoThrustButton()
    {
        buttonManager.DisableButton(false, GameObject.Find("Full Thrust").GetComponent<Button>());
    }
   

    void Thrusting(float xF, float yF)
    {
        if (moveForce > 0f)
        {
            moveForce -= Time.deltaTime * 2.5f;
            //you can set to play a charging sound here
           // Debug.Log("Charging");
            boostTime = 0;
            boosting = false;
            killImpact = true; // immune to death while charging
        }
        else
        {
            moveForce = 0f;
            if (boostTime <= 0.8f)
            {
                boosting = true;
               // Debug.Log("BOOST!!");
                if (boostDir.x == 0 && boostDir.y == 0)
                {
                    boostDir = new Vector3(xF, yF);
                }

                canLook = false;
                boostTime += Time.deltaTime;

                // rb.velocity = boostDir * 100;

                float xForce = mJoy.Horizontal;
                float yForce = mJoy.Vertical;
                Vector3 moveVector = new Vector3(xForce, yForce);
                rb.velocity = (moveVector * 10);
            }
            else
            {
                UndoFullThrust();
                moveForce = 3.5f;
                canLook = true;
                boosting = false;
                killImpact = false;
            }    

        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (boosting == true)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                //creates stuff where the enemy dies
                Instantiate(scrapToSpawn, collision.gameObject.transform.position, Quaternion.identity);

                if (gameManager)
                {
                    gameManager.AddCurrentAmount(1); // Player has defeated an enemy, so add to the current amount to get closer to win condition
                }

                Destroy(collision.gameObject);
                if (HitSoundEffect)
                {
                    HitSoundEffect.PlayOneShot(HitSoundEffect.clip);
                }

            }
        }
    }
}
