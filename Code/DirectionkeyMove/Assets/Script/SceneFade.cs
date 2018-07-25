using UnityEngine;

public class SceneFade : MonoBehaviour {
    private float _fadeDuration = 2f;
    
    private void Start() {
        
        //FadeToWhite();
        Invoke("FadeFromWhite", _fadeDuration);
        Debug.Log("FadeFromWhite");
    }

    private void FadeToWhite() {
        //set start color
        SteamVR_Fade.Start(Color.clear, 0f);
        //set and start fade to
        SteamVR_Fade.Start(Color.white, _fadeDuration);
    }

    private void FadeFromWhite() {
        //set start color
        SteamVR_Fade.Start(Color.white, 0f);
        //set and start fade to
        SteamVR_Fade.Start(Color.clear, _fadeDuration);
    }
}