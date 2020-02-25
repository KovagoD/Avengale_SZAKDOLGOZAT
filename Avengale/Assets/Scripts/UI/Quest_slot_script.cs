using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_slot_script : MonoBehaviour
{
    public int ID;
    public GameObject Quest_name;
    public GameObject Quest_description;

    void OnMouseDown()
    {
        GameObject.Find("Quest_preview").GetComponent<Quest_preview_script>().showQuest(ID);
        var quest = GameObject.Find("Game manager").GetComponent<Quest_manager_script>().quests[GameObject.Find("Game manager").GetComponent<Character_stats>().accepted_quests[ID]];
        Quest_name.GetComponent<Text_animation>().startAnim(quest.name, 0.01f);
        Quest_description.GetComponent<Text_animation>().startAnim(quest.description, 0.01f);
    }
}
