using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectUpgrade : MonoBehaviour
{

    public float upgradeProg;
    public float scrapNeedToUpgrade, numUpgrade = 0;

    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Scrap")
        {
            upgradeProg += 1;
            CheckUpgrade();
            Destroy(collision.gameObject);
        }
    }
    


    public void CheckUpgrade()
    {
        if (upgradeProg >= scrapNeedToUpgrade) 
        { 
            upgradeProg = 0;
            scrapNeedToUpgrade = Mathf.Round(scrapNeedToUpgrade * 1.7f);
            Upgrade();
        }
    }


    public void Upgrade()
    {
        numUpgrade++;
        //impliment an actual upgrade system here
        switch(numUpgrade)
        {
            case 1:
                playerMovement.trishot = true;
                break;

            case 2:
                playerMovement.moveForce += 2;
                break;
        }
        

    }
}
