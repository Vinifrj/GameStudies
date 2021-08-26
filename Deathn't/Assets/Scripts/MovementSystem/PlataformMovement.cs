using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformMovement : AbstratMovement
{
    [SerializeField] float baseSpeed = 1f;
    [SerializeField] DirectionalSpeed[] dirSpeedMultipliers = new DirectionalSpeed[0];

    public override void StepSimulation(float deltaTime)
    {
        base.StepSimulation(deltaTime);
        rb.velocity *= baseSpeed;
        foreach(DirectionalSpeed d in dirSpeedMultipliers)
        {
            if (velocityInput == d.dir)
            {
                rb.velocity *= d.speedMultiplier;
            }
        }
        rb.velocity -= Physics2D.gravity * deltaTime;
    }

    [System.Serializable]
    class DirectionalSpeed
    {
        public Vector2 dir = Vector2.zero;
        public float speedMultiplier = 1f;
    }
}
