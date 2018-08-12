using UnityEngine;
using UnityEngine.SceneManagement;

[SerializeField]
public class E0_SceneMove : MonoBehaviour {

    public void OnTriggerEnter(Collider col)
    {
        string objName = null;

        if(col.gameObject.tag == "CrystalBall")
        {
            
            objName = col.gameObject.name;
            //string objName = col.gameObject.name;
            SceneManager.LoadScene(objName);

            //fader.FadeIn();
            //SceneManager.LoadScene(objName);
            //fader.FadeOut();
        }

    }
}
