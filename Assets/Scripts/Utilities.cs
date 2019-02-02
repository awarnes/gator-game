using UnityEngine;
using System.Text.RegularExpressions;

namespace GatorGame {
    public static class Utilities {

        private static TextAsset allGatorNames = Resources.Load<TextAsset>("gatorNames");

        public static string RandomGatorName() {
            string[] gatorNames = parseTxtAsset(allGatorNames);

            return gatorNames[Random.Range(0, gatorNames.Length)];
        }

        /**
        * Parses a txt TextAsset into an array of strings.
        */
        public static string[] parseTxtAsset(TextAsset textAsset) {
            string textAssetAsString = textAsset.text;
            return Regex.Split(textAssetAsString, "\n|\r|\r\n");
        }

        public static GameObject GetGameObject(string name) {
            foreach(GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject))) {
                if (go.name == name) {
                    return go;
                }
            }
            return null;
        }
    }
}
