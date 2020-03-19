using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_preview_script : MonoBehaviour
{
    [Header("Quest properties")]
    public GameObject quest_name, quest_description, quest_long_description, quest_icon, complete_button, abandon_button;
    public void showQuest(int slot_id)
    {
        var quests = GameObject.Find("Game manager").GetComponent<Quest_manager_script>().quests;
        var accepted = GameObject.Find("Game manager").GetComponent<Character_stats>().accepted_quests;
        if (accepted[slot_id] != 0)
        {
            GameObject.Find("Quest_preview").GetComponent<Open_button_script>().Open();
            quest_name.GetComponent<Text_animation>().startAnim(quests[accepted[slot_id]].name, 0.01f);
            quest_description.GetComponent<Text_animation>().startAnim(quests[accepted[slot_id]].description, 0.01f);
            quest_long_description.GetComponent<Text_animation>().startAnim(quests[accepted[slot_id]].long_description, 0.01f);
            complete_button.GetComponent<Complete_quest_script>().slot_id = slot_id;
            abandon_button.GetComponent<Quest_abandon_button_script>().slot_id = slot_id;
        }
    }
}
