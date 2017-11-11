using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public GameObject loginPanel, playPanel, topBarPanel, icon;
    public Text showLoginButton, coinText;
    public Button toOfflineBtn;
    

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
        string email = emailInputField.text.Trim();
        string pass = passwordInputField.text.Trim();
        if (email.Equals("") || pass.Equals(""))
        {
            //error
            Debug.Log("email pass k hop le");
        } else
        {
            //auth.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWith(task => {
            //    if (task.IsCanceled)
            //    {
            //        Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
            //        return;
            //    }
            //    if (task.IsFaulted)
            //    {
            //        Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            //        return;
            //    }

                
            //});

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
            });
        }
    }

    public void ShowPlayPanel()
    {
        loginPanel.SetActive(false);
        if (PlayerPrefs.GetInt(RefDefinition.OFFLINE_MODE) == 1)
        {
            //not login jet
            toOfflineBtn.gameObject.SetActive(false);
            showLoginButton.text = "Login";
        }
        else
        {
            toOfflineBtn.gameObject.SetActive(true);
            showLoginButton.text = "Hi " + PlayerPrefs.GetString("email");
        }
        playPanel.SetActive(true);
    }

    private void InitTopBar()
    {
        
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
        });
    }

    public void play()
    {

    }

    public void logout()
    {
        auth.SignOut();
        PlayerPrefs.SetInt(RefDefinition.OFFLINE_MODE, 1);
        ShowPlayPanel();
        topBarPanel.SetActive(false);
    }
}
