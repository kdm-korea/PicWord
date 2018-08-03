using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControllerGrabObject : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;

    // 애니메이션
    Animator anim;
    int Idle = Animator.StringToHash("Idle");
    int GrabStickFront = Animator.StringToHash("GrabStickFront");
    // 애니메이션

    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
	
    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        anim = GetComponent<Animator>();
    }

    private void SetCollidingObject(Collider col) {
        if(collidingObject || !col.GetComponent < Rigidbody>() ) {
            return;
        }
        collidingObject = col.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    // 2
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // 3
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject() {
        var joint = AddFixedJoint();
        objectInHand = collidingObject;
        collidingObject = null;
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedJoint() {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void RelaseObject() {
        if (GetComponent<FixedJoint>()) {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }
        objectInHand = null;
    }

    void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetTrigger(Idle);
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger(GrabStickFront);
        }

        if (Controller.GetHairTriggerDown()) {
            Debug.Log("ControllerGrabObject_Update_GetHairTriggerDown");
            if (collidingObject) {
                GrabObject();
            }
        }else if (Controller.GetHairTriggerUp()) {
            if (objectInHand) {
                RelaseObject();
            }
        }
	}
}
