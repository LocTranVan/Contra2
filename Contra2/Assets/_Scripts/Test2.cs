using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;

public class Test2 : MonoBehaviour {
    private FirebaseAuth auth;
    private DatabaseReference reference;

    void Awake ()
    {
        auth = FirebaseAuth.DefaultInstance;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://super-contra-20171.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference.Child("test");
    }

    // Use this for initialization
    void Start () {
        //List<Mission> dataList = new List<Mission>();
        //PassAreaMission passAreaMission = new PassAreaMission("XXXX");
        //dataList.Add(passAreaMission);
        //KillMission killMission = new KillMission("XXXXX2", 20);
        //dataList.Add(killMission);
        //string json = JsonUtility.ToJson(killMission);
        //Debug.Log(json);
        //auth.SignInWithEmailAndPasswordAsync("tan@example.com", "12345678").ContinueWith(task =>
        //{
        //    if (task.IsCompleted)
        //    {
        //        Debug.Log("sign in complete");
                

        //        //reference.SetValueAsync(dataList).ContinueWith(task2 => {
        //        //    if (task2.IsCompleted)
        //        //    {
        //        //        Debug.Log("push data complete");
        //        //    }
        //        //});
        //    }
        //});

        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
