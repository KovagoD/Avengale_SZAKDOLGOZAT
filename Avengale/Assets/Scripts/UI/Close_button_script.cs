using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close_button_script : MonoBehaviour
{
    public Sprite sprite_normal, sprite_activated;
    public GameObject[] targets;

    void OnMouseOver()
    {
        if (sprite_normal != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite_activated;
        }

        if (Input.GetMouseButtonUp(0) && gameObject.GetComponent<Visibility_script>().isOpened == true)
        {
            if (sprite_normal != null)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite_normal;
            }
            otherFunctions();
        }
    }
    void OnMouseExit()
    {
        if (sprite_normal != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite_normal;
        }
    }

    private void Update()
    {

        /*
        if (Input.GetKey(KeyCode.Escape))
        {
            Close();
        }
        */
    }

    public void otherFunctions()
    {
        if (gameObject.name == "Battle_continue")
        {
            GameObject.Find("Game manager").GetComponent<Combat_manager_script>().getBattleReward();
            Close();
        }
        else if (gameObject.name == "Inventory_exit_button")
        {
            GameObject.Find("Item_preview").GetComponent<Animator>().Play("Item_preview_slide_out_anim");
            GameObject.Find("Inventory slots").GetComponent<Animator>().Play("Inventory_slide_out_anim");

            if (GameObject.Find("Inventory slots").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Inventory_slide_out_anim")
            && GameObject.Find("Inventory slots").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                Close();
            }
            /*
            StopCoroutine("Wait");
            StartCoroutine("Wait", 1);
            */
        }
        else if (gameObject.name == "Authorization_no")
        {
            GameObject.Find("Authorization").GetComponent<Animator>().Play("Authorization_slide_out_anim");

            if (GameObject.Find("Authorization").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Authorization_slide_out_anim")
            && GameObject.Find("Authorization").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                Close();
            }

        }
        else if (gameObject.name == "Exit button item_preview")
        {
            GameObject.Find("Item_preview").GetComponent<Animator>().Play("Item_preview_slide_out_anim");

            if (GameObject.Find("Item_preview").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Item_preview_slide_out_anim")
            && GameObject.Find("Item_preview").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                Close();
            }

        }
        else if (gameObject.name == "Conversation_exit_button")
        {
            GameObject.Find("Conversation").GetComponent<Conversation_script>().closeConversation();
        }
        else if (gameObject.name == "Exit button quest preview")
        {
            GameObject.Find("Quest_preview").GetComponent<Quest_preview_script>().closeQuestPreview();
        }
        else
        {
            Close();
        }

    }

    IEnumerator Wait(int duration)
    {
        yield return new WaitForSeconds(duration);
        Close();
    }



    public void Close()
    {
        foreach (var item in targets)
        {
            item.GetComponent<Visibility_script>().setInvisible();
        }
    }
}
