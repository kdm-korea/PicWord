using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesrScript : MonoBehaviour {
    TextSandboxStreaming tts;
    // Use this for initialization
    void Start () {
         tts = new TextSandboxStreaming();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A)) {
            tts.TextToSpeech(AudioClip,"Who Are you");
        }
	}


    private void AudioClip(AudioClip clip) {
        if (Application.isPlaying && clip != null) {
            GameObject audioObject = new GameObject("AudioObject");
            AudioSource source = audioObject.AddComponent<AudioSource>();
            source.spatialBlend = 0.0f;
            source.loop = false;
            source.clip = clip;
            source.Play();

            Destroy(audioObject, clip.length);
        }
    }
    
}
