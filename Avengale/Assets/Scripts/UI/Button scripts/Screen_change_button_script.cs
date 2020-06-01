using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screen_change_button_script : MonoBehaviour
{
    public bool isMenu;
    public GameObject target, icon, notification;
    public Sprite normal_icon, selected_icon;
    public void SetEnabled()
    {
        if (icon != null) { icon.GetComponent<SpriteRenderer>().enabled = true; }


        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void SetDisabled()
    {
        if (icon != null) { icon.GetComponent<SpriteRenderer>().enabled = false; }
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }


    private void Update()
    {

        if (icon != null && GameObject.Find("Game manager").GetComponent<Game_manager>().current_screen == target)
        {
            icon.GetComponent<SpriteRenderer>().sprite = selected_icon;
        }
        else if (icon != null)
        {
            icon.GetComponent<SpriteRenderer>().sprite = normal_icon;
        }
    }

    public void setNotification()
    {
        notification.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void clearNotification()
    {
        notification.GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0) && GameObject.Find("Game manager").GetComponent<Game_manager>().current_screen != target)
        {
            GameObject.Find("Game manager").GetComponent<Game_manager>().Change_screen(target, isMenu);
        }
    }
}
