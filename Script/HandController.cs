using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
// 물체를 잡고 던지는 기능 구현
public class HandController : MonoBehaviour {
    // 어느 손인지 결정
    //public OVRInput.Controller ovrController;
    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    // 잡기 기능 활성화
    bool isGrabbing;
    // 잡은 물체
    GameObject grabbedObject;
    // 잡을 수 있는 범위
    public float grabRange = 0.5f;
    // 잡을 수 있는 레이어 마스크 값
    public LayerMask mask;

    // 현재 회전값
    Quaternion currentRotation;
    // 직전 프레임의 회전값
    Quaternion lastRotation;

    // 잡아서 던질때의 힘
    public float ThrowPower = 2;


    // 물체 잡기
    void CatchObject()
    {
        // 잡기 기능 활성화
        isGrabbing = true;
        // 범위에 있는 물체를 다 검색. 조건 지정된 Layer 값만 검색
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hitInfos = Physics.SphereCastAll(ray, grabRange, 0, mask);

        if (hitInfos.Length > 0)
        {
            // 거리가 가장 가까운 물체를 검색
            int closest = 0;
            for (int i = 0; i < hitInfos.Length; i++)
            {
                if (hitInfos[i].distance < hitInfos[closest].distance)
                {
                    closest = i;
                }
            }
            // 가장 가까운 물체를 잡은 물체로 지정
            grabbedObject = hitInfos[closest].transform.gameObject;
            // 물체의 위치를 손의 위치로 지정
            grabbedObject.transform.position = transform.position;
            // 물체의 부모를 손으로 지정
            grabbedObject.transform.parent = transform;
            // 물리 기능 비 활성화
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            grabbedObject.GetComponent<Rigidbody>().useGravity = false;
            
        }
    }

    void DropObject()
    {
        isGrabbing = false;
        // 잡은 물체가 있을경우
        if (grabbedObject != null)
        {
            // 1. root 로 복귀
            grabbedObject.transform.parent = null;
            // Rigidbody 속성 복귀
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Rigidbody>().useGravity = true;

            // rigidbody 의 velocity 를 사용자(touch) 의 손 velocity 로 설정
            grabbedObject.GetComponent<Rigidbody>().velocity = Controller.velocity * ThrowPower;// OVRInput.GetLocalControllerVelocity(ovrController) * ThrowPower;
            grabbedObject.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;// GetAngularVelocity();

            // 잡은 물체 없게 설정
            grabbedObject = null;

            //
        }
    }
    

    // 물체를 잡았을때 손의 회전 변위를 계산 하기 위한 함수
    Vector3 GetAngularVelocity()
    {
        // quaternion 의 변위값 (얼마나 회전했는지)
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);

        return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x),
            Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
    }

    // Update is called once per frame
    void Update () {
        // 잡은 물체가 있을경우 손의 변위 값을 저장한다.
        if(grabbedObject != null)
        {
            lastRotation = currentRotation;
            currentRotation = grabbedObject.transform.rotation;
        }

        // 손을 (LocalAvatar 가 아닐경우) touch 의 위치로 한다.
        //transform.localPosition = OVRInput.GetLocalControllerPosition(ovrController);
        // 손을 (LocalAvatar 가 아닐경우) touch 의 회전으로 한다.
        //transform.localRotation = OVRInput.GetLocalControllerRotation(ovrController);

        // A 버튼을 클릭, 인자로 대응할 컨트롤러 넣어주면 해당 touch 의 입력만 체크
        if (!isGrabbing && Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) // OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, ovrController))
        {
            CatchObject();
            
        }
        else if (isGrabbing && Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) // OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, ovrController))
        {
            DropObject();
        }
    }
}
