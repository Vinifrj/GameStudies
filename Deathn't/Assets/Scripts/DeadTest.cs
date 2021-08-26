using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DeadTest : MonoBehaviour
{
    [SerializeField] MonoBehaviour layer = null;

    // Update is called once per frame
    void Update()
    {
        if(AlivePlayer.instance)
        {
            layer.enabled = AlivePlayer.instance.died;
        }
    }
}
