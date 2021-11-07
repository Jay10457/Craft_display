using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tutorial
{

    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        public float Velocity;

        public float InputX;
        public float InputZ;
        public float Speed;
        public float allowPlayerRotation = 0.1f;
        public Vector3 desiredMoveDirection;
        public float desiredRotationSpeed = 0.1f;
        public Animator anim;

        public CharacterController controller;
        public bool isGrounded;

        [Header("Animation Smoothing")]
        [Range(0, 1f)]
        public float HorizontalAnimSmoothTime = 0.2f;
        [Range(0, 1f)]
        public float VerticalAnimTime = 0.2f;
        [Range(0, 1f)]
        public float StartAnimTime = 0.3f;
        [Range(0, 1f)]
        public float StopAnimTime = 0.15f;

        public float verticalVel;
        private Vector3 moveVector;

        // Use this for initialization
        void Start()
        {
            anim = this.GetComponent<Animator>();

            controller = this.GetComponent<CharacterController>();
        }
        private void Update()
        {
            InputNagnitude();
        }
        private void FixedUpdate()
        {
            AnimePlayer();
            Fall();
        }
        private void InputNagnitude()
        {
            InputX = Input.GetAxis("Horizontal");
            InputZ = Input.GetAxis("Vertical");

            Speed = new Vector2(InputX, InputZ).sqrMagnitude;
        }

        private void AnimePlayer()
        {
            if (Speed > allowPlayerRotation)
            {
                anim.SetFloat("Velocity", Speed, StartAnimTime, Time.fixedDeltaTime);
                PlayerMoveAndRotation();
            }
            else if (Speed < allowPlayerRotation)
            {
                anim.SetFloat("Velocity", Speed, StopAnimTime, Time.fixedDeltaTime);
            }
        }
        private void PlayerMoveAndRotation()
        {
            desiredMoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            desiredMoveDirection = Vector3.ClampMagnitude(desiredMoveDirection, 1f);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
            controller.Move(desiredMoveDirection * Time.deltaTime * Velocity);
        }
        private void Fall()
        {
            isGrounded = controller.isGrounded;
            if (isGrounded)
            {
                verticalVel -= 0;
            }
            else
            {
                verticalVel -= 1;
            }
            moveVector = new Vector3(0, verticalVel * .2f * Time.fixedDeltaTime, 0);
            controller.Move(moveVector);
        }
    }
}
