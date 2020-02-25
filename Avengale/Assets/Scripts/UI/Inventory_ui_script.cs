using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_ui_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Inventory_slide_out_anim") && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            GameObject.Find("Inventory_exit_button").GetComponent<Close_button_script>().Close();
        }
        
    }
}
