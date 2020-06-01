using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sex_change_button_script : MonoBehaviour
{
    public bool isMale;
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
                GameObject.Find("Customization_controller").GetComponent<Character_customization_script>().changeSex(isMale);
        }
    }
}
