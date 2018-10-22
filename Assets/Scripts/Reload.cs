using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour {
    [SerializeField] private SceneController controller;
    public AudioSource reload;
    public void OnMouseDown() {
        reload.Play();
        controller.ammo = controller.ammo + 1;
        controller.ammoLabel.text = "Ammo: " + controller.ammo;
    }

}
