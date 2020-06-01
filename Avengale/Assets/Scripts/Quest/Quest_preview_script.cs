using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_preview_script : MonoBehaviour
{

    public bool isOpened;
    public GameObject quest_name, quest_description, quest_long_description, quest_icon, complete_button, abandon_button, quest_rewards;
    private Item_script _itemScript;
    private Character_stats _characterStats;

    public void Start()
    {
        _itemScript = GameObject.Find("Game manager").GetComponent<Item_script>();
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
    }
    public void showQuest(int slot_id)
    {

        var quests = GameObject.Find("Game manager").GetComponent<Quest_manager_script>().quests;
        var accepted = GameObject.Find("Game manager").GetComponent<Character_stats>().accepted_quests;
        if (accepted[slot_id] != 0)
        {
            gameObject.GetComponent<Animator>().Play("Quest_preview_slide_in_anim");
            GameObject.Find("Quest_preview").GetComponent<Open_button_script>().Open();
            isOpened = true;

            quest_name.GetComponent<Text_animation>().startAnim(quests[accepted[slot_id]].name, 0.01f);
            quest_description.GetComponent<Text_animation>().startAnim(quests[accepted[slot_id]].description, 0.01f);
            quest_long_description.GetComponent<Text_animation>().startAnim(quests[accepted[slot_id]].long_description, 0.01f);
            complete_button.GetComponent<Complete_quest_script>().slot_id = slot_id;
            abandon_button.GetComponent<Quest_abandon_button_script>().slot_id = slot_id;


            string _Rewards = "Rewards:\n";
            var quest = quests[accepted[slot_id]];


            if (quest.item != 0)
            {
                _Rewards += "[" + _itemScript.items[quest.item].name + "]\n";
            }
            if (quest.money != 0)
            {
                _Rewards += "+"+Convert.ToInt32(Math.Round(((double)quest.money * ((_characterStats.Player_plus_money_rate/100)+1)))) + " credit\n";
            }
             if (quest.xp != 0)
            {
                _Rewards += "+"+quest.xp + " XP\n";
            }
            quest_rewards.GetComponent<Text_animation>().startAnim(_Rewards, 0.01f);
        }
    }

    public void closeQuestPreview()
    {
        gameObject.GetComponent<Animator>().Play("Quest_preview_slide_out_anim");
        isOpened = false;
    }

}
