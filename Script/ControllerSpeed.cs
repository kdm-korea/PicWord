using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSpeed : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    Vector3 tmpPos;

    public  SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Start()
    {
        trackedObj = GetComponentInParent<SteamVR_TrackedObject>();
        tmpPos = trackedObj.transform.position;
    }

	// Update is called once per frame
	void Update () {
        tmpPos =- trackedObj.transform.position;
        Debug.Log(tmpPos = -trackedObj.transform.position);
    }
}
