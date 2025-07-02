using UnityEngine;

public class IdleState : PlayerBaseState
{
    public IdleState(PlayerStateManager.EPlayerState key) : base(key) { }
    public override void EnterState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) {
        nextStateId = StateKey;

        PlayerStateManager.Instance.playerController.height = PlayerStateManager.Instance.playerContext.playerHeight;
        PlayerStateManager.Instance.playerController.center = Vector3.zero;

        playerStateManager.animator.SetBool("IsIdle", true);
    }
    public override void UpdateState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) {

        float verI = Input.GetAxis("Vertical");
        float hzI = Input.GetAxis("Horizontal");

        Vector3 forward = PlayerStateManager.Instance.playerTransform.forward * verI;
        Vector3 side = PlayerStateManager.Instance.playerTransform.right * hzI;
        Vector3 resultant = forward + side;

        if (Input.GetButtonDown("Crouch"))
        {
            //Transition to CrouchWalk
            if (IsCrouchPossible())
            {
                nextStateId = PlayerStateManager.EPlayerState.Crouch;
                return;
            }
        }
        else if (Input.GetButtonDown("Prone"))
        {
            //Transition to Crawl
            if (IsCrawlPossible())
            {
                nextStateId = PlayerStateManager.EPlayerState.Crawl;
                return;
            }
        }
        else if (Input.GetButtonDown("Climb") && IsClimbPossible())
        {
               //Transition to Climb
                Debug.Log("I am in climb");
                nextStateId = PlayerStateManager.EPlayerState.Climb;
                return;
            
        }
        else if (Input.GetButtonDown("Jump") && IsJumpPossible())
        {
            //Transition to Jump
                nextStateId = PlayerStateManager.EPlayerState.Jump;
                return;
        }
        else if (verI != 0 || hzI != 0)
        {
            //Transition to walk
            if (IsMovePossible(resultant.normalized))
            {
                nextStateId = PlayerStateManager.EPlayerState.Walk;
                return;
            }
        }

    }


    public override void FixedUpdateState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) { }


    public override void ExitState(StateManager<PlayerStateManager.EPlayerState> playerStateManager)
    {
        //verify for next Special States like Jump ,Climb , Slide 
        if (!PlayerStateManager.Instance.specialStates.Contains(nextStateId))
        {
            playerStateManager.animator.SetBool("IsIdle", false);
        }
    }
           
    public override PlayerStateManager.EPlayerState GetNextStateID()
    {
        return nextStateId;
    }
    public override void OnTriggerEnterState(Collider other){
            
        if (other.CompareTag("Obstacle"))
        {
            Vector3 headPos = PlayerStateManager.Instance.playerTransform.position;
            float headOffset = 1.2f;
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
    public override void OnTriggerExitState(Collider other){
        Debug.Log("False");
        PlayerStateManager.Instance.playerContext.IsClimbPossible = false;
    }
    public override void OnTriggerStayState(Collider other){ }
    public override void OnCollisionEnterState(Collision other){ }
    public override void OnCollisionExitState(Collision other){ }
    public override void OnCollisionStayState(Collision other){ }
}
