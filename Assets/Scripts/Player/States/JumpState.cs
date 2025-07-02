using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class JumpState : PlayerBaseState
{
    int cnt = 0;
    public JumpState(PlayerStateManager.EPlayerState key) : base(key) { }
    public override void EnterState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) {
        cnt = 0;
        nextStateId = StateKey;

        PlayerStateManager.Instance.playerController.height = PlayerStateManager.Instance.playerContext.playerHeight;
        PlayerStateManager.Instance.playerController.center = Vector3.zero;

        playerStateManager.animator.SetTrigger("IsJump");
    }
    public override void UpdateState(StateManager<PlayerStateManager.EPlayerState> playerStateManager)
    {
        cnt++;
        AnimatorStateInfo stateInfo = playerStateManager.animator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetButtonDown("Climb"))
        {
            Debug.Log("Climb Start");
            if (IsClimbPossible())
            {
                nextStateId = PlayerStateManager.EPlayerState.Climb;
                return;
            }
        }
        else if (cnt > 350)
        {
            Debug.Log("Thanks");
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
        }

    }

    public override void FixedUpdateState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) { }


    public override void ExitState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) {

    }
          
    public override PlayerStateManager.EPlayerState GetNextStateID(){
        return nextStateId;
    }
    public override void OnTriggerEnterState(Collider other){}
    public override void OnTriggerExitState(Collider other){}
    public override void OnTriggerStayState(Collider other){}
    public override void OnCollisionEnterState(Collision other){}
    public override void OnCollisionExitState(Collision other){}
    public override void OnCollisionStayState(Collision other){}
}
