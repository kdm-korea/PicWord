using UnityEngine;

public class ChagePosition{
    //public float height; == y
    /// <summary>
    /// Scene 전환시 사용
    /// </summary>
    public void ChangePos (GameObject obj, float x,float y,float z) {
        Vector3 pos = obj.transform.position;
        pos.x = x;
        pos.y = y/100;
        pos.z = z;
        obj.transform.position = pos;
    }

    public void ChangeCameraPos (GameObject obj, float x,float y,float z) {
        Vector3 pos = obj.transform.position;
        pos.x = x;
        pos.y = y;
        pos.z = z;
        obj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        obj.transform.position = pos;
    }



    /// <summary>
    /// 신장을 기준으로 카메라 위치를 선정
    /// </summary>
    public void ChangePos(GameObject obj, float y) {
        Vector3 pos = obj.transform.position;
        pos.y = y / 100;
        obj.transform.position = pos;
    }

    public void ChangeRot(GameObject obj, float x, float z) {
        float y = obj.transform.rotation.y * Time.deltaTime;
        obj.transform.rotation = Quaternion.Euler(x, y, z);
    }
}
