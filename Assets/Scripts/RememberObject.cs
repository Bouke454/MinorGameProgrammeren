using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RememberObject : MonoBehaviour {

public void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

}
