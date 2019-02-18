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

            age = 0;

            gender = Random.Range(0f, 1f) > 0.5f ? Gender.Male : Gender.Female;

            stress.crowding = 0;
            stress.environment = 0;
            stress.interaction = 0;

            health.healthPoints = 100;
            health.hunger = 100;
            health.thirst = 100;
            health.temperature = 95;
        }
    }
}
