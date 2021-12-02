
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorAnimationScript : MonoBehaviour
{
  public Animator animator;
  private void OnTriggerEnter2D(Collider2D collision)
  {
      if(collision.gameObject.tag == "Player")
        animator.SetBool("Near", true);
  }
}
