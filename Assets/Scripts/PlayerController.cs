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
        [SerializeField] private float moveSpeed;

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
        }

        private void Update()
        {
            Input();
            MovePlayer();
        }

        private void Input()
        {
            horizontalInput = PlayerInputManager.Instance.getMovement().x;
            verticalInput = PlayerInputManager.Instance.getMovement().y;
        }

        private void MovePlayer()
        {
            //Calculate movement direction 
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
    }
}

