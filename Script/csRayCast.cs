using UnityEngine;

public class csRayCast : MonoBehaviour
{
    private GameObject gmObj, tmpObj, gm;

    private float speed = 5.0f;

    private bool ChkTrigger = false;

    private SteamVR_TrackedObject trackedObj;

    RaycastHit hit;

    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Start()
    {
        trackedObj = GetComponentInParent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        float amtMove = speed * Time.deltaTime;
        float hor = Input.GetAxis("Horizontal");

        if (Controller.GetHairTriggerDown())
        {
            ChkTrigger = true;
        }
        else if (Controller.GetHairTriggerUp())
        {
            ChkTrigger = false;
        }


        transform.Translate(Vector3.right * hor * amtMove);
        Debug.DrawRay(transform.position, transform.right * 8, Color.red);

        if (Physics.Raycast(transform.position, transform.right, out hit, 8) && hit.collider.gameObject.tag == "Word")
        {
            if (!WordManager.GetInstance.ChkBoardInWord)
            {
                hit.collider.gameObject.GetComponent<Outline>().enabled = true;
                if (tmpObj != hit.collider.gameObject && tmpObj != null)
                {
                    tmpObj.GetComponent<Outline>().enabled = false;
                }
                tmpObj = hit.collider.gameObject;
            }
            
            if (ChkTrigger && !WordManager.GetInstance.ChkBoardInWord)
            {
                //hit.collider.gameObject.GetComponent<>().;
                hit.collider.gameObject.GetComponent<ExampleClass>().MakeObj();
                hit.collider.gameObject.GetComponent<ExampleClass>().enabled = true;
                gmObj = GameObject.Find("TestSpeechNText");
                gmObj.GetComponent<M3_HitEvent>().ThrowWordInBoard(hit.collider.gameObject.name);
                WordManager.GetInstance.ChkBoardInWord = true;
                ChkTrigger = false;
            }
        }
    }

}