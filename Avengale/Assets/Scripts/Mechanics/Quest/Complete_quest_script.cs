using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complete_quest_script : MonoBehaviour
{
    public int slot_id;

    void OnMouseOver()
    {
        // && gameObject.GetComponent<Visibility_script>().isOpened
        if (Input.GetMouseButtonUp(0))
        {
            GameObject.Find("Game manager").GetComponent<Quest_manager_script>().isQuestCompleted(slot_id, GameObject.Find("Game manager").GetComponent<Character_stats>().accepted_quests[slot_id]);
        }
    }
}
