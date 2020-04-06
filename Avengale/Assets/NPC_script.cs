using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum npc_modes { conversation, quest_giver, quest }

public class NPC_script : MonoBehaviour
{
    // Start is called before the first frame update

    public int npc_id;
    public string character_name;
    public npc_modes mode;
    public int conversation_id;
    public int[] idle_conversations;
    public int quest_id;
    public GameObject quest_mark;


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

        nameText.GetComponent<Text_animation>().startAnim(character_name, 0.01f);
    }

    public void setQuestIcon()
    {
        quest_mark.GetComponent<SpriteRenderer>().enabled = true;
    }
    public void clearQuestIcon()
    {
        quest_mark.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();

        if (npc_id == 2 && _characterStats.isInCompletedQuests(1))
        {
            mode = npc_modes.quest_giver;
            quest_id = 2;
            conversation_id = 8;
        }


        if (mode == npc_modes.quest_giver && quest_id != 0 && _questManager.available_quests.Contains(_questManager.quests[quest_id]) && !_characterStats.isInCompletedQuests(quest_id)
         && !_characterStats.isOnQuest(quest_id))
        {
            setQuestIcon();
        }
        else { clearQuestIcon(); }
    }
    void OnMouseDown()
    {
        //_questManager.acceptQuest(4);
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        var conversation = GameObject.Find("Conversation").GetComponent<Conversation_script>();

        if (mode == npc_modes.conversation)
        {
            conversation.showConversation(idle_conversations[UnityEngine.Random.Range(0, idle_conversations.Length)]);
        }
        else if (mode == npc_modes.quest_giver)
        {
            if (!_characterStats.isOnQuest(quest_id) && _questManager.quests[quest_id].level_requirement <= _characterStats.Local_level && !_characterStats.isInCompletedQuests(quest_id))
            {
                conversation.showConversation(conversation_id);
            }
            else { conversation.showConversation(idle_conversations[UnityEngine.Random.Range(0, idle_conversations.Length)]); }
        }
        else if (mode == npc_modes.quest)
        {
            if (_characterStats.isOnQuest(quest_id))
            {
                conversation.showConversation(conversation_id);
            }
            else { conversation.showConversation(idle_conversations[UnityEngine.Random.Range(0, idle_conversations.Length)]); }
        }



    }
}
