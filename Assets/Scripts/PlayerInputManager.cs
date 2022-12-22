using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*** 
 * Script holder that manages boolean values for each movement state
***/

namespace PlayerInput
{
    public class PlayerInputManager : MonoBehaviour
    {
        //Singleton instance of the input manager
        public static PlayerInputManager Instance = null;

        //Input controller
        [SerializeField] private InputController input;

        //Basic Movement
        private Vector2 movement;
        private bool movementPressed;

        //Looking
        private Vector2 looking;
        private bool lookingPressed;

        //Jumping Movement
        private bool jump;
        private bool wallJump;

        //Singleton for only one Input Manager
        private void Awake()
        {
            //Initialize singleton
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != null)
            {
                Destroy(this.gameObject);
            }

            //Initialize Input
            input = new InputController();

            //Sets player input values with listeners
            input.Player.Move.performed += ctx => {
                //Reads the current stick/wasd value
                movement = ctx.ReadValue<Vector2>();
                //Sets boolean if stick is moved on either axis
                movementPressed = movement.x != 0 || movement.y != 0;
            };


            //Deals with player looking around
            input.Player.Look.performed += ctx => {
                //Reads current stick/mouse value
                looking = ctx.ReadValue<Vector2>();
                //Sets boolean if stick/mouse is looking around
                lookingPressed = looking.x != 0 || looking.y != 0;
            };
        }


        //Returns the current value of the player movement
        public Vector2 getMovement()
        {
            return movement.normalized;
        }

        //Returns value if the movement stick was moved or not
        public bool isMovePressed()
        {
            return movementPressed;
        }

        //Returns looking value from player
        public Vector2 getLooking()
        {
            return looking;
        }
        //Looking but just 1 -1 or 0
        public Vector2 getLookingRaw()
        {
            if (lookingPressed)
            {
                if (looking.x < 0)
                {
                    looking.x = -1;
                }
                else if (looking.x > 0)
                {
                    looking.x = 1;
                }

                if (looking.y < 0)
                {
                    looking.y = -1;
                }
                else if (looking.x > 0)
                {
                    looking.y = 1;
                }
            }
            else
            {
                looking = new Vector2(0,0);
            }

            return looking;
        }
        public bool isLookingPressed()
        {
            return lookingPressed;
        }


        //When script is enabled and disabled
        private void OnEnable()
        {
            input.Player.Enable();
        }
        void OnDisable()
        {
            input.Player.Disable();
        }
    }
}
