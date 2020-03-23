using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomize_customization_button_script : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            GameObject.Find("Customization_controller").GetComponent<Character_customization_script>().randomizeLook();
        }
    }
}
