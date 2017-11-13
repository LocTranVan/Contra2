using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMission : Mission {

    public int Process;
    public int killMustDo;

    public KillMission(string description ,int killMustDo, int coin)
    {
        this.Coin = coin;
        this.Complete = false;
        this.Receive = false;
        this.Process = 0;
        this.killMustDo = killMustDo;
        this.Description = description;
    }
}
