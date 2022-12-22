using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
    * Player controller
*/

namespace PlayerInput
{
    public class PlayerController : MonoBehaviour
    {

        [Header("Movement")]
        //Base movement speed of the player
        [SerializeField] private float moveSpeed;

        //Friction so the speed doesn't go on forever
        [SerializeField] private float groundDrag;


        [Header("Jumping")]
        //Force needed to jump
        [SerializeField] private float jumpForce;
        //Cooldown between jumps
        [SerializeField] private float jumpCooldown;
        [SerializeField] private float airMultiplier;
        private bool readyToJump;

        [Header("Ground Check")]
        [SerializeField] private float playerHeight;
        [SerializeField] private LayerMask whatIsGround;
        private bool grounded;

        [SerializeField] private Transform orientation;


        private float horizontalInput;
        private float verticalInput;

        private Vector3 moveDirection;

        private Rigidbody rb;

        // Start is called before the first frame update
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            readyToJump = true;
        }

        private void Update()
        {

            //Ground check in order to see if player is touching ground
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

            Input();
            SpeedControl();

            //Add in the drag
            if (grounded)
            {
                rb.drag = groundDrag;
            }
            else
            {
                rb.drag = 0;
            }
        }

        //Any physics calculations go here
        private void FixedUpdate()
        {
            MovePlayer();
        }

        //Takes and holds input values from the instances
        private void Input()
        {
            horizontalInput = PlayerInputManager.Instance.getMovement().x;
            verticalInput = PlayerInputManager.Instance.getMovement().y;

            if (PlayerInputManager.Instance.jumpPressed() && readyToJump && grounded)
            {

                readyToJump = false;

                Jump();

                //Resets ready to jump to true after certain amount of time
                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }

        //Moves the player
        private void MovePlayer()
        {
            //Calculate movement direction 
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            //On ground
            if (grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
            //in air
            else if (!grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            }
        }

        //Limits speed of player
        private void SpeedControl()
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            //limit velocity if needd
            //If faster than movement speed, calculate max velcoty then apply it.
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }


        //Jump function
        private void Jump()
        {
            //Reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            readyToJump = true;
        }
    }
}

