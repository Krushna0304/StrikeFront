using UnityEngine;

public class RunState : PlayerBaseState
{
    [SerializeField]
    private float magnitude = 5f;

    public  RunState(PlayerStateManager.EPlayerState key) : base(key) { }
    public override void EnterState(StateManager<PlayerStateManager.EPlayerState> playerStateManager)
    {
        Debug.Log("Enter Run State");
        nextStateId = StateKey;
        playerStateManager.animator.SetBool("IsRunning", true);
    }
    public override void UpdateState(StateManager<PlayerStateManager.EPlayerState> playerStateManager)
    {
        float verI = Input.GetAxis("Vertical");
        float hzI = Input.GetAxis("Horizontal");

        playerStateManager.animator.SetFloat("VerI", verI);
        playerStateManager.animator.SetFloat("HzI", hzI);

        Vector3 forward = PlayerStateManager.Instance.playerTransform.forward * verI;
        Vector3 side = PlayerStateManager.Instance.playerTransform.right * hzI;
        Vector3 resultant = forward + side;

        Vector3 lastPos = PlayerStateManager.Instance.playerTransform.position;

         if (!Input.GetButton("Run"))
        {
            //Transition to Walk State
            nextStateId = PlayerStateManager.EPlayerState.Walk;
            return;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Detect to ho raha hai");
            //Transition to Slide State
            nextStateId = PlayerStateManager.EPlayerState.Slide;
            return;
        }
        /*else if (Input.GetButtonDown("Climb"))
        {
            //Transition to Climb State
            if (IsClimbPossible())
            {
                nextStateId = PlayerStateManager.EPlayerState.Climb;
                return;
            }
        }*/
        else if (Input.GetButtonDown("Jump"))
        {
            //Transition to Jump State
            if (IsJumpPossible())
            {
                nextStateId = PlayerStateManager.EPlayerState.Jump;
                return;
            }
        }
        else if (Input.GetButtonDown("Crouch"))
        {
            //Transition to Crouch State
            nextStateId = PlayerStateManager.EPlayerState.Crouch;
            return;
        }
        else if (Input.GetButton("Run"))
        {
            //In Run State
            Vector3 output = magnitude * Time.deltaTime * resultant.normalized;
            playerStateManager.playerController.Move(output);
        }
        Vector3 currentPos = PlayerStateManager.Instance.playerTransform.position;
        if (!((Vector3.Distance(currentPos, lastPos) / Time.deltaTime) >= 0.01f))
        {
            //Transition to Idle State through Walk
            nextStateId = PlayerStateManager.EPlayerState.Walk;
            return;
        }
        //Add other States
    }
    public override void FixedUpdateState(StateManager<PlayerStateManager.EPlayerState> playerStateManager) { }

    public override void ExitState(StateManager<PlayerStateManager.EPlayerState> playerStateManager)
    {
        //verify for next Special States like Jump ,Climb , Slide 
        if (!PlayerStateManager.Instance.specialStates.Contains(nextStateId))
        {
            Debug.Log("Run State Exit");
            playerStateManager.animator.SetBool("IsRunning", false);
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
