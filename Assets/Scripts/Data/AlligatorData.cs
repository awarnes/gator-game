using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GatorGame {
    [System.Serializable]
    public class AlligatorData {
        private string gatorName;

        public string GatorName {
            get { return gatorName; }
            set {
                gatorName = value;
            }
        }

        private float age;

        public float Age {
            get { return age; }
        }

        private Gender gender;

        public Gender Gender {
            get { return gender; }
        }

        public Measurements measurements;

        // Health
        public Health health;

        // Stress
        public Stress stress;

        // Hide
        public Hide hide;

        // Meat
        public Meat meat;

        private List<Bonus> bonuses;

        // Genes could be a function of the standard deviation in relation to a specific gator.
        
        public AlligatorData(string name = null) {
            if (gatorName == null) {
                gatorName = Utilities.RandomGatorName();
            }

            measurements.girth = Random.Range(1, 15);
            measurements.weight = Random.Range(1, 15);
            measurements.length = Random.Range(1, 15);
        }
    }
}
