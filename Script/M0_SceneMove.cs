using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class M0_SceneMove : MonoBehaviour {
    Fade fade;
    ChagePosition changePos;
    private GameObject VRCamera;

    private void OnTriggerEnter(Collider col)
    {
        changePos = new ChagePosition();
        if(col.gameObject.tag == "CrystalBall")
        {
            VRCamera = GameObject.Find("[CameraRig]");
            //fade.FadeOut();
            ChangeScence(col.gameObject.name);
            //fade.FadeIn();
            
        }
    }
    public void ChangeScence(string objName)
    {
        switch (objName)
        {
            case "Main":
                //changePos.ChangeCameraPos(VRCamera, -200.55f, 5.03f,  -180.81f);
                changePos.ChangeCameraPos(VRCamera, -200.55f, 2f, -180.81f);
                break;
            case "ElementaryRoom":
                changePos.ChangeCameraPos(VRCamera, 0f, 0.04f, 0f);
                break;
            default:
                break;
        }
        //SteamVR_Fade.Start(Color.black, 2f);
        SceneManager.LoadScene(objName);
        Debug.Log(objName);
        //SteamVR_Fade.Start(Color.clear, 2f);
    }
}


