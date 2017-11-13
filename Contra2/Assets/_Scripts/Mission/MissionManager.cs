using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {

    public static MissionManager instance = null;
    public ArrayList missionList;


    // Use this for initialization
    void Start () {
        if (instance == null)
            instance = this;
        else if (instance != this)

            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitMission()
    {
        missionList = new ArrayList();
        //12 mission
        PassAreaMission passArea1 = new PassAreaMission("Pass Area 1", 200);
        missionList.Add(passArea1);

        PassAreaMission passArea2 = new PassAreaMission("Pass Area 2", 300);
        missionList.Add(passArea2);

        PassAreaMission passArea3 = new PassAreaMission("Pass Area 3", 400);
        missionList.Add(passArea3);

        KillMission killMission4 = new KillMission("Kill 10 Soldier", 10, 20);
        missionList.Add(killMission4);

        KillMission killMission5 = new KillMission("Kill 50 Soldier", 50, 100);
        missionList.Add(killMission5);

        KillMission killMission6 = new KillMission("Kill 100 Soldier", 100, 200);
        missionList.Add(killMission6);

        KillMission killMission7 = new KillMission("Kill 10 Sniper", 10, 40);
        missionList.Add(killMission7);

        KillMission killMission8 = new KillMission("Kill 50 Sniper", 50, 200);
        missionList.Add(killMission8);

        KillMission killMission9 = new KillMission("Kill 100 Sniper", 100, 400);
        missionList.Add(killMission9);

        KillMission killMission10 = new KillMission("Kill 10 Sandbag Sniper", 10, 40);
        missionList.Add(killMission10);

        KillMission killMission11 = new KillMission("Kill 50 Sandbag Sniper", 50, 200);
        missionList.Add(killMission11);

        KillMission killMission12 = new KillMission("Kill 100 Sandbag Sniper", 100, 400);
        missionList.Add(killMission12);
    }
}
