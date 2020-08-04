using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 movement;

    public float speed = 5;
    public float gravity = 15;
    public float jump = 4;

    public float rotationSpeed = 100;
    private float rotation;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {
            movement = new Vector3(0,0,moveVertical);
            movement = this.transform.TransformDirection(movement);

            if (Input.GetButton("Jump"))
            {
               movement.y = jump;
               animator.SetBool("isJumping",true);
            }
            else
            {
                animator.SetBool("isJumping",false);
            }
        }

        if (Input.GetKey(KeyCode.Z))
        {
            animator.SetBool("isFliping",true);
        }
        else
        {
            animator.SetBool("isFliping",false);
        }

        if (moveVertical != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isBoosting",true);
                speed = 10;
            }
            else
            {
                animator.SetBool("isSkating",true);
                speed = 5;
            }
        }
        else
        {
            animator.SetBool("isBoosting",false);
            animator.SetBool("isSkating",false);
        }

        if (Input.GetKey(KeyCode.C))
        {
            animator.SetBool("isCrouching",true);
        }
        else
        {
            animator.SetBool("isCrouching",false);
        }

        rotation += rotationSpeed*moveHorizontal*Time.deltaTime;
        this.transform.eulerAngles = new Vector3(0,rotation,0);

        movement.y -= gravity*Time.deltaTime;
        controller.Move(movement*speed*Time.deltaTime);
    }
}
