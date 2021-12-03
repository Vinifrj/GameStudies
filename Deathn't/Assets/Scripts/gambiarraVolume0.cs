using UnityEngine;

public class gambiarraVolume0 : MonoBehaviour {

    private AudioSource audioSrc;

	void Start () {

        audioSrc = GetComponent<AudioSource>();

        audioSrc.volume = BGM.instance.GetBackgrounVolume();
	}

  void Update(){
      audioSrc.volume = BGM.instance.GetBackgrounVolume();
  }
}
