using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body_part_selector_button_script : MonoBehaviour
{
    public bool isNext;
    public body_parts part;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (isNext)
            {
                GameObject.Find("Customization_controller").GetComponent<Character_customization_script>().next(part);
            }
            else
            {
                GameObject.Find("Customization_controller").GetComponent<Character_customization_script>().previous(part);
            }
        }
    }
}
