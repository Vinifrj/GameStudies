using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    void Awake()
    {
        //Volume Slider synchronization
        Slider sld = GetComponentInChildren<Slider>();
        sld.value = BGM.instance.GetBackgrounVolume();
        if(sld.value != BGM.instance.GetDeathVolume())
        {
            BGM.instance.SetDeathVolume(sld.value);
        }
    }

    //Change Volume
    public void BackgroundVolume(float x)
    {
        BGM.instance.SetBackgrounVolume(x);
    }

    public void DeathVolume(float x)
    {
        BGM.instance.SetDeathVolume(x);
    }
}
