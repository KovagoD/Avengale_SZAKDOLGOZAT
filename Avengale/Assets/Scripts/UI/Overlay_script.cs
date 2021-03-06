﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay_script : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isOpen;
    public void showOverlay()
    {
        isOpen = true;
        gameObject.GetComponent<Animator>().Play("Show_overlay");
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void closeOverlay()
    {
        isOpen = false;
        gameObject.GetComponent<Animator>().Play("Close_overlay");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

    }
}
