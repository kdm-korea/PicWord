using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingCamera : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(transform);
    }
}
