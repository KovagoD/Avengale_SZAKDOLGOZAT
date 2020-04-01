using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum npc_modes { conversation, quest }

public class NPC_script : MonoBehaviour
{
    // Start is called before the first frame update

    public npc_modes mode;
    public int conversation_id;
    public int quest_id;


    private Character_stats _characterStats;
    private Conversation_script _conversationScript;
    private Quest_manager_script _questManager;
    void Start()
    {
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        _questManager = GameObject.Find("Game manager").GetComponent<Quest_manager_script>();
        _conversationScript = GameObject.Find("Game manager").GetComponent<Conversation_script>();
    }
    void OnMouseDown()
    {
        //_questManager.acceptQuest(4);

        if (mode == npc_modes.conversation )
        {
            GameObject.Find("Conversation").GetComponent<Conversation_script>().showConversation(conversation_id);
        }

        if (mode == npc_modes.quest && _characterStats.isOnQuest(quest_id))
        {
            GameObject.Find("Conversation").GetComponent<Conversation_script>().showConversation(conversation_id);
        }

    }
}
