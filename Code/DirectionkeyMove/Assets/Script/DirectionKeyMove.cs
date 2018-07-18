using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionKeyMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveDirection();
	}

    private void MoveDirection() {
        float keyHorizontal = Input.GetAxis("Horizontal");
        float ketVertical = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.right * spe)
    }
}
