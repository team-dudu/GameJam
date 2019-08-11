using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
    public class DeathBehavior : StateMachineBehaviour
    {
        private Jiraya jiraya;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("OnEnter Death");
            jiraya = animator.GetComponent<Jiraya>();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}