using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;               // Новый компонент Animator

    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();  // Получаем Animator из детей
    }

    void Update()
    {
        GroundCheck();
        MovePlayer();
        AnimateMovement();
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;
    }

    private void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void AnimateMovement()
    {
        // Параметр "Speed" ожидает значение от 0 до 1 или больше для Blend Tree
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float inputMagnitude = new Vector2(x, z).magnitude;

        // Устанавливаем параметр Speed для Animator
        animator.SetFloat("Speed", inputMagnitude);
    }
}
