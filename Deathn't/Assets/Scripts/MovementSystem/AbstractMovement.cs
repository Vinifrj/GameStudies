using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class AbstratMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 velocityInput { get; protected set; }
    public bool jumpInput { get; protected set; }
    [SerializeField]
    private UpdateType updateType = UpdateType.LateUpdate;
    public bool holdSimulation = false;

    enum UpdateType
    {
        LateUpdate,
        FixedUpdate
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Move(Vector2 input)
    {
        velocityInput = input;
    }

    // returns true if the character jumped 
    public virtual bool Jump()
    {
        jumpInput = true;
        return false;
    }
    public virtual void StepSimulation(float deltaTime)
    {
        rb.velocity = velocityInput;        
    }

    void LateUpdate()
    {
        if (!holdSimulation && updateType == UpdateType.LateUpdate)
        {
            StepSimulation(Time.deltaTime);
            
        }
    }

    void FixedUpdate()
    {
        if (!holdSimulation && updateType == UpdateType.FixedUpdate)
        {
            StepSimulation(Time.fixedDeltaTime);
           
        }
    }

    public void ClearInputs()
    {
        velocityInput = Vector2.zero;
        jumpInput = false;
    }

}
