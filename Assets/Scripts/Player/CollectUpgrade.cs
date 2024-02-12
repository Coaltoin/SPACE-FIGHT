using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectUpgrade : MonoBehaviour
{

    public float upgradeProg, uiSpacing = 250f, score = 0;
    public float scrapNeedToUpgrade, numUpgrade = 0;



    public PlayerMovement playerMovement;
    public GameObject canvas, uiBlocker, scoreHolder, fullThrustButton, triShotButton;

    [Header("Upgrades")]
    public List<GameObject> upgradeOptionsCom = new List<GameObject>();
    public List<GameObject> revealedOptions = new List<GameObject>();
    

    // Start is called before the first frame update
    void Start()
    {

        playerMovement = GetComponent<PlayerMovement>();
        Object[] upgradeLoaded = Resources.LoadAll("Upgrades", typeof(GameObject));

        foreach (GameObject prefab in upgradeLoaded)
        {
            upgradeOptionsCom.Add(prefab);
        }


    }

    // Update is called once per frame
    void Update()
    {

        //testing
        if (Input.GetKeyDown(KeyCode.V))
        {
            Upgrade();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Scrap")
        {
            score += 100;
            scoreHolder.GetComponent<TMPro.TextMeshProUGUI>().text = "Score : " + score;
            
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
        Time.timeScale = 0.0f;
        uiBlocker.SetActive(true);

        //Spawn em. 301.055y.
        int upgrade1 = Random.Range(0, upgradeOptionsCom.Count);
        int upgrade2 = Random.Range(0, upgradeOptionsCom.Count);
        int upgrade3 = Random.Range(0, upgradeOptionsCom.Count);

        while (upgrade2 == upgrade1)
        {
            upgrade2 = Random.Range(0, upgradeOptionsCom.Count);
        }
        while (upgrade3 == upgrade1 || upgrade3 == upgrade2)
        {
            upgrade3 = Random.Range(0, upgradeOptionsCom.Count);
        }

        GameObject option1 = upgradeOptionsCom[upgrade1];
        GameObject option2 = upgradeOptionsCom[upgrade2];
        GameObject option3 = upgradeOptionsCom[upgrade3];



        SpawnUpgrades(option1, -1);
        SpawnUpgrades(option2, 0);
        SpawnUpgrades(option3, 1);


        //impliment an actual upgrade system here


        /*
        switch(numUpgrade)
        {
            case 1:
                playerMovement.trishot = true;
                break;

            case 2:
                playerMovement.moveForce += 2;
                break;
        }
        */

    }

    public void SpawnUpgrades(GameObject option, int space)
    {
        Vector3 spawnPosition = transform.position + Vector3.right * space * 550;
        GameObject spawnedUI = Instantiate(option, spawnPosition, Quaternion.identity);
        spawnedUI.transform.SetParent(canvas.transform, false);
        Button button = spawnedUI.GetComponent<Button>();
        revealedOptions.Add(spawnedUI);

        switch (spawnedUI.name)
        {
            case "Full Thrust!(Clone)":
                button.onClick.AddListener(FullThrust);
                break;
            case "Speed(Clone)":
                button.onClick.AddListener(Speed); 
                break;
            case "TriShot(Clone)":
                button.onClick.AddListener(TriShotUpgrade);
                break;
            case "Firerate(Clone)":
                button.onClick.AddListener(Firerate);
                break;
            case "Ability Cooldown(Clone)":
                button.onClick.AddListener(AbilityCD);
                break;
            
        }    
        button.onClick.AddListener(Resume);

    }




    public void Resume()
    {
        //Debug.Log("Clicka da button");
        foreach (GameObject option in revealedOptions)
        {
            Destroy(option);
        }
        revealedOptions.Clear();
        uiBlocker.SetActive(false);
        Time.timeScale = 1f;
    }
    //UPGRADE FUNCTIONS
    public void TriShotUpgrade()
    {
        triShotButton.SetActive(true);

        for (int i = 0; i < upgradeOptionsCom.Count; i++) 
        {
            if (upgradeOptionsCom[i].gameObject.name == "TriShot")
            {
                upgradeOptionsCom.RemoveAt(i);
            }
        }
    }
    public void Speed()
    {
        playerMovement.moveAmp += .2f;

    }

    public void Firerate()
    {
        playerMovement.fireRate += .1f;
    }
    public void AbilityCD()
    {
        playerMovement.cdAmp += .15f;
    }

    public void FullThrust()
    {
        fullThrustButton.SetActive(true);

        for (int i = 0; i < upgradeOptionsCom.Count; i++)
        {
            if (upgradeOptionsCom[i].gameObject.name == "Full Thrust!")
            {
                upgradeOptionsCom.RemoveAt(i);
            }
        }

    }

}
