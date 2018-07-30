using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObj : MonoBehaviour
{
    private GameObject gmObj;

    bool ChkInAlphabat = false;

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Start()
    {
        trackedObj = GetComponentInParent<SteamVR_TrackedObject>();
    }

    private void Update()
    {
        if (Controller.GetHairTriggerDown())
        {
            ChkInAlphabat = true;
        }
        else if (Controller.GetHairTriggerUp())
        {
            ChkInAlphabat = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Alphabat")
        {
            col.gameObject.GetComponent<Outline>().enabled = true;
            if (ChkInAlphabat)
            {
                ChooseObj(col.gameObject.name);
            }
            ChkInAlphabat = false;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Alphabat")
        {
            col.gameObject.GetComponent<Outline>().enabled = true;
            if (ChkInAlphabat)
            {
                ChooseObj(col.gameObject.name);
            }
            ChkInAlphabat = false;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Alphabat")
        {
            ChkInAlphabat = false;
            col.GetComponent<Outline>().enabled = false;
        }
    }
    

    private void ChooseObj(string spelling)
    {
        Debug.Log(WordManager.GetInstance.ChkBoardInWord);
        if (WordManager.GetInstance.ChkBoardInWord)
        {
            gmObj = GameObject.Find("TestSpeechNText");
            gmObj.GetComponent<M3_HitEvent>().ThrowAlphabat(spelling);
        }
    }
}
