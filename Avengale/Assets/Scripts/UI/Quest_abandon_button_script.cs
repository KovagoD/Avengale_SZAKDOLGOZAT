using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_abandon_button_script : MonoBehaviour
{
    public int slot_id;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            GameObject.Find("Game manager").GetComponent<Quest_manager_script>().abandonQuest(slot_id);
        }
    }
}
