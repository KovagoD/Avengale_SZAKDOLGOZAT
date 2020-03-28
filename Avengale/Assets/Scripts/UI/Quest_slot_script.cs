using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_slot_script : MonoBehaviour
{
    public int ID;
    public int quest_id;
    public GameObject Quest_name, Quest_description, Quest_icon, Quest_complete_sign;

    public Sprite full, empty;

    private Quest quest;

    private void Start()
    {
        quest = GameObject.Find("Game manager").GetComponent<Quest_manager_script>().quests[GameObject.Find("Game manager").GetComponent<Character_stats>().accepted_quests[ID]];
    }

    private void Update() {
        quest_id = GameObject.Find("Game manager").GetComponent<Character_stats>().accepted_quests[ID];
    }

    void OnMouseDown()
    {
        GameObject.Find("Quest_preview").GetComponent<Quest_preview_script>().showQuest(ID);
        if (quest.id != 0)
        {
            Quest_description.GetComponent<Text_animation>().startAnim(quest.description, 0.01f);
            Quest_name.GetComponent<Text_animation>().startAnim(quest.name, 0.01f);
        }
    }
}
