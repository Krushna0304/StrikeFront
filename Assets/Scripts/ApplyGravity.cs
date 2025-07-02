using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyGravity : MonoBehaviour
{
    private float u = 0f;
    private bool isGrounded = true;
    private float gravity = 981f;
    private float dist;
    private float minDistCheck = 0.96f;
    public LayerMask Floor;
    private CharacterController playerController;
    int cnt = 0;
    private void Awake()
    {
        playerController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        isGrounded  = Physics.CheckSphere(transform.position,minDistCheck,Floor);
        if (!isGrounded)
        {
            cnt++;
            dist = u * Time.fixedDeltaTime + 0.5f * gravity * Mathf.Pow(Time.fixedDeltaTime, 2);
            playerController.Move(dist * Vector3.down * Time.fixedDeltaTime);
            u = dist /Time.fixedDeltaTime;
        }
        else
        {
            //Debug.Log("Grounded");
        }
    }
}
