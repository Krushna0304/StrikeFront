using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbState : PlayerBaseState
{
    public ClimbState(PlayerStateManager.EPlayerState key) : base(key) { }
    public override void EnterState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) {
        nextStateId = StateKey;
        playerStateManager.animator.SetTrigger("IsClimb");

        Vector3 locAfterClimb = PlayerStateManager.Instance.playerContext.contactPoint + PlayerStateManager.Instance.playerContext.climbLocOffset;
        PlayerStateManager.Instance.playerTransform.position = locAfterClimb;

        Time.timeScale = 0;
        Debug.Log(PlayerStateManager.Instance.playerTransform.position);

        //Place hands for climb correctly and then remove after specific time
        //StartCoroutine(PlaceHands());
    }
    IEnumerator PlaceHands()
    {
        Vector3 lefthandPos = Vector3.zero;
        Vector3 rightHandPos = Vector3.zero;

        
        yield  return new WaitForSeconds(1); // Check to use frames

    }

    public override void UpdateState(StateManager<PlayerStateManager.EPlayerState> playerStateManager)
    {
        AnimatorStateInfo stateInfo = playerStateManager.animator.GetCurrentAnimatorStateInfo(0);

     /*   if (!stateInfo.IsName("Climb"))
        {
            if (playerStateManager.animator.GetBool("IsIdle"))
            {
                nextStateId = PlayerStateManager.EPlayerState.Idle;
                return;
            }
            else if (playerStateManager.animator.GetBool("IsWalking"))
            {
                nextStateId = PlayerStateManager.EPlayerState.Walk;
                return;
            }
            else if (playerStateManager.animator.GetBool("IsRunning"))
            {
                nextStateId = PlayerStateManager.EPlayerState.Run;
                return;
            }

            //Think for Crouching
        }*/



    }
    public override void FixedUpdateState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) { 
    

    
    }

    public override void ExitState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) { }

    public override PlayerStateManager.EPlayerState GetNextStateID()
    {
        return nextStateId;
    }
    public override void OnTriggerEnterState(Collider other) { }
    public override void OnTriggerExitState(Collider other) { }
    public override void OnTriggerStayState(Collider other) { }
    public override void OnCollisionEnterState(Collision other) { }
    public override void OnCollisionExitState(Collision other) { }
    public override void OnCollisionStayState(Collision other) { }
}
