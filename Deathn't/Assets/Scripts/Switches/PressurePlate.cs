using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] CollisionRegister collisionCheck = null;
    [SerializeField] MonoBehaviour[] triggerON = null;
    [SerializeField] MonoBehaviour[] triggerOFF = null;


    // Update is called once per frame
    void Update()
    {
        bool check = collisionCheck.isColliding;
        foreach(MonoBehaviour mb in triggerON)
        {
            mb.enabled = check;
        }
        foreach (MonoBehaviour mb in triggerOFF)
        {
            mb.enabled = !check;
        }
    }
}
