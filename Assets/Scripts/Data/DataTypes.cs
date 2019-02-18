using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GatorGame {
    public enum Gender {
        Female, Male
    }

    public struct Measurements {
        public float length;
        public float weight;
        public float girth;
    }

    public struct Health {
        public int healthPoints;
        public int hunger;
        public int thirst;
        public int temperature;
        public List<Sickness> sicknesses;

        public string CalculateHP() {
            if (healthPoints < 33) return "Very Hurt!";
            if (healthPoints > 33 && healthPoints < 66) return "A little hurt!";
            return "Doing Okay!";
        }

        public string CalculateHunger() {
            if (hunger < 33) return "Very Hungry!";
            if (hunger > 33 && hunger < 66) return "A little Hungry!";
            return "Doing Okay!";
        }

        public string CalculateThirst() {
            if (thirst < 33) return "Very Thirsty!";
            if (thirst > 33 && thirst < 66) return "A little Thirsty!";
            return "Doing Okay!";
        }

        public string CalculateTemp() {
            if (temperature < 90) return "Chilly!";
            if (temperature > 100) return "Hot!";
            return "Just Right!";
        }
    }

    public struct Stress {
        public float crowding;
        public float environment;
        public float interaction;

        public string CalculateStress() {
            float totalStress = crowding + environment + interaction;
            string message = "Not stressed!";
            if (totalStress > 1 && totalStress < 2) {
                message = "A little stressed!";
            } else if (totalStress > 2) {
                message = "Very stressed!";
            }

            return message;
        }
    }

    public struct Hide {
        public float quality;
        public HidePattern pattern;
    }

    public struct Meat {
        public float quality;
        public float amount; //gatorWeight * .4157
    }
}