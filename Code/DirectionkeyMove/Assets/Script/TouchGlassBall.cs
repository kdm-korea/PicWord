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
        Debug.Log(other);

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

//객체를 마우스로 이동시키고 싶을 때, 사용하기 편하게 해주는 스크립트
//스크립트 파일을 원하는 객체에 드래그

//public class draggrameobject : monobehaviour {
//    ienumerator onmousedown() {
//        vector3 scrspace = camera.main.worldtoscreenpoint(transform.position);
//        vector3 offset = transform.position - camera.main.screentoviewportpoint(new vector3(input.mouseposition.x, input.mouseposition.y, input.mouseposition.z));
//        while (input.getmousebutton(0)) {
//            vector3 curscreenspace = new vector3(input.mouseposition.x, input.mouseposition.y, input.mouseposition.z);

//            vector3 curposition = camera.main.screentoviewportpoint(curscreenspace) + offset;
//            transform.position = curposition;
//            yield return null;
//        }
//    }
//}