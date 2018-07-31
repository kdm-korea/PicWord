using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputPos : MonoBehaviour
{
    public SteamVR_TrackedObject leftCtrl, rightCtrl;
    //public GameObject alphabatCollection;
    public Vector3 lastPosition; // stores the left controller's last position
    private float moveTime = 0, acceleration = 0.0f; // stores the controller's speed, move Time
    private bool chkPositiveNum, lastPositiveNum = true;
    private float collectMoveTime = 0;
    private bool chkAlphabatCollection = false, chkCollection = false;
    private GameObject AlphabatCollection;

    void Awake()
    {
        lastPosition.y = Mathf.Abs(leftCtrl.transform.position.y);
    }

    void Update()
    {
        var deviceLeft = SteamVR_Controller.Input((int)leftCtrl.index);
        var deviceRight = SteamVR_Controller.Input((int)rightCtrl.index);
        collectMoveTime = +Time.deltaTime;
        moveTime = +Time.deltaTime;
        if (!chkAlphabatCollection)
        {
            if (lastPosition.y > leftCtrl.transform.position.y)
            {
                acceleration = -leftCtrl.transform.position.y;
                chkPositiveNum = true;
            }
            else if (lastPosition.y < leftCtrl.transform.position.y)
            {
                acceleration = leftCtrl.transform.position.y - lastPosition.y;
                chkPositiveNum = false;
            }
            else
            {
                return;
            }

            lastPosition.y = leftCtrl.transform.position.y;
            lastPositiveNum = chkPositiveNum;

            if (deviceLeft.GetPress(SteamVR_Controller.ButtonMask.Grip))
            {
                if (!chkCollection)
                {
                    AlphabatCollection = GameObject.Find("AlphabatCollection");
                    chkCollection = true;
                }
                AlphabatCollection.transform.Rotate(transform.position * Mathf.Abs(acceleration * 50) / moveTime * Time.deltaTime);
                moveTime = 0;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "AlphabatCollection")
        {
            chkAlphabatCollection = true;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "AlphabatCollection")
        {
            chkAlphabatCollection = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "AlphabatCollection")
        {
            chkAlphabatCollection = false;
        }
    }
}