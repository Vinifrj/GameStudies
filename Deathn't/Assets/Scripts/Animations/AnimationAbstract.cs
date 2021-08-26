using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAbstract : MonoBehaviour
{
    private Rigidbody2D rb = null;
        private Animator animator = null;
        private SpriteRenderer sprite = null;

    private void Awake() {
        
        rb = GetComponent <Rigidbody2D> ();
        animator = GetComponent <Animator> ();
        sprite = GetComponent <SpriteRenderer> ();
    }

    void Update () {

        if (rb.velocity.x > 0 && sprite.flipX == false){

            sprite.flipX = true;

        } else if (rb.velocity.x < 0 && sprite.flipX == true){

            sprite.flipX = false;

        }
        animator.SetFloat("Speed", Mathf.Abs (rb.velocity.x));
        
    }
}
