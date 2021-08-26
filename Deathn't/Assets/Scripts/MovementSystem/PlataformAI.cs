using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformAI : MonoBehaviour
{
    [SerializeField] AbstratMovement movementBehav = null;
    Vector2 currDir = Vector2.down;
    [SerializeField] Vector2 startingDir = Vector2.down;
    [SerializeField] CustomCheck[] checks = new CustomCheck[0];
    PossessionTarget possessionTarget = null;

    private void Awake()
    {
        possessionTarget = GetComponent<PossessionTarget>();
    }

    private void OnEnable()
    {
        currDir = startingDir;
    }

    private void Update()
    {
        if (possessionTarget && possessionTarget.enabled) return;
        foreach (CustomCheck c in checks)
        {
            if (c.Checks())
            {
                currDir = c.newDir;
            }
        }
        movementBehav.ClearInputs();
        movementBehav.Move(currDir.normalized);
    }    
}
