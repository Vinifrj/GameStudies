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
        //Uses ray cart to see if there will be a collision
        RaycastHit2D lowHit;
        RaycastHit2D highHit;
        //Left
        if(input > 0)
        {
            //Raycast using the lower bound of the collisor
            lowHit = Physics2D.Raycast(rb.position+new Vector2(+CapsuleCollider.size.x/2,-CapsuleCollider.size.y/2),Vector2.right);
            //Raycast using the upper bound of the collisor
            highHit = Physics2D.Raycast(rb.position+new Vector2(+CapsuleCollider.size.x/2,+CapsuleCollider.size.y/2*0.65f),Vector2.right);
        }
        //Right
        else
        {
            //Raycast using the lower bound of the collisor
            lowHit = Physics2D.Raycast(rb.position+new Vector2(-CapsuleCollider.size.x/2,-CapsuleCollider.size.y/2),Vector2.left);
            //Raycast using the upper bound of the collisor
            highHit = Physics2D.Raycast(rb.position+new Vector2(-CapsuleCollider.size.x/2,+CapsuleCollider.size.y/2*0.65f),Vector2.left);
        }
        
        if (lowHit.collider != null && highHit.collider != null)
        {
            //If there is a collision, and it is not a trigger collision, turns the horizontal velocity 0
            if(((lowHit.distance < 0.03f && lowHit.distance > -0.03f && !lowHit.collider.isTrigger) || (highHit.distance < 0.03f && highHit.distance > -0.03f) && !highHit.collider.isTrigger))
            {
                velocityInput = new Vector2(0f,velocityInput.y);
            }
            else
            {
                if(lowHit.collider.isTrigger)
                {
                    OneWayDoor OWD = lowHit.collider.GetComponent<OneWayDoor>();
                    if(OWD != null && lowHit.distance < 0.03f && lowHit.distance > -0.03f)
                    {
                        if(input > 0)
                        {
                            if(OWD.leftToRight)
                            {
                                velocityInput = new Vector2(input,velocityInput.y);
                            }
                            else
                            {
                                velocityInput = new Vector2(0f,velocityInput.y);
                            }
                        }
                        else
                        {
                            if(OWD.rightToLeft)
                            {
                                velocityInput = new Vector2(input,velocityInput.y);
                            }
                            else
                            {
                                velocityInput = new Vector2(0f,velocityInput.y);
                            }
                        }
                    }
                    else
                    {
                         velocityInput = new Vector2(input,velocityInput.y);
                    }
                }
                else if(highHit.collider.isTrigger )
                {
                    OneWayDoor OWD = highHit.collider.GetComponent<OneWayDoor>();
                    if(OWD != null && highHit.distance < 0.03f && highHit.distance > -0.03f)
                    {
                        if(input > 0)
                        {
                            if(OWD.leftToRight)
                            {
                                velocityInput = new Vector2(input,velocityInput.y);
                            }
                            else
                            {
                                velocityInput = new Vector2(0f,velocityInput.y);
                            }
                        }
                        else
                        {
                            if(OWD.rightToLeft)
                            {
                                velocityInput = new Vector2(input,velocityInput.y);
                            }
                            else
                            {
                                velocityInput = new Vector2(0f,velocityInput.y);
                            }
                        }
                    }
                    else
                    {
                         velocityInput = new Vector2(input,velocityInput.y);
                    }
                }
                else
                {
                    velocityInput = new Vector2(input,velocityInput.y);
                }

            }
        }
        else
        {
            velocityInput = new Vector2(input,velocityInput.y);
        }
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
