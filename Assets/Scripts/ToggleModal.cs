using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GatorGame {
    public class ToggleModal : MonoBehaviour {    
        public void Toggle() {
            gameObject.SetActive(!gameObject.activeInHierarchy);

            GameObject[] modals = GameObject.FindGameObjectsWithTag("Modal");

            foreach (var modal in modals) {
                if (modal != gameObject && modal.activeSelf) {
                    modal.SetActive(false);
                }
            }
        }

        public void Destroy() {
            Destroy(gameObject);
        }
    }
}

