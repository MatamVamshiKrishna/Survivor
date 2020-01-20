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
    private HealthSystem healthSystem;
    private HungerSystem hungerSystem;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        healthSystem = GetComponent<HealthSystem>();
        hungerSystem = GetComponent<HungerSystem>();
        hungerSystem.SetHealthSystem(healthSystem);
    }

    void Update()
    {
        speed = walkSpeed;
        canSprint = false;
        canJump = false;

        if(characterController.isGrounded)
        {
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if(Input.GetMouseButtonDown(0))
            {
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Obstacle"))
        {
            var obstacle = hit.gameObject.GetComponent<Obstacle>();
            if(obstacle)
            {
                healthSystem.DecreaseHealth(obstacle.health);
            }
        }

        if(hit.gameObject.CompareTag("Food"))
        {
            var food = hit.gameObject.GetComponent<Food>();
            if(food)
            {
                Destroy(hit.gameObject);
                healthSystem.IncreaseHealth(food.health);
                hungerSystem.DecreaseHungerLevel(food.hunger);
            }
        }
    }
}
