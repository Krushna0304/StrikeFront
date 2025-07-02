
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : StateManager<PlayerStateManager.EPlayerState>
{

    public enum EPlayerState { 
        Idle,
        Walk,
        Crouch,
        Run,
        Crawl,
        Slide,
        Jump,
        Climb,
    }


    public static PlayerStateManager Instance { get; private set; }
    public List<EPlayerState> specialStates = new List<EPlayerState>();
    public PlayerContext playerContext;

    [SerializeField] private float headOffset;
    [SerializeField] private float footOffset;
    [SerializeField] private float climbOffset;
    [SerializeField] private float jumpOffset;
    [SerializeField] private float moveOffset;
    [SerializeField] private float playerHeight;

    [SerializeField] private float playerCrouchHeight;
    [SerializeField] private float playerProneHeight;

    [SerializeField] private Vector3 climbLocOffset;

    private bool isClimbPossible;
    public Transform playerTransform;

    public LayerMask playerMask;

    public void Awake()
    {
        Instance = this;
        playerTransform = GetComponent<Transform>();
        isClimbPossible = false;

        playerContext = new(playerTransform, headOffset, footOffset, climbOffset, jumpOffset, moveOffset,isClimbPossible,playerHeight,playerCrouchHeight,playerProneHeight, climbLocOffset);

        animator = GetComponentInChildren<Animator>();
        playerController = GetComponent<CharacterController>();

        AddStates();
        AddSpecialStates();
        currentState = states[EPlayerState.Idle];
    }

    void AddSpecialStates()
    {
        specialStates.Add(EPlayerState.Jump);
        specialStates.Add(EPlayerState.Climb);
        specialStates.Add(EPlayerState.Slide);
    }
    void AddStates()
    {
        states.Add(EPlayerState.Idle, new IdleState(EPlayerState.Idle));
        states.Add(EPlayerState.Walk, new WalkState(EPlayerState.Walk));
        states.Add(EPlayerState.Run, new RunState(EPlayerState.Run));

        states.Add(EPlayerState.Crouch, new CrouchState(EPlayerState.Crouch));
        states.Add(EPlayerState.Jump, new JumpState(EPlayerState.Jump));
        states.Add(EPlayerState.Slide, new SlideState(EPlayerState.Slide));

        states.Add(EPlayerState.Climb, new ClimbState(EPlayerState.Climb));
        states.Add(EPlayerState.Crawl, new CrawlState(EPlayerState.Crawl));
    }

    
}
