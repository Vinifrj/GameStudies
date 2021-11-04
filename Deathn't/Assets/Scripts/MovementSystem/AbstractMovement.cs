using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class AbstratMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 velocityInput { get; protected set; }
    public bool jumpInput { get; protected set; }
    
    [SerializeField]
    private CapsuleCollider2D CapsuleCollider;

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

    public virtual void HorizontalMove(float input)
    {
        Debug.Log(CapsuleCollider.size);
        RaycastHit2D hit;
        // horizontal input.
        

        if(input > 0)
            hit = Physics2D.Raycast(rb.position+new Vector2(+CapsuleCollider.size.x/2,-CapsuleCollider.size.y/2),Vector2.right);
        else
            hit = Physics2D.Raycast(rb.position+new Vector2(-CapsuleCollider.size.x/2,-CapsuleCollider.size.y/2),Vector2.left);

        //if(input > 0)
        //    hit = Physics2D.Raycast(rb.position+new Vector2(+0.176f,-0.35f),Vector2.right);
        //else
        //    hit = Physics2D.Raycast(rb.position+new Vector2(-0.176f,-0.35f),Vector2.left);
        if (hit.collider != null)
        {
            if(hit.distance < 0.03f && hit.distance > -0.03f)
                velocityInput = new Vector2(0f,velocityInput.y);
            else
                velocityInput = new Vector2(input,velocityInput.y);
        }
        else
        {
            velocityInput = new Vector2(input,velocityInput.y);
        }
        //Debug.Log(hit.distance);

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
        //velocityInput = Vector2.zero;
        jumpInput = false;
    }

}
