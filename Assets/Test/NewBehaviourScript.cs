using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Animator animator;
    public GameObject objPrefab;
    private Vector3 HeadPos;
    public float headOffset;
    private void Awake()
    {
 
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        { 
            float x = Input.GetAxis("Horizontal");
            if (x != 0)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    Debug.Log("Slide detected");
                }
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        HeadPos = transform.position;
        HeadPos.y += headOffset;
        if (other.CompareTag("Obstacle"))
        {
            Instantiate(objPrefab,other.ClosestPoint(HeadPos),Quaternion.identity);
        }
    }
}
