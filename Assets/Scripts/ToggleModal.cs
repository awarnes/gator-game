﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleModal : MonoBehaviour
{    
    public void Toggle() {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
