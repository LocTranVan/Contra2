using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameHandler : MonoBehaviour {

    public GameObject textPanel, loadingPanel;
    public Text title, scoreText;
    private DatabaseReference reference;

    // Use this for initialization
    void Start () {
        setEnablePanel(textPanel, false);
        setEnablePanel(loadingPanel, true);
        int score;
        GameManager.instance.gameResult.TryGetValue(RefDefinition.SCORE, out score);
        int areaIndex = GameManager.instance.currentArea + 1;
        if (GameManager.instance.isGameOver)
        {
            //game over
            title.text = "Area " + areaIndex + " Failed";
            scoreText.text = score + "";
            
        }

        if (PlayerPrefs.GetInt(RefDefinition.OFFLINE_MODE) == 1) 
        {
            //offline mode, just get score,  area
            setEnablePanel(loadingPanel, false);
            setEnablePanel(textPanel, true);
        } else
        {
            //online mode
            //update misson list
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://super-contra-20171.firebaseio.com/");
            reference = FirebaseDatabase.DefaultInstance.RootReference.Child("User").Child(PlayerPrefs.GetString(RefDefinition.UID));
            int count = 0;
            if (!GameManager.instance.isGameOver)
            {
                if (!((PassAreaMission)MissionManager.instance.missionList[GameManager.instance.currentArea]).isComplete)
                {
                    Dictionary<string, bool> missionData = new Dictionary<string, bool>();
                    missionData.Add("Complete", true);
                    missionData.Add("Receive", false);
                    reference.Child("Missions").Child(GameManager.instance.currentArea.ToString()).SetValueAsync(missionData).ContinueWith(task =>
                    {
                        if (task.IsCanceled || task.IsFaulted)
                        {
                            count++;
                            Debug.Log("Update pass mission error");
                        }
                        else if (task.IsCompleted)
                        {
                            Debug.Log("Update pass mission complete");

                        }
                    });
                }
            }

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void setEnablePanel(GameObject panel, bool isActive)
    {
        panel.SetActive(isActive);
    }
}
