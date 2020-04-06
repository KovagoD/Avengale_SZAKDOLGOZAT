using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accept_quest_button : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            GameObject.Find("Game manager").GetComponent<Quest_manager_script>().acceptRandomQuest();
        }
    }
}
