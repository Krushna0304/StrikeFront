using UnityEngine;
using System;
public abstract class BaseState<EState> : MonoBehaviour where EState : Enum
{
    public EState StateKey;
    public BaseState(EState key){
        StateKey = key;
    }
    public abstract void EnterState(StateManager<EState> stateManager);
    public abstract void UpdateState(StateManager<EState> stateManager);
    public abstract void FixedUpdateState(StateManager<EState> stateManager);
    public abstract void ExitState(StateManager<EState> stateManager);

    public abstract EState GetNextStateID();
    public abstract void OnTriggerEnterState(Collider other);
    public abstract void OnTriggerExitState(Collider other);
    public abstract void OnTriggerStayState(Collider other);
    public abstract void OnCollisionEnterState(Collision other);
    public abstract void OnCollisionExitState(Collision other);
    public abstract void OnCollisionStayState(Collision other);


}
