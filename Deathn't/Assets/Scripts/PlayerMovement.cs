//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerMovement : MonoBehaviour {

//    public float walkSpeed;
//    public CharacterController2D controller;

//    private float walkSense = 0f;
//    private bool jump = false;

//    void Update () {
        
//        walkSense = Input.GetAxisRaw ("Horizontal") * walkSpeed;

//        if (Input.GetButtonDown ("Jump")){

//            jump = true;
//        }

//    }

//    void FixedUpdate () {
        
//        controller.Move (walkSense , jump);
//        jump = false;

//    }
//}
