using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : AbstratMovement
{
    [SerializeField] CollisionRegister groundCheck = null;
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpSpeed = 1f;
    [Header("Non-newtonian jump variables")]
    [SerializeField] [Range(0.1f, 5f)] float lowJumpMultiplier = 1;
    [SerializeField] [Range(0.1f, 5f)] float fallMultiplier = 1;

    public override bool Jump()
    {
        jumpInput = true;        
        return false;
    }

    public override void StepSimulation(float deltaTime)
    {
        
        rb.velocity = new Vector2(velocityInput.x * speed, rb.velocity.y);
        
        //if(rb.SweepTest())
        //{

        //}

        // jump
        if (groundCheck != null && jumpInput && groundCheck.isColliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        // Non-newtoniam jump logic
        if (rb.velocity.y <= 0)
        {
            rb.velocity += Physics2D.gravity * (fallMultiplier - 1) * deltaTime;
        }
        else if (rb.velocity.y > 0 && !jumpInput)
        {
            rb.velocity += Physics2D.gravity * (lowJumpMultiplier - 1) * deltaTime;
        }
    }
}
