using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlockWall : MonoBehaviour {

    public GameObject Camera;
    public Image FadeObj;
    ChagePosition changePosition;
    M0_SceneMove sceneMove;

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("private void OnCollisionEnter(Collision col)");
        if (col.gameObject.tag == "Wall")
        {
            
            changePosition = new ChagePosition();
            changePosition.ChangePos(Camera, 0, 0.04f, 0);

        }else if(col.gameObject.tag == "Door")
        {
            sceneMove.ChangeScence(col.gameObject.name);
        }
    }
}
