using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingShootBehaivior : StateMachineBehaviour {

	public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		animator.ResetTrigger("Shoot");
	}
}
