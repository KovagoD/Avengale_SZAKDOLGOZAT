using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_test_button : MonoBehaviour
{
public GameObject target;
    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0) && !GameObject.Find("Item_preview").GetComponent<Visibility_script>().isOpened)
        {
            GameObject.Find("Game manager").GetComponent<Game_manager>().Change_screen(target);
        }

    }
}
