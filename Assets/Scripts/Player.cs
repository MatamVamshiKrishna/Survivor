using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 5.0f;

    private Vector3 moveDirection = Vector3.zero;
    private bool isSprinting = false;
    private bool canJump = false;
    private float jumpSpeed = 10;

    // component references
    private CharacterController characterController;
    private Animator animator;
    //private bool canMove = false;
    //private Vector3 movePos = Vector3.zero;


    

    // Start is called before the first frame update
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(characterController.isGrounded);
        //if (characterController.isGrounded)
        {
            moveDirection = new Vector3(0,0, Input.GetAxis("Vertical"));
            transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0));
            moveDirection = transform.TransformDirection(moveDirection);
        }
        

        moveDirection.y -= 9.8f;

        characterController.Move(moveDirection);



        /*if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }*/

        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            //if (characterController.isGrounded)
            {
                Debug.Log("Grounded");
                //canJump = true;
                animator.SetBool("canJump", true);
            }
                
        }*/

        

        // move only x and z axis
       // moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

         // rotate around y axis
        

        // add gravity
        //moveDirection.y -= 9.8f * Time.deltaTime;
        //characterController.Move(moveDirection * speed * Time.deltaTime);
        
        animator.SetFloat("speed", characterController.velocity.magnitude);
        //animator.SetBool("isSprinting", isSprinting);
        
        




        

        
            

       
        
    }
}
