using UnityEngine;

public class gambiarraVolume : MonoBehaviour {

    private AudioSource audioSrc;

	void Start () {

        audioSrc = GetComponent<AudioSource>();

        audioSrc.volume = BGM.instance.GetBackgrounVolume();
	}

  void Update() {
    if(audioSrc.volume != BGM.instance.GetBackgrounVolume())
      audioSrc.volume = BGM.instance.GetBackgrounVolume();
  }

}
