using Unity.VisualScripting;
using UnityEngine;

public class WalkState : PlayerBaseState
{
    [SerializeField]
    private float magnitude = 2f;
    private Vector3 resultant;
    public WalkState(PlayerStateManager.EPlayerState key) : base(key) { }
    public override void EnterState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) {
        nextStateId = StateKey;

        PlayerStateManager.Instance.playerController.height = PlayerStateManager.Instance.playerContext.playerHeight;
        PlayerStateManager.Instance.playerController.center = Vector3.zero;

        playerStateManager.animator.SetBool("IsWalking",true);
    }
    public override void UpdateState(StateManager<PlayerStateManager.EPlayerState> playerStateManager)
    {
        float verI = Input.GetAxis("Vertical");
        float hzI = Input.GetAxis("Horizontal");

        playerStateManager.animator.SetFloat("VerI", verI);
        playerStateManager.animator.SetFloat("HzI", hzI);

        Vector3 forward = PlayerStateManager.Instance.playerTransform.forward * verI;
        Vector3 side = PlayerStateManager.Instance.playerTransform.right * hzI;
        resultant = forward + side;

        if (resultant.magnitude == 0 || !IsMovePossible(resultant.normalized))
        {
            //Transition to Idle State
            nextStateId = PlayerStateManager.EPlayerState.Idle;
            return;
        }
        else if (Input.GetButton("Run"))
        {
            //Transition to Run State
            nextStateId = PlayerStateManager.EPlayerState.Run;
            return;
        }
        else if (Input.GetButtonDown("Crouch"))
        {
            //Transition to Run State
            nextStateId = PlayerStateManager.EPlayerState.Crouch;
            return;
        }
        else if (false && Input.GetButtonDown("Jump"))
        {
            //Transition to Run State
            if (IsJumpPossible())
            { 
                nextStateId = PlayerStateManager.EPlayerState.Jump;
                return;
            }
        }
        else if (Input.GetButtonDown("Climb"))
        {
            //Transition to Run State
            if (IsClimbPossible())
            {
                Debug.Log("Climb me hu");
                nextStateId = PlayerStateManager.EPlayerState.Jump;
                return;
            }
        }
        //Add other States
     /*   else
        {
            Vector3 output = magnitude * Time.deltaTime * resultant.normalized;
            playerStateManager.playerController.Move(output);
        }*/
    }



    public override void FixedUpdateState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) { 
        Vector3 output = magnitude * Time.fixedDeltaTime * resultant.normalized;
        playerStateManager.playerController.Move(output);
    }

    public override void ExitState(StateManager<PlayerStateManager.EPlayerState> playerStateManager)
    {
        //verify for next Special States like Jump ,Climb , Slide 
        if (!PlayerStateManager.Instance.specialStates.Contains(nextStateId))
        {
            playerStateManager.animator.SetBool("IsWalking", false);
        }
    }

    public override PlayerStateManager.EPlayerState GetNextStateID()
    {

        return nextStateId;
    }
    public override void OnTriggerEnterState(Collider other) {

        if (other.CompareTag("Obstacle"))
        {
            Vector3 headPos = PlayerStateManager.Instance.playerTransform.position;
            float headOffset = 9f;
            headPos.y += headOffset;

            Vector3 contactPt = other.ClosestPoint(headPos);
            if (contactPt.y < headPos.y)
            {
                PlayerStateManager.Instance.playerContext.contactPoint = contactPt;
                PlayerStateManager.Instance.playerContext.IsClimbPossible = true;
                Debug.Log("Can Climb");
            }
            else
            {
                PlayerStateManager.Instance.playerContext.IsClimbPossible = false;
                Debug.Log("Can Not Climb");
            }
        }

    }
    public override void OnTriggerExitState(Collider other) {
        Debug.Log("False");
        PlayerStateManager.Instance.playerContext.IsClimbPossible = false;
    }
    public override void OnTriggerStayState(Collider other) { }
    public override void OnCollisionEnterState(Collision other) { }
    public override void OnCollisionExitState(Collision other) { }
    public override void OnCollisionStayState(Collision other) { }
}
