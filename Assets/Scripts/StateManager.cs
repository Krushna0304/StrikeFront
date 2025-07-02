using System;
using System.Collections.Generic;
using UnityEngine;

public class StateManager<EState> : MonoBehaviour where EState : Enum
{
    public CharacterController playerController;
    public Animator animator;

    protected BaseState<EState> currentState;
    protected EState nextStateID;
    protected Dictionary<EState, BaseState<EState>> states = new Dictionary<EState, BaseState<EState>>();
    protected bool IsTransitioning = false;
    protected void Start()
    {
        currentState.EnterState(this);
    }

    protected void Update()
    {
        nextStateID = currentState.GetNextStateID();

        if (!IsTransitioning && nextStateID.CompareTo(currentState.StateKey) == 0)
        {
            currentState.UpdateState(this);
        }
        else if(!IsTransitioning)
        {
            ChangeState(nextStateID);
        }
    }

    protected void FixedUpdate()
    {
        if (!IsTransitioning)
        {
            currentState.FixedUpdateState(this);
        }
    }


    protected void ChangeState(EState id)
    {
        IsTransitioning = true;
        currentState.ExitState(this);
        currentState = states[id]; 
        currentState.EnterState(this);
        IsTransitioning = false;

    }
    protected void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnterState(other);
    }

    protected void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExitState(other);
    }

    protected void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStayState(other);
    }

    protected void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnterState(collision);
    }
    protected void OnCollisionExit(Collision collision)
    {
        currentState.OnCollisionExitState(collision);
    }
    protected void OnCollisionStay(Collision collision)
    {
        currentState.OnCollisionStayState(collision);
    }

}
