﻿using UnityEngine;
using System.Collections;

public class DismissPopUp : MonoBehaviour {

    public SceneFadeInOut sceneFader;

    public void Dismiss()
    {
        Destroy(transform.parent.gameObject);
        GameData.activeModals--;
    }
}
