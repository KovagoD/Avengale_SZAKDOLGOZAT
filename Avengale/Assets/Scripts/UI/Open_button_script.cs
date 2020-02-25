using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_button_script : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite sprite_normal;
    public Sprite sprite_activated;
    public GameObject[] targets;

    public string mode;

    public bool isClickable = true;

    void OnMouseOver()
    {
        if (sprite_normal != null && isClickable)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite_activated;
        }
        if(Input.GetMouseButtonUp(0) && isClickable)
        {
            if (sprite_normal != null)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite_normal;
            }

            if (mode=="Inventory")
            {
                Open();
                GameObject.Find("Inventory slots").GetComponent<Animator>().Play("Inventory_slide_in_anim", -1, 0f);
                GameObject.Find("Inventory slots").GetComponent<Animator>().Play("Inventory_slide_in_anim");

                return;
            }
            else if (mode=="Inventory" && GameObject.Find("Inventory slots").GetComponent<Visibility_script>().isOpened)
            {
                GameObject.Find("Inventory_exit_button").GetComponent<Close_button_script>().Close();
                return;
            }
            Open();
        }
    }
    void OnMouseExit() {
        if (sprite_normal != null && isClickable)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite_normal;
        }
    }


    public void Open()
    {
         foreach (var item in targets)
        {
            item.GetComponent<Visibility_script>().setVisible();
        }
    }

}
