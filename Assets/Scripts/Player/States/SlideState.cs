using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : PlayerBaseState
{
    private float speed = 8f;
    private bool IsInteruppt = false;
    public SlideState(PlayerStateManager.EPlayerState key) : base(key) { }
    public override void EnterState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) {
        nextStateId = StateKey;
        playerStateManager.animator.SetTrigger("IsSlide");
    }
    public override void UpdateState(StateManager<PlayerStateManager.EPlayerState> playerStateManager)
    {

        float verI = Input.GetAxis("Vertical");
        float hzI = Input.GetAxis("Horizontal");


        AnimatorStateInfo stateInfo = playerStateManager.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("Slide") && true)
        {
            if (verI == 0 && hzI == 0 && playerStateManager.animator.GetBool("IsIdle"))
            {
                nextStateId = PlayerStateManager.EPlayerState.Idle;
                return;
            }
            //
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
            else if (playerStateManager.animator.GetBool("IsCrouching"))
            {
                nextStateId = PlayerStateManager.EPlayerState.Crouch;
                return;
            }
        }

        Vector3 dir = Vector3.zero;
        Vector3 lastPos = playerStateManager.playerController.transform.position;

        playerStateManager.playerController.Move(dir.normalized * speed * Time.deltaTime);
        
        Vector3 currentPos = playerStateManager.playerController.transform.position;

        if (currentPos == lastPos)
        {
            //Interuppt Occurs 
                
            //Restrictrict This States
            playerStateManager.animator.SetBool("IsWalking", false);  
            playerStateManager.animator.SetBool("IsRunning", false);

            //Transition to Crouch State
            if (playerStateManager.animator.GetBool("IsCrouching"))
            { 
                nextStateId = PlayerStateManager.EPlayerState.Crouch;
            }
            //Transition to Idle State
            else
            {
                nextStateId = PlayerStateManager.EPlayerState.Idle;
            }
            return;

        }
    }
    public override void FixedUpdateState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) { }

    public override void ExitState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) {

        if (IsInteruppt)
        {
            playerStateManager.animator.SetBool("IsRunning",false);
        }

    }

    public override PlayerStateManager.EPlayerState GetNextStateID()
    {
        return nextStateId;
    }
    public override void OnTriggerEnterState(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
                   
        }
        
    }
    public override void OnTriggerExitState(Collider other) { }
    public override void OnTriggerStayState(Collider other) { }
    public override void OnCollisionEnterState(Collision other) { }
    public override void OnCollisionExitState(Collision other) { }
    public override void OnCollisionStayState(Collision other) { }
}
