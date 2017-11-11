using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameHandler : MonoBehaviour {

    public GameObject shopPanel, preData;
    private DatabaseReference reference;
    public PreGameData preGameData;
    int coin;

    void Awake()
    {
        
        if (PlayerPrefs.GetInt(RefDefinition.OFFLINE_MODE) == 1)
        {
            //offline mode
            shopPanel.SetActive(false);
        } else
        {
            shopPanel.SetActive(false);
            //online mode
            //init coin
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://super-contra-20171.firebaseio.com/");

            // Get the root reference location of the database.
            reference = FirebaseDatabase.DefaultInstance.RootReference;


        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void buyLife()
    {
        //get coin
        if (coin >= ItemPrice.LIFE)
        {
            int newCoin = coin - ItemPrice.FLAME_BULLET;
            //can buy

        } else
        {
            Debug.Log("not enough coin");
        }
    }

    public void buyMachineBullet()
    {
        if (coin >= ItemPrice.MACHINE_BULLET)
        {
            int newCoin = coin - ItemPrice.MACHINE_BULLET;
            //can buy
        }
        else
        {
            Debug.Log("not enough coin");
        }
    }

    public void buyLazerBullet()
    {
        if (coin >= ItemPrice.LAZER_BULLET)
        {
            int newCoin = coin - ItemPrice.LAZER_BULLET;
            //can buy
        }
        else
        {
            Debug.Log("not enough coin");
        }
    }

    public void buyFlameBullet()
    {
        if (coin >= ItemPrice.FLAME_BULLET)
        {

            int newCoin = coin - ItemPrice.FLAME_BULLET;
            //can buy
        }
        else
        {
            Debug.Log("not enough coin");
        }
    }

    public void buySpreadBullet()
    {
        if (coin >= ItemPrice.SPREAD_BULLET)
        {
            int newCoin = coin - ItemPrice.SPREAD_BULLET;
            //can buy
        }
        else
        {
            Debug.Log("not enough coin");
        }
    }
}
