using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider_script : MonoBehaviour
{

    void Update()
    {
        if (GameObject.Find("Overlay").GetComponent<Overlay_script>().isOpen)
        {
            gameObject.GetComponent<Slider>().interactable = false;
        }
        else
        {
            gameObject.GetComponent<Slider>().interactable = true;
        }
    }
}
