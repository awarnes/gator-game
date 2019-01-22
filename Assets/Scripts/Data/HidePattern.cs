using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GatorGame {

    enum Color {
        red, orange, yellow, green, blue, indigo, violet
    }

    enum Pattern {
        scale, plaid,  
    }
    public class HidePattern
    {
        private Color primaryColor;
        private Color secondaryColor;
        private Color tertiaryColor;

        private Pattern pattern;
    }
}
