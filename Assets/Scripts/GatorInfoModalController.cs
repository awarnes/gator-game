using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GatorGame {
    public class GatorInfoModalController : MonoBehaviour {

        public AlligatorData currentGatorData;

        private GameObject infoPane;

        private GameObject nameText;
        private GameObject lengthText;
        private GameObject weightText;
        private GameObject girthText;

        void Start() {
            infoPane = GameObject.Find("InfoPane");
            nameText = GameObject.Find("NameText");
            lengthText = GameObject.Find("LengthText");
            weightText = GameObject.Find("WeightText");
            girthText = GameObject.Find("GirthText");
        }

        void Update() {
            nameText.GetComponent<Text>().text = currentGatorData.GatorName;
            lengthText.GetComponent<Text>().text = currentGatorData.measurements.length.ToString();
            weightText.GetComponent<Text>().text = currentGatorData.measurements.weight.ToString();
            girthText.GetComponent<Text>().text = currentGatorData.measurements.girth.ToString();
        }

        public void SetCurrentGatorData(AlligatorData gatorData) {
            currentGatorData = gatorData;
        }

        public void RemoveCurrentGatorData() {
            currentGatorData = null;
        }
    }
}
