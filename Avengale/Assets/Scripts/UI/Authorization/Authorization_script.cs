using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Authorization_script : MonoBehaviour
{
    public GameObject title, description, yes, no;

    public string input_mode;
    public int input_id_num;
    private void Update()
    {
        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Authorization_slide_out_anim")
        && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            GameObject.Find("Authorization_no").GetComponent<Close_button_script>().Close();
        }
    }
    public void ShowAuthorization(string input, int input_id)
    {
        gameObject.GetComponent<Open_button_script>().Open();

        GameObject.Find("Overlay").GetComponent<Overlay_script>().showOverlay();

        if (input == "surrenderBattle")
        {
            title.GetComponent<Text_animation>().startAnim("Are you sure?", 0.05f);
            description.GetComponent<Text_animation>().startAnim("You will face the penalty of loosing <b>20%</b> of your credits!", 0.05f);
            yes.GetComponent<Text_animation>().startAnim("Surrender", 0.05f);
            no.GetComponent<Text_animation>().startAnim("No.", 0.05f);
        }

        if (input == "deleteItem" || input == "abandonQuest" || input == "confirmReCustomization")
        {
            title.GetComponent<Text_animation>().startAnim("Are you sure?", 0.05f);
            description.GetComponent<Text_animation>().startAnim("You can't redo this action!", 0.05f);
            yes.GetComponent<Text_animation>().startAnim("Yes!", 0.05f);
            no.GetComponent<Text_animation>().startAnim("No.", 0.05f);
        }


        if (input == "confirmCustomization")
        {
            title.GetComponent<Text_animation>().startAnim("Are you ready?", 0.05f);
            description.GetComponent<Text_animation>().startAnim("You can change your appearance later!", 0.05f);
            yes.GetComponent<Text_animation>().startAnim("I'm ready!", 0.05f);
            no.GetComponent<Text_animation>().startAnim("No.", 0.05f);
        }


        gameObject.GetComponent<Animator>().Play("Authorization_slide_in_anim", -1, 0f);
        gameObject.GetComponent<Animator>().Play("Authorization_slide_in_anim");
        input_mode = input;
        input_id_num = input_id;
    }
    public void AuthorizationYes()
    {
        if (input_mode == "surrenderBattle")
        {
            
            GameObject.Find("Game manager").GetComponent<Combat_manager_script>().surrenderBattle();
            GameObject.Find("Game manager").GetComponent<Character_stats>().looseMoney(GameObject.Find("Game manager").GetComponent<Character_stats>().getPercentOfMoney(20));
            gameObject.GetComponent<Animator>().Play("Authorization_slide_out_anim");
            GameObject.Find("Overlay").GetComponent<Overlay_script>().closeOverlay();
        }

        if (input_mode == "deleteItem")
        {
            var item_preview = GameObject.Find("Delete_button").GetComponent<Item_delete_script>();
            GameObject.Find("Game manager").GetComponent<Character_stats>().deleteItem(item_preview.mode, item_preview.slot_id);
            gameObject.GetComponent<Animator>().Play("Authorization_slide_out_anim");
            GameObject.Find("Overlay").GetComponent<Overlay_script>().closeOverlay();
        }

        if (input_mode == "abandonQuest")
        {
            GameObject.Find("Game manager").GetComponent<Quest_manager_script>().abandonQuest(input_id_num);
            GameObject.Find("Quest_preview").GetComponent<Quest_preview_script>().closeQuestPreview();
            gameObject.GetComponent<Animator>().Play("Authorization_slide_out_anim");
            GameObject.Find("Overlay").GetComponent<Overlay_script>().closeOverlay();
        }

        if (input_mode == "confirmCustomization" || input_mode == "confirmReCustomization")
        {
            GameObject.Find("Customization_controller").GetComponent<Character_customization_script>().confirmCustomization();
            gameObject.GetComponent<Animator>().Play("Authorization_slide_out_anim");
            GameObject.Find("Overlay").GetComponent<Overlay_script>().closeOverlay();
        }
    }
    public void AuthorizationNo()
    {
        gameObject.GetComponent<Animator>().Play("Authorization_slide_out_anim");
        GameObject.Find("Overlay").GetComponent<Overlay_script>().closeOverlay();
    }
}
