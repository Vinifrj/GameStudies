using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaAI : MonoBehaviour
{

    [SerializeField] AbstratMovement movementBehav = null;
    [SerializeField] Vector2 currDir = Vector2.left;
    [SerializeField] CustomCheck[] checks = new CustomCheck[0];
    PossessionTarget possessionTarget = null;

    private void Awake()
    {
        possessionTarget = GetComponent<PossessionTarget>();
    }

    void FixedUpdate()
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

[System.Serializable]
class CustomCheck
{
    [SerializeField] CollisionRegister check = null;
    [SerializeField] bool state = true;
    public Vector2 newDir = Vector2.zero;

    public bool Checks()
    {
        if (check == null) return false;
        return (check.isColliding == state);
    }
}
