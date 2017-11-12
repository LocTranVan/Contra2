using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {

    public class User
    {
        public string username;
        public string email;
        public int age;

        public User()
        {
        }

        public User(string username, string email, int age)
        {
            this.username = username;
            this.email = email;
            this.age = age;
        }
    }

    public InputField emailInputField, passwordInputField;
    private FirebaseAuth auth;
    private DatabaseReference reference;
    public GameObject loginPanel, playPanel, topBarPanel, icon, loadingPanel;
    public Text showLoginButton, coinText;
    public Button toOfflineBtn, playImmortalModeBtn;
    

    private void Awake()
    {
        LeanTween.alpha(icon.GetComponent<RectTransform>(), 0.0f, 0.0f);
        auth = FirebaseAuth.DefaultInstance;
        passwordInputField.inputType = InputField.InputType.Password;

        //init null player
        PlayerPrefs.SetString(RefDefinition.UID, "");
        PlayerPrefs.SetInt(RefDefinition.OFFLINE_MODE, 1);
        topBarPanel.SetActive(false);

        Invoke("ShowPlayPanel", 2.5f);
    }

    // Use this for initialization
    void Start () {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://super-contra-20171.firebaseio.com/");

        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        LeanTween.alpha(icon.GetComponent<RectTransform>(), 1.0f, 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowLoginPanel()
    {
        //show login panel
        //if logined, do notthing
        if (PlayerPrefs.GetInt(RefDefinition.OFFLINE_MODE) == 1)
        {
            playPanel.SetActive(false);
            emailInputField.text = "";
            passwordInputField.text = "";
            loginPanel.SetActive(true);
        } else
        {
            //do nothing
        }

        
    }

    public void Login()
    {
        showLoading();
        string email = emailInputField.text.Trim();
        string pass = passwordInputField.text.Trim();
        if (email.Equals("") || pass.Equals(""))
        {
            //error
            Debug.Log("email pass k hop le");
        } else
        {

            auth.SignInWithEmailAndPasswordAsync(email, pass).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }
                disableLoading();
                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
                // Firebase user has been created.
                PlayerPrefs.SetString("email", newUser.Email);
                //PlayerPrefs.SetString("display_name", newUser.DisplayName);
                PlayerPrefs.SetString("uid", newUser.UserId);
                PlayerPrefs.SetInt(RefDefinition.OFFLINE_MODE, 0);

                ShowPlayPanel();
                //writeNewUser(newUser.UserId, "tanphamanh", "tanpham@example.com", 20);
                InitTopBar();
                InitMission();
            });
        }
    }

    public void ShowPlayPanel()
    {
        loginPanel.SetActive(false);
        if (PlayerPrefs.GetInt(RefDefinition.OFFLINE_MODE) == 1)
        {
            //not login jet
            playImmortalModeBtn.gameObject.SetActive(true);
            toOfflineBtn.gameObject.SetActive(false);
            showLoginButton.text = "Login";
        }
        else
        {
            toOfflineBtn.gameObject.SetActive(true);
            playImmortalModeBtn.gameObject.SetActive(false);
            showLoginButton.text = "Hi " + PlayerPrefs.GetString("email");
        }
        playPanel.SetActive(true);
    }

    private void InitTopBar()
    {
        showLoading();
        Debug.Log("Init top bar");
        //get coin from db
        DatabaseReference userRef = reference.Child("User").Child(PlayerPrefs.GetString("uid"));
        topBarPanel.SetActive(true);

        userRef.Child("Coin").GetValueAsync().ContinueWith(task => {
            
            if (task.IsFaulted)
            {
                Debug.Log("coin get failed");
                coinText.text = "0";
                
            }
            else if (task.IsCompleted)
            {
                Debug.Log("coin get complete");

                DataSnapshot snapshot = task.Result;
                Debug.Log("coin = " + snapshot.Value.ToString());
                //int coin = (int) snapshot.Value;
                coinText.text = snapshot.Value.ToString();
            }
            disableLoading();
        });
    }

    private void InitMission()
    {
        showLoading();
        Debug.Log("Start init misson");
        MissionManager.instance.InitMission();
        DatabaseReference missionRef = reference.Child("User").Child(PlayerPrefs.GetString("uid")).Child("Missions");
        //missionRef.
        missionRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.Log("Get missoon error");
            }
            else if (task.IsCompleted)
            {
                Debug.Log("Get missoon complete");
                DataSnapshot snapshot = task.Result;
                for (int i = 0; i < MissionManager.instance.missionList.Count; i++)
                {
                    int j = i;
                    DataSnapshot missionSnapshot = snapshot.Child(j.ToString());
                    bool isComplete = (bool)missionSnapshot.Child("Complete").Value;
                    bool isReceive = (bool)missionSnapshot.Child("Receive").Value;
                    if (j <= 2)
                    {
                        //pass misson
                        ((PassAreaMission)MissionManager.instance.missionList[j]).isComplete = isComplete;
                        ((PassAreaMission)MissionManager.instance.missionList[j]).isReceive = isReceive;
                        Debug.Log("Pass Area Mission " + j + isComplete + " " + isReceive);
                    }
                    else
                    {
                        //kill mission
                        int process = System.Int32.Parse(missionSnapshot.Child("Process").Value.ToString());
                        ((KillMission)MissionManager.instance.missionList[j]).isComplete = isComplete;
                        ((KillMission)MissionManager.instance.missionList[j]).isReceive = isReceive;
                        ((KillMission)MissionManager.instance.missionList[j]).process = process;
                        Debug.Log("Kill Mission " + j + isComplete + " " + isReceive + " " + process);
                    }
                }
                disableLoading();
            }
        });


        //for (int i = 0; i < MissionManager.instance.missionList.Count; i++)
        //{
        //    int j = i;
        //    missionRef.Child(j.ToString()).GetValueAsync().ContinueWith(task =>
        //    {
        //        if (task.IsCanceled || task.IsFaulted)
        //        {
        //            Debug.Log("Get missoon " + j + " error");
        //        }
        //        else if (task.IsCompleted)
        //        {
        //            Debug.Log("Get missoon " + j + " complete");
        //            DataSnapshot snapshot = task.Result;
        //            bool isComplete = (bool)snapshot.Child("Complete").Value;
        //            bool isReceive = (bool)snapshot.Child("Receive").Value;
        //            if (j <= 2)
        //            {
        //                //pass misson
        //                ((PassAreaMission)MissionManager.instance.missionList[j]).isComplete = isComplete;
        //                ((PassAreaMission)MissionManager.instance.missionList[j]).isReceive = isReceive;
        //                Debug.Log("Pass Area Mission " + j + isComplete + " " + isReceive);
        //            } else
        //            {
        //                //kill mission
        //                int process = System.Int32.Parse(snapshot.Child("Process").Value.ToString());
        //                ((KillMission)MissionManager.instance.missionList[j]).isComplete = isComplete;
        //                ((KillMission)MissionManager.instance.missionList[j]).isReceive = isReceive;
        //                ((KillMission)MissionManager.instance.missionList[j]).process = process;
        //                Debug.Log("Kill Mission " + j + isComplete + " " + isReceive + " " + process);
        //            }
        //        }
        //    });
        //}
        
    }

    public void play(bool immortal)
    {
        GameManager.instance.immortal = immortal;
        //set default bullet...


        if (immortal)
        {
            GameManager.instance.lives = RefDefinition.IMMORTAL_LIVE_VALUE;
        } else
        {
            GameManager.instance.lives = RefDefinition.DEFAULT_LIVES;

        }
        //to pre scene
        SceneManager.LoadScene("PreGame");
        
    }

    public void logout()
    {
        auth.SignOut();
        PlayerPrefs.SetInt(RefDefinition.OFFLINE_MODE, 1);
        ShowPlayPanel();
        topBarPanel.SetActive(false);
    }

    public void showLoading()
    {
        loadingPanel.SetActive(true);
    }

    public void disableLoading()
    {
        loadingPanel.SetActive(false);
    }
}
