using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlState : PlayerBaseState
{
    [SerializeField] private float speed = .4f;
    public CrawlState(PlayerStateManager.EPlayerState key) : base(key) { }
    public override void EnterState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) {
        nextStateId = StateKey;
        PlayerStateManager.Instance.playerController.height = PlayerStateManager.Instance.playerContext.playerProneHeight;
        PlayerStateManager.Instance.playerController.center = Vector3.up * (-PlayerStateManager.Instance.playerContext.playerHeight / 2 + PlayerStateManager.Instance.playerContext.playerProneHeight);
        playerStateManager.animator.SetBool("IsCrawling", true);
    }
    public override void UpdateState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) 
    {

        float verI = Input.GetAxis("Vertical");
        float hzI = Input.GetAxis("Horizontal");

        Vector3 forward = PlayerStateManager.Instance.playerTransform.forward * verI;
        Vector3 side = PlayerStateManager.Instance.playerTransform.right * hzI;
        Vector3 resultant = forward + side;

        if (Input.GetButtonDown("Prone"))
        {
            //Transition to Idle State
            nextStateId = PlayerStateManager.EPlayerState.Idle;
            return;
        }
        else if (Input.GetButtonDown("Crouch"))
        {
            //Transition to Crouch State
            nextStateId = PlayerStateManager.EPlayerState.Crouch;
            return;
        }
        else 
        {
            //Decide the is incliend movement Allowed

            if (resultant.magnitude !=0 && IsMovePossible(resultant.normalized))
            { 
                Vector3 output = speed * Time.deltaTime * resultant.normalized;
                playerStateManager.playerController.Move(output);

                playerStateManager.animator.SetFloat("VerI", verI);
                playerStateManager.animator.SetFloat("HzI", hzI);
            }
            else
            {
                playerStateManager.animator.SetFloat("VerI", 0);
                playerStateManager.animator.SetFloat("HzI", 0);
            }
        }
    }
    public override void FixedUpdateState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) { }

    public override void ExitState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) 
    {
        //verify for next Special States like Jump ,Climb , Slide 
        if (!PlayerStateManager.Instance.specialStates.Contains(nextStateId))
        {
            playerStateManager.animator.SetBool("IsCrawling", false);
        }
    }

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

