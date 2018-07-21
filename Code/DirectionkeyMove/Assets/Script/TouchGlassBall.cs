using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchGlassBall : MonoBehaviour {
    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;

    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void SetColldingObject(Collider col) {
        if (collidingObject || !col.GetComponent<Rigidbody>()) {
            return;
        }
        collidingObject = col.gameObject;
    }

    public void OnTriggerEnter(Collider other) {
        SetColldingObject(other);
    }

    public void OnTriggerStay(Collider other) {
        SetColldingObject(other);
    }

    public void OnTriggerExit(Collider other) {
        if (!collidingObject) {
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

    private void GlassBall() {
        
    }

    private void ChangeScene(string sceneName) { //Scene Move to SceneName
        //effect function
        SceneManager.LoadScene(sceneName);  //Chaneg Scene
    }

    void Update() {
        if (Controller.GetHairTriggerDown()) {
            if (collidingObject) {
                GrabObject();
            }
        }
        else if (Controller.GetHairTriggerUp()) {
            if (objectInHand) {
                RelaseObject();
            }
        }
    }
}