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
        private GameObject ageText;
        private GameObject genderText;
        private GameObject stressText;
        private GameObject hitPointsText;
        private GameObject hungerText;
        private GameObject thirstText;
        private GameObject tempText;

        void Start() {
            infoPane = GameObject.Find("InfoPane");
            nameText = GameObject.Find("NameText");
            lengthText = GameObject.Find("LengthText");
            weightText = GameObject.Find("WeightText");
            girthText = GameObject.Find("GirthText");
            ageText = GameObject.Find("AgeText");
            genderText = GameObject.Find("GenderText");
            stressText = GameObject.Find("StressText");
            hitPointsText = GameObject.Find("HitPointsText");
            hungerText = GameObject.Find("HungerText");
            thirstText = GameObject.Find("ThirstText");
            tempText = GameObject.Find("TempText");
        }

        void Update() {
            nameText.GetComponent<Text>().text = currentGatorData.GatorName;
            lengthText.GetComponent<Text>().text = currentGatorData.measurements.length.ToString();
            weightText.GetComponent<Text>().text = currentGatorData.measurements.weight.ToString();
            girthText.GetComponent<Text>().text = currentGatorData.measurements.girth.ToString();
            ageText.GetComponent<Text>().text = currentGatorData.Age.ToString();
            genderText.GetComponent<Text>().text = currentGatorData.Gender.ToString();
            stressText.GetComponent<Text>().text = currentGatorData.stress.CalculateStress();
            hitPointsText.GetComponent<Text>().text = currentGatorData.health.CalculateHP();
            hungerText.GetComponent<Text>().text = currentGatorData.health.CalculateHunger();
            thirstText.GetComponent<Text>().text = currentGatorData.health.CalculateThirst();
            tempText.GetComponent<Text>().text = currentGatorData.health.CalculateTemp();
        }

        public void SetCurrentGatorData(AlligatorData gatorData) {
            currentGatorData = gatorData;
        }

        public void RemoveCurrentGatorData() {
            currentGatorData = null;
        }
    }
}
