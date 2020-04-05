using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum npc_modes { conversation, quest }

public class NPC_script : MonoBehaviour
{
    // Start is called before the first frame update

    public int npc_id;
    public string character_name;
    public npc_modes mode;
    public int conversation_id;
    public int quest_id;


    public GameObject nameText;
    private Character_stats _characterStats;
    private Conversation_script _conversationScript;
    private Quest_manager_script _questManager;
    private Game_manager _gameManager;
    void Start()
    {
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        _questManager = GameObject.Find("Game manager").GetComponent<Quest_manager_script>();
        _conversationScript = GameObject.Find("Game manager").GetComponent<Conversation_script>();
        _gameManager = GameObject.Find("Game manager").GetComponent<Game_manager>();

        nameText.GetComponent<Text_animation>().startAnim(character_name,0.01f);
    }

    void OnMouseDown()
    {
        //_questManager.acceptQuest(4);

        if (mode == npc_modes.conversation )
        {
            GameObject.Find("Conversation").GetComponent<Conversation_script>().showConversation(conversation_id);
        }

        if (mode == npc_modes.quest && !_characterStats.isOnQuest(quest_id) && _questManager.quests[quest_id].level_requirement <= _characterStats.Local_level)
        {
            GameObject.Find("Conversation").GetComponent<Conversation_script>().showConversation(conversation_id);
        }

    }
}
