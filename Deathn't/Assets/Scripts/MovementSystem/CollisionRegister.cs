using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionRegister : MonoBehaviour
{
    private List<Collider2D> registeredColliders = new List<Collider2D>();
    [SerializeField]
    private LayerMask collisionLayerMask = default;

    // this will be true if this object's collider is colliding with at least 1 object within the selected layermask
    public bool isColliding = false;

    private void OnEnable()
    {
        registeredColliders = new List<Collider2D>();
        isColliding = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collisionLayerMask == (collisionLayerMask | (1 << collision.gameObject.layer)))
        {
            registeredColliders.Add(collision);
        }
        CheckCollision();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        registeredColliders.Remove(collision);
        CheckCollision();
    }

    void CheckCollision()
    {
        isColliding = registeredColliders.Count > 0;
    }
}
