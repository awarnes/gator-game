using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GatorGame {
    public enum Gender {
        female, male
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
    }

    public struct Stress {
        public float crowding;
        public float environment;
        public float interaction; 
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