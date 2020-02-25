using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screen_change_button_script : MonoBehaviour
{
    public GameObject target;
    public GameObject icon;
    public GameObject selected;

    public void SetEnabled()
    {
        if(icon!=null){icon.GetComponent<SpriteRenderer>().enabled=true;}
        gameObject.GetComponent<BoxCollider2D>().enabled=true;
    }

    public void SetDisabled()
    {
        if(icon!=null){icon.GetComponent<SpriteRenderer>().enabled=false;}
        gameObject.GetComponent<BoxCollider2D>().enabled=false;
    }

    private void Update() {
        if (selected!=null && GameObject.Find("Game manager").GetComponent<Game_manager>().current_screen==target)
        {
            selected.GetComponent<SpriteRenderer>().enabled=true;
        }
        else if (selected!=null)
        {
            selected.GetComponent<SpriteRenderer>().enabled=false;
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0) && GameObject.Find("Game manager").GetComponent<Game_manager>().current_screen!=target)
        {
            GameObject.Find("Game manager").GetComponent<Game_manager>().Change_screen(target);
        }
    }
}
