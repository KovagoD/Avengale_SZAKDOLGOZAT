using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_ui_script : MonoBehaviour
{

    public GameObject[] slots;
    public GameObject text;

    public bool isOpened;
    public void showInventory()
    {
        text.GetComponent<Text_animation>().startAnim("Inventory", 0.01f);
        
        gameObject.GetComponent<Open_button_script>().Open();
        gameObject.GetComponent<Animator>().Play("Inventory_slide_in_anim");
        isOpened = true;


    }

    public void closeInventory()
    {
        //GameObject.Find("Inventory_exit_button").GetComponent<Close_button_script>().Close();
        gameObject.GetComponent<Animator>().Play("Inventory_slide_out_anim");
        isOpened = false;
    }


    void Update()
    {

        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Inventory_slide_out_anim") && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            GameObject.Find("Inventory_exit_button").GetComponent<Close_button_script>().Close();
        }

    }

}
