using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Authorization_script : MonoBehaviour
{
    public string input_mode;
    public int input_id_num;
    private void Update() {
         if(gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Authorization_slide_out_anim")
         && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            GameObject.Find("Authorization_no").GetComponent<Close_button_script>().Close();
        }
    }
    public void ShowAuthorization(string input, int input_id)
    {
        gameObject.GetComponent<Open_button_script>().Open();
        gameObject.GetComponent<Animator>().Play("Authorization_slide_in_anim", -1, 0f);
        gameObject.GetComponent<Animator>().Play("Authorization_slide_in_anim");
        input_mode=input;
        input_id_num=input_id;
    }
    public void AuthorizationYes()
    {
        
        if(input_mode=="deleteItem")
        {
            var item_preview=GameObject.Find("Delete_button").GetComponent<Item_delete_script>();
            GameObject.Find("Game manager").GetComponent<Character_stats>().deleteItem(item_preview.mode, item_preview.slot_id);
            gameObject.GetComponent<Animator>().Play("Authorization_slide_out_anim");
        }
    }
    public void AuthorizationNo()
    {
        gameObject.GetComponent<Animator>().Play("Authorization_slide_out_anim");
    }
}
