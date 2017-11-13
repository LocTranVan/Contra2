using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameHandler : MonoBehaviour {

    public GameObject textPanel, loadingPanel, continueBtn;
    public Text title, scoreText;
    private DatabaseReference reference;

    // Use this for initialization
    void Start () {
        setEnablePanel(textPanel, false);
        setEnablePanel(loadingPanel, true);
        int score;
        GameManager.instance.gameResult.TryGetValue(RefDefinition.SCORE, out score);
        scoreText.text = score + "";

        if (GameManager.instance.isGameOver || (GameManager.instance.currentArea == 2))
        {
            Destroy(continueBtn);
        }

        int areaIndex = GameManager.instance.currentArea + 1;
        if (GameManager.instance.isGameOver)
        {
            //game over
            title.text = "Area " + areaIndex + " Failed";
            
        } else
        {
            title.text = "Area " + areaIndex + " Complete";
        }

        if (PlayerPrefs.GetInt(RefDefinition.OFFLINE_MODE) == 1) 
        {
            //offline mode, just get score,  area
            setEnablePanel(loadingPanel, false);
            setEnablePanel(textPanel, true);

            //init for next area
        } else
        {
            //online mode
            //update misson list
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://super-contra-20171.firebaseio.com/");
            reference = FirebaseDatabase.DefaultInstance.RootReference.Child("User").Child(PlayerPrefs.GetString(RefDefinition.UID));
            int count = 0;
            int taskNum = 0;
            if (!GameManager.instance.isGameOver)
            {
                PassAreaMission m = (PassAreaMission)MissionManager.instance.missionList[GameManager.instance.currentArea];
                if (!m.Complete)
                {
                    //chua hoan thanh, update hoan thanh
                    m.Complete = true;
                    m.Receive = false;
                    string json = JsonUtility.ToJson(m);
                    taskNum++;
                    reference.Child("Missions").Child(GameManager.instance.currentArea.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
                    {
                        count++;
                        checkTask(count, taskNum);
                    });
                }
            }
            int soldierKill;
            GameManager.instance.gameResult.TryGetValue(RefDefinition.SOLDIER, out soldierKill);
            if (soldierKill > 0)
            {
                for (int i = 3; i < 6; i++)
                {
                    
                    int j = i;
                    Debug.Log("UPDATE SOLDIER " + j);
                    KillMission m = (KillMission)MissionManager.instance.missionList[j];
                    if (!m.Complete)
                    {
                        Debug.Log("UPDATE SOLDIER2 " + j);
                        m.Process += soldierKill;
                        if (m.Process >= m.killMustDo)
                        {
                            m.Process = m.killMustDo;
                            m.Complete = true;
                            m.Receive = false;
                        }
                        taskNum++;
                        string json = JsonUtility.ToJson(m);
                        reference.Child("Missions").Child(j.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
                        {
                        Debug.Log("UPDATE SOLDIER3 " + j + " "+ json);
                            count++;
                            checkTask(count, taskNum);
                        });
                    }
                }
            }
            int sniperKill;
            GameManager.instance.gameResult.TryGetValue(RefDefinition.SNIPER, out sniperKill);
            if (sniperKill > 0)
            {
                for (int i = 6; i < 9; i++)
                {
                    int j = i;
                    Debug.Log("UPDATE SNIPER " + j);
                    KillMission m = (KillMission)MissionManager.instance.missionList[j];
                    if (!m.Complete)
                    {
                        Debug.Log("UPDATE SNIPER2 " + j);
                        m.Process += sniperKill;
                        if (m.Process >= m.killMustDo)
                        {
                            m.Process = m.killMustDo;
                            m.Complete = true;
                            m.Receive = false;
                        }
                        taskNum++;
                        string json = JsonUtility.ToJson(m);
                        reference.Child("Missions").Child(j.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
                        {
                            Debug.Log("UPDATE SNIPER3 " + j + " " + json);
                            count++;
                            checkTask(count, taskNum);
                        });
                    }
                }
            }
            int sandbagKill;
            GameManager.instance.gameResult.TryGetValue(RefDefinition.SANDBAG_SNIPER, out sandbagKill);
            if (sandbagKill > 0)
            {
                for (int i = 9; i < 12; i++)
                {
                    int j = i;
                    Debug.Log("UPDATE SANDBAG " + j);
                    KillMission m = (KillMission)MissionManager.instance.missionList[j];
                    if (!m.Complete)
                    {
                        Debug.Log("UPDATE SANDBAG2 " + j);
                        m.Process += sandbagKill;
                        if (m.Process >= m.killMustDo)
                        {
                            m.Process = m.killMustDo;
                            m.Complete = true;
                            m.Receive = false;
                        }
                        taskNum++;
                        string json = JsonUtility.ToJson(m);
                        reference.Child("Missions").Child(j.ToString()).SetRawJsonValueAsync(json).ContinueWith(task =>
                        {
                            Debug.Log("UPDATE Sandbag3 " + j + " " + json);
                            count++;
                            checkTask(count, taskNum);
                        });
                    }
                }
            }

        }
	}
	
    private void checkTask(int count, int task)
    {
        if (task == count)
        {
            //task complete
            setEnablePanel(loadingPanel, false);
            setEnablePanel(textPanel, true);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    private void setEnablePanel(GameObject panel, bool isActive)
    {
        panel.SetActive(isActive);
    }

    public void NextArea()
    {
        GameManager.instance.currentArea++;
        GameManager.instance.setResult(RefDefinition.SCORE, 0);
        GameManager.instance.setResult(RefDefinition.SOLDIER, 0);
        GameManager.instance.setResult(RefDefinition.SNIPER, 0);
        GameManager.instance.setResult(RefDefinition.SANDBAG_SNIPER, 0);
        SceneManager.LoadScene("PreGame");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
