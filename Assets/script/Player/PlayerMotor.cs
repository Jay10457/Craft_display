using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{
    
   public struct State
    {
        public Vector3 position;
        public Vector3 velocity;
        public bool isGrounded;
        public int jumpFrame;
    }
}
