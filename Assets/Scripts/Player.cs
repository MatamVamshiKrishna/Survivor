using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 10.0f;
    public float sprintSpeed = 20.0f;
    public float rotationSpeed = 30.0f;
    public float jumpSpeed = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private bool canSprint = false;
    private bool canJump = false;
    private float speed;

    // component references
    private CharacterController characterController;
    private Animator animator;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        speed = walkSpeed;
        canSprint = false;
        canJump = false;

       

        //Debug.Log(characterController.isGrounded);

        if(characterController.isGrounded)
        {
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if(Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Space key pressed");
                moveDirection.y = jumpSpeed;
                canJump = true;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
            canSprint = true;
        }

        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * 
            rotationSpeed * Time.deltaTime, 0));
        moveDirection.y -= 9.8f * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);

        var magnitude = new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;
        animator.SetFloat("speed", magnitude);
        animator.SetBool("canSprint", canSprint);
        animator.SetBool("canJump", canJump);
        
    }
}
