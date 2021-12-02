using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.IO;

public class BGM : MonoBehaviour
{
    public static BGM instance = null;
    //[SerializeField] AudioClip bgmClip = null;
    private AudioSource background;
    private AudioSource death;

    void Awake()
    {
        //Create Singleton instance
        if(!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);

            //AudioSources references
            AudioSource[] a = GetComponentsInChildren<AudioSource>();
            background = a[0];
            death = a[1];

            //start background track
            background.loop = true;
            //background.Play();
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetBackgrounVolume(float v)
    {

        background.volume = v;
        //Debug.Log(background.volume);
    }

    public void SetDeathVolume(float v)
    {
        death.volume = v;
    }

    public float GetBackgrounVolume()
    {
        return background.volume;
        //Debug.Log(background.volume);
    }

    public float GetDeathVolume()
    {
        return death.volume;
    }

    public void PlayDeathClip()
    {
        death.Play();
    }
}
