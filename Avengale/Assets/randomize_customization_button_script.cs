using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomize_customization_button_script : MonoBehaviour
{
    public bool isHair;
    public bool isAll;
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isAll)
            {
                GameObject.Find("Customization_controller").GetComponent<Character_customization_script>().randomizeAll();
            }
            else if (!isHair)
            {
                GameObject.Find("Customization_controller").GetComponent<Character_customization_script>().randomizeLook();
            }
            else if (isHair)
            {
                GameObject.Find("Customization_controller").GetComponent<Character_customization_script>().randomizeHairColor();
            }


        }
    }
}
