using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_menu_button_script : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().showPauseMenu();
        }
    }
}
