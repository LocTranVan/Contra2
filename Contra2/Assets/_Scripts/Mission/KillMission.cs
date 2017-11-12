using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMission : Mission {

    public int process;
    public int killMustDo;

    public KillMission(string description ,int killMustDo)
    {
        this.isComplete = false;
        this.isReceive = false;
        this.process = 0;
        this.killMustDo = killMustDo;
        this.description = description;
    }
}
