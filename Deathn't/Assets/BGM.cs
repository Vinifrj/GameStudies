using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    static BGM instance = null;
    [SerializeField] AudioClip bgmClip = null;

    private void Awake()
    {
        if (instance)
        {
            Destroy(instance);
        }
        instance = this;
        DontDestroyOnLoad(this);
        AudioSource a = gameObject.AddComponent<AudioSource>();
        a.loop = true;
        a.clip = bgmClip;
        a.Play();        
    }
}
