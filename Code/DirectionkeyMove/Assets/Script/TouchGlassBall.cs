using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchGlassBall : MonoBehaviour {
    public GameObject respawnPrefab;
    public GameObject respawn;

    public void SceneChage() {
        if (respawn == null) {
            //respawn = GameObject.FindWithTag
        }
        SceneManager.LoadScene("");
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