using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionTarget : MonoBehaviour
{
    [SerializeField] AbstratMovement movementBehav = null;

    private void OnEnable()
    {
        RewindController r = GetComponent<RewindController>();
        if (r) 
        {
            r.StartReplay();
            r.StopReplay(); 
        }
    }

    private void Update()
    {
        movementBehav.ClearInputs();
        if (Input.GetKey(KeyCode.Space))
        {
            movementBehav.Jump();
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        movementBehav.Move(new Vector2(x, y));
    }
}
