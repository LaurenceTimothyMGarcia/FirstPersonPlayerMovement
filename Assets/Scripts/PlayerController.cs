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
        [SerializeField] private InputController input;

        private Vector2 currentMovement;
        private bool movementPressed;
        private bool runPressed;

        //
        void Awake()
        {
            input = new InputController();

            //Sets player input values with listeners
            input.Player.Move.performed += ctx => {
                //Reads the current stick/wasd value
                currentMovement = ctx.ReadValue<Vector2>();
                //Sets boolean if stick is moved on either axis
                movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
            };
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            
        }

        void Rotation()
        {

        }

        void Movement()
        {
            //Set player to walk if true
            if (movementPressed)
            {

            }

            //Stops player from walking
            if (!movementPressed)
            {

            }
        }


        //When script is enabled and disabled
        void OnEnable()
        {
            // Enables character controls action map
            input.Player.Enable();
        }

        void OnDisable()
        {
            // Disables character controls action map
            input.Player.Disable();
        }
    }
}

