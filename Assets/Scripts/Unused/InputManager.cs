using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*** 
 * Script holder that manages boolean values for each movement state
***/

namespace PlayerInput
{
    public class InputManager : MonoBehaviour
    {
        //Singleton instance of the input manager
        public static InputManager Instance = null;

        //Singleton for only one Input Manager
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != null)
            {
                Destroy(this.gameObject);
            }
        }


        //Basic Movement
        private bool moveForward;
        private bool moveBackward;
        private bool moveRight;
        private bool moveLeft;

        //Jumping Movement
        private bool jump;
        private bool wallJump;
    }
}
