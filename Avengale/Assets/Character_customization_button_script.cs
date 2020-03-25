using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_customization_button_script : MonoBehaviour
{
    public bool isNewCharacter;
    public GameObject target;
    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            GameObject.Find("Game manager").GetComponent<Game_manager>().Change_screen(target, true);
            GameObject.Find("Customization_controller").GetComponent<Character_customization_script>().initializeCustomization(isNewCharacter);

        }
    }
}
