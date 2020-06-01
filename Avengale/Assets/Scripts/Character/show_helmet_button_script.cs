using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class show_helmet_button_script : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isHide;
    public Sprite hide, hide_pressed, show, show_pressed;

    void OnMouseDown()
    {
        var _customizationManager = GameObject.Find("Customization_controller").GetComponent<Character_customization_script>();
        if (isHide)
        {
            isHide = false;
            _customizationManager.ShowHelmet();

            gameObject.GetComponent<Button_script>().sprite_normal = hide;
            gameObject.GetComponent<Button_script>().sprite_activated = hide_pressed;

        }
        else
        {
            isHide = true;
            _customizationManager.HideHelmet();

            gameObject.GetComponent<Button_script>().sprite_normal = show;
            gameObject.GetComponent<Button_script>().sprite_activated = show_pressed;
        }
    }


}
