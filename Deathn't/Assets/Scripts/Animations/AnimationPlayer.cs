using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    private Rigidbody2D rb = null;
    private Animator animator = null;
    private SpriteRenderer sprite = null;
    [SerializeField] private CollisionRegister groundCheck= null;

    private void Awake() {
        
        rb = GetComponent <Rigidbody2D> ();
        animator = GetComponent <Animator> ();
        sprite = GetComponent <SpriteRenderer> ();

    }

    void Update () {

        if (rb.velocity.x > 0 && sprite.flipX == true){

            sprite.flipX = false;

        } else if (rb.velocity.x < 0 && sprite.flipX == false){

            sprite.flipX = true;

        }

        animator.SetFloat("Speed", Mathf.Abs (rb.velocity.x));
        animator.SetFloat("JumpSpeed", rb.velocity.y);
        animator.SetBool("Grounded", groundCheck.isColliding);
        
    }
}
