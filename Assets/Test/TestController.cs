using UnityEngine;

public class TestController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float zInput;
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        zInput = Input.GetAxis("Vertical");
        animator.SetFloat("verI", zInput);
    }
    private void FixedUpdate()
    {
        transform.Translate(0,0,zInput * Time.deltaTime * speed);
    }
}
