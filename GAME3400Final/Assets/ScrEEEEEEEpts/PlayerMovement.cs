using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0;
    public float gravity = 9.81f;
    public float jumpHeight = 5;


    public Camera fpsCam;

    public StepSounds ss;

    private Vector3 moveDirection;
    private Vector3 input;
    private CharacterController _controller;

    private bool hitGround;

    // Start is called before the first frame update
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        this.hitGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CameraRevolve.active)
        {
            Move();
        }
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = transform.right * moveHorizontal + transform.forward * moveVertical;
        input *= moveSpeed;


        if (_controller.isGrounded)
        {
            //jump
            moveDirection = input;
            // jump height = .5 * v(0)^2 / g

            if (Input.GetButton("Jump"))
            {
                // jump
                // this.hitGround = false;
                // moveDirection.y = Mathf.Sqrt(2 * gravity * jumpHeight);
            }
            else
            {
                // ground the object
                if (!this.hitGround)
                {
                    this.hitGround = true;
                  
                }
                moveDirection.y = -3.0f;
            }
        }
        else
        {
            //mid-air
            moveDirection.y -= gravity * Time.deltaTime;
        }
        this.ss.active = this._controller.isGrounded && moveDirection != Vector3.down * 3;
        _controller.Move(moveDirection * Time.deltaTime);
    }

}
