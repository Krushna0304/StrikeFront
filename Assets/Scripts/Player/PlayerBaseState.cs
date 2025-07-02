using UnityEngine;
using System.Collections.Generic;

public abstract class PlayerBaseState : BaseState<PlayerStateManager.EPlayerState>
{
    protected PlayerStateManager.EPlayerState nextStateId;

    public PlayerBaseState(PlayerStateManager.EPlayerState key):base(key) { }

    protected bool IsCrouchPossible()
    {
        float pivot = PlayerStateManager.Instance.playerContext.footOffset;
        float dist = PlayerStateManager.Instance.playerContext.playerCrouchHeight;
        Vector3 dir = Vector3.up;
        if (!CheckInterrupt(pivot, dist, dir))
        {
            return false;
        }
        return true;
    }
    protected bool IsCrawlPossible()
    {
        float pivot = PlayerStateManager.Instance.playerContext.footOffset;
        float dist = PlayerStateManager.Instance.playerContext.playerHeight/2 + PlayerStateManager.Instance.playerContext.moveOffset;
        Vector3 dir = PlayerStateManager.Instance.playerTransform.forward;
        Vector3 backDir = -dir;
        if (!(CheckInterrupt(pivot, dist, dir) && CheckInterrupt(pivot, dist, backDir)))
        {
            return false;
        }
        return true;
    }
    protected bool IsJumpPossible()
    {
        float pivot = 0;
        float dist = PlayerStateManager.Instance.playerContext.playerHeight / 2 + PlayerStateManager.Instance.playerContext.moveOffset;
        Vector3 dir = PlayerStateManager.Instance.playerTransform.forward;
        Vector3 backDir = -dir;
        if (!(CheckInterrupt(pivot, dist, dir) || CheckInterrupt(pivot, dist, backDir)))
        {
            return false;
        }
        return true;
    }
    protected bool IsClimbPossible()
    {
        //If any obstacle above the player return false
        float pivot = PlayerStateManager.Instance.playerContext.headOffset;
        float dist = PlayerStateManager.Instance.playerContext.playerHeight;
        Vector3 dir = Vector3.up;
      
        if (!CheckInterrupt(pivot,dist,dir))  {
            Debug.Log("Vertical Interrupt detected");
            return false;
        }
        else
        { 
            Debug.Log("no Vertical Interrupt detected");
        }

        return PlayerStateManager.Instance.playerContext.IsClimbPossible;
    }
    protected bool IsMovePossible(Vector3 dir)
    {
        float pivotOffsetX;
        float pivotOffsetY;

        float dist;
        if (nextStateId == PlayerStateManager.EPlayerState.Crawl){

            pivotOffsetY = PlayerStateManager.Instance.playerContext.footOffset;
            if (CheckIsCrawlMoveInForward(nextStateId, dir))
            {
                dist = PlayerStateManager.Instance.playerController.radius * 3 + PlayerStateManager.Instance.playerContext.moveOffset;

                if (!CheckInterrupt(pivotOffsetY, dist, dir))
                {
                    return false;
                }

            }
            else
            {
                //Check From Middle to Side
                dist = PlayerStateManager.Instance.playerController.radius + PlayerStateManager.Instance.playerContext.moveOffset;
                if (!CheckInterrupt(pivotOffsetY, dist, dir))
                {
                    return false;
                }
                else
                { 
                    Debug.Log("Clean Check From Middle");
                }

                //Check From Head to Side
                dist = PlayerStateManager.Instance.playerController.radius + PlayerStateManager.Instance.playerContext.moveOffset;
                pivotOffsetX = dist * 2;
                if (!CheckInterrupt(pivotOffsetY, dist, dir, pivotOffsetX))
                {
                    return false;
                }
                else
                { 
                    Debug.Log("Clean Check From Head");
                }

                //Check From Toe to Side
                dist = PlayerStateManager.Instance.playerController.radius + PlayerStateManager.Instance.playerContext.moveOffset;
                pivotOffsetX = -dist * 2;
                if (!CheckInterrupt(pivotOffsetY, dist, dir, pivotOffsetX))
                {
                    return false;
                }
                else 
                {
                    Debug.Log("Clean Check From Toe");
                }
            }
        }
        else
        {
            pivotOffsetY = 0;
            dist = PlayerStateManager.Instance.playerController.radius  + PlayerStateManager.Instance.playerContext.moveOffset;
            if (!CheckInterrupt(pivotOffsetY, dist, dir))
            {
                return false;
            }
        }

        //Return True is no Interrupt
        return true;
    }

    bool CheckIsCrawlMoveInForward(PlayerStateManager.EPlayerState currentState, Vector3 dir)
    {
        if (currentState == PlayerStateManager.EPlayerState.Crawl)
        {
            if ((dir == PlayerStateManager.Instance.playerTransform.forward) || (dir == -PlayerStateManager.Instance.playerTransform.forward))
            {
                return true ;
            }
            else
            {
                return false;

            }
        }
        return false;
    }

    bool CheckInterrupt(float pivotoffsetY,float dist,Vector3 dir, float pivotoffsetX = 0)
    {
        Vector3 pivotTransform = PlayerStateManager.Instance.playerTransform.position;
        pivotTransform.y += pivotoffsetY;
        pivotTransform.z += pivotoffsetX;

        
        if (Physics.Raycast(pivotTransform, dir,dist,PlayerStateManager.Instance.playerMask))
        {
            return false;
        }

        return true;

    }
}
