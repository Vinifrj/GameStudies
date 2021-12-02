using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class OneWayDoor_Collider : MonoBehaviour
{

    [SerializeField]
    private BoxCollider2D BoxCollider;

    [SerializeField]
    private Sprite SpriteOpen;

    [SerializeField]
    private Sprite SpriteClosed;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !BoxCollider.enabled)
        {
            BoxCollider.enabled = true;
            SpriteRenderer SprRender = GetComponent<SpriteRenderer>();
            if(SprRender != null)
                SprRender.sprite = SpriteClosed;
        }
    }
}
