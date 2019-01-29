using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GatorGame {
    enum Gender {
        female, male
    }

    public class Alligator {

        private string gatorName;

        private float gatorAge;

        private Gender gatorGender;

        // Measurements
        private float gatorWeight;
        private float gatorLength;
        private float gatorGirth;

        // private Dictionary<string, float> gatorMeasurements =
        //     new Dictionary<string, float> {"weight", weight};

        // public Dictionary<string, float> gatorMeasurements {
        //     get { return }
        // } 

        // Health
        private int gatorHp;
        private int gatorHunger;
        private int gatorThirst;
        private int gatorTemperature;

        private List<Sickness> gatorSicknesses;

        // Stress
        private float gatorCrowding;
        private float gatorEnvironmentalDisturbance;
        private float gatorInteractionDisturbance;

        // Hide
        private float gatorHideQuality;
        private HidePattern gatorHidePattern;

        // Meat
        private float gatorMeatQuality;
        private float gatorMeatAmountEstimate;
        private float gatorMeatAmount; //gatorWeight * .4157

        private List<Bonus> gatorBonuses;

        // Genes could be a function of the standard deviation in relation to a specific gator.
        
        
    }
}
