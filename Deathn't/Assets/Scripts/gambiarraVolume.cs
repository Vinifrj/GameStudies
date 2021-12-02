using UnityEngine;

public class gambiarraVolume : MonoBehaviour {

    private AudioSource audioSrc;

	void Start () {

        audioSrc = GetComponent<AudioSource>();

        audioSrc.volume = BGM.instance.GetBackgrounVolume();
	}
}
