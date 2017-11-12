using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassAreaMission : Mission {

	public PassAreaMission(string description)
    {
        this.isComplete = false;
        this.isReceive = false;
        this.description = description;
    }
}
