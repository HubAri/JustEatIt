using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class BeginGameCounter : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("three"))
        {
            Debug.Log("stateInfo: three");
            Time.timeScale = 0;
        } 
        else if (stateInfo.IsName("two"))
        {
            BeginGameText beginGameIn = GameObject.Find("GameObject").GetComponent<BeginGameText>();
            beginGameIn.SetText("2");
            Debug.Log("stateInfo: two");
        }
        else if (stateInfo.IsName("one"))
        {
            BeginGameText beginGameIn = GameObject.Find("GameObject").GetComponent<BeginGameText>();
            beginGameIn.SetText("1");
            Debug.Log("stateInfo: one");
        }
        else if (stateInfo.IsName("start"))
        {
            BeginGameText beginGameIn = GameObject.Find("GameObject").GetComponent<BeginGameText>();
            beginGameIn.SetText("Start"); 
            Debug.Log("stateInfo: start");
        }
        else if (stateInfo.IsName("play"))
        {
            BeginGameText beginGameIn = GameObject.Find("GameObject").GetComponent<BeginGameText>();
            beginGameIn.SetText(string.Empty); 
            Debug.Log("stateInfo: play");
            Time.timeScale = 1f;
            FindObjectOfType<AudioManager>().Play("Background");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
