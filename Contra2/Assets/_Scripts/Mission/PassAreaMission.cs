using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassAreaMission : Mission {

	public PassAreaMission(string description, int Coin)
    {
        this.Coin = Coin;
        this.Complete = false;
        this.Receive = false;
        this.Description = description;
    }
}
