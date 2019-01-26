using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleModal : MonoBehaviour
{    
    public void Toggle() {
        gameObject.SetActive(!gameObject.activeInHierarchy);

        GameObject[] modals = GameObject.FindGameObjectsWithTag("Modal");

        foreach (var modal in modals) {
            if (modal != gameObject && modal.activeSelf) {
                modal.SetActive(false);
            }
        }
    }
}
