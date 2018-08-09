using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    public void FadeOut(GameObject go){
        go.GetComponent<SteamVR_Fade>();
        SteamVR_Fade.Start(Color.black,2.0f);
    }
    public void FadeIn(GameObject go)
    {
        go.GetComponent<SteamVR_Fade>();
        SteamVR_Fade.Start(Color.clear,2.0f);
    }
}
