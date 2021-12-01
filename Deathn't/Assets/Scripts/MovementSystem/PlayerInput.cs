using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class that reads input and calls functions like move and jump on selected movement script
public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private AbstratMovement targetMovementBehavior = null;

    private void Update()
    {
        targetMovementBehavior.ClearInputs();
        if (Input.GetKey(KeyCode.Space))
        {
            targetMovementBehavior.Jump();
        }

        // move with horizontal input
        float x = Input.GetAxisRaw("Horizontal");
        targetMovementBehavior.HorizontalMove(x);
        //targetMovementBehavior.Move(new Vector2(x,0));
    }
}
