using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum quest_types { combat, conversation, item }
public class Quest_manager_script : MonoBehaviour
{
    [Header("References")]

    public List<Quest> available_quests = new List<Quest>();
    public GameObject[] quest_slots = new GameObject[3];
    public List<Quest> quests = new List<Quest>();
    private Ingame_notification_script _notification;
    private Character_stats _characterStats;
    private Game_manager _gameManager;

    public Sprite ConversationIcon, CombatIcon, ItemIcon;

    void Awake()
    {

        /*
            ID, NAME, TYPE, DESCRIPTION, LONG DESCRIPTION, OBJECTIVE, XP, ITEM
        */

        quests.Add(new Quest(0,0, "", quest_types.combat, "", "", 0, 0, 0));
        quests.Add(new Quest(1,2, "Welcome to the HUB", quest_types.conversation, "Speak with NPC#2 at the HUB", "NPC asked you to speak with NPC#2 about your recruitment.", 2, 100, 8));

    }

    void Update() {
        if (available_quests.Count > 1)
        {
            _gameManager.hub_button.GetComponent<Screen_change_button_script>().setNotification();
        }
        else
        {
            _gameManager.hub_button.GetComponent<Screen_change_button_script>().clearNotification();
        }
    }

    public void checkAvailableQuests()
    {
        available_quests.Clear();
        foreach (var quest in quests)
        {
            if (quest.level_requirement <= _characterStats.Local_level)
            {
                available_quests.Add(quest);
            }
        }
    }

    private void Start()
    {
        _notification = GameObject.Find("Notification").GetComponent<Ingame_notification_script>();
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        _gameManager = GameObject.Find("Game manager").GetComponent<Game_manager>();

    }

    public void updateQuestSlots()
    {
        for (int i = 0; i < _characterStats.accepted_quests.Length; i++)
        {
            quest_slots[i].GetComponent<Quest_slot_script>().quest_id = _characterStats.accepted_quests[i];
        }

        for (int i = 0; i < quest_slots.Length; i++)
        {
            var _slot = quest_slots[i].GetComponent<Quest_slot_script>();
            var _quest = quests[_characterStats.accepted_quests[_slot.ID]];

            if (_characterStats.accepted_quests[_slot.ID] != 0)
            {
                _slot.Quest_name.GetComponent<Text_animation>().startAnim(_quest.name, 0.01f);
                _slot.Quest_description.GetComponent<Text_animation>().startAnim("-" + _quest.description, 0.01f);
                _slot.Quest_icon.GetComponent<SpriteRenderer>().enabled = true;
                _slot.GetComponent<SpriteRenderer>().sprite = _slot.full;
                _slot.GetComponent<Button_script>().sprite_normal = _slot.full;

                switch (_quest.type)
                {
                    case quest_types.conversation:
                        _slot.Quest_icon.GetComponent<SpriteRenderer>().sprite = ConversationIcon;
                        break;
                    case quest_types.combat:
                        _slot.Quest_icon.GetComponent<SpriteRenderer>().sprite = CombatIcon;
                        break;
                    case quest_types.item:
                        _slot.Quest_icon.GetComponent<SpriteRenderer>().sprite = ItemIcon;
                        break;
                }

                if (isQuestCompleted(i))
                {
                    _slot.Quest_complete_sign.GetComponent<SpriteRenderer>().enabled = true;
                }
                else { _slot.Quest_complete_sign.GetComponent<SpriteRenderer>().enabled = false; }
            }
            else
            {
                _slot.Quest_name.GetComponent<Text_animation>().startAnim("", 0.01f);
                _slot.Quest_description.GetComponent<Text_animation>().startAnim("", 0.01f);
                _slot.Quest_complete_sign.GetComponent<SpriteRenderer>().enabled = false;
                _slot.Quest_icon.GetComponent<SpriteRenderer>().enabled = false;
                _slot.GetComponent<SpriteRenderer>().sprite = _slot.empty;
                _slot.GetComponent<Button_script>().sprite_normal = _slot.empty;
            }
        }
        if (GameObject.Find("Quest_preview").GetComponent<Quest_preview_script>().isOpened)
        {
            GameObject.Find("Quest_preview").GetComponent<Quest_preview_script>().closeQuestPreview();
        }
    }

    public bool isQuestSlotsFull()
    {
        for (int i = 0; i < _characterStats.accepted_quests.Length; i++)
        {
            if (_characterStats.accepted_quests[i] == 0)
            {
                return false;
            }
        }
        _notification.message("You cannot pick up another quest!", 3, "red");
        return true;
    }

    public bool isQuestAlreadyAccepted(int id)
    {
        for (int i = 0; i < _characterStats.accepted_quests.Length; i++)
        {
            if (_characterStats.accepted_quests[i] == id)
            {
                _notification.message("You are already on this quest!", 3, "red");
                return true;
            }
        }

        return false;
    }


    public void acceptQuest(int id)
    {

        if (!isQuestSlotsFull() && !isQuestAlreadyAccepted(id))
        {
            for (int i = 0; i < _characterStats.accepted_quests.Length; i++)
            {
                if (_characterStats.accepted_quests[i] == 0)
                {
                    _characterStats.accepted_quests[i] = id;
                    _notification.message(quests[id].name + " is accepted!", 3);
                    updateQuestSlots();

                    for (int y = 0; y < available_quests.Count; y++)
                    {
                        if (available_quests[y].id == id)
                        {
                            available_quests.Remove(available_quests[y]);
                        }
                    }

                    break;
                }
            }
        }
    }

    public void abandonQuest(int slot_id)
    {
        _characterStats.accepted_quests[slot_id] = 0;
        sortQuests();
    }

    public void sortQuests()
    {
        for (int i = 0; i < _characterStats.accepted_quests.Length - 1; i++)
        {
            for (int j = i + 1; j < _characterStats.accepted_quests.Length; j++)
            {
                int temp;
                if (_characterStats.accepted_quests[i] < _characterStats.accepted_quests[j])
                {
                    temp = _characterStats.accepted_quests[i];
                    _characterStats.accepted_quests[i] = _characterStats.accepted_quests[j];
                    _characterStats.accepted_quests[j] = temp;
                }
            }
        }
        updateQuestSlots();
    }

    public bool isQuestCompleted(int slot_id)
    {
        var _quest = quests[_characterStats.accepted_quests[slot_id]];

        if (_quest.type == quest_types.combat)
        {
            if (_characterStats.defeated_enemies.Contains(GameObject.Find("Game manager").GetComponent<Enemy_manager_script>().enemies[_quest.objective]))
            {
                return true;
            }

        }
        else if (_quest.type == quest_types.conversation)
        {
            if (_characterStats.completed_conversations.Contains(GameObject.Find("Conversation").GetComponent<Conversation_script>().conversations[_quest.objective]))
            {
                return true;
            }

        }
        else if (_quest.type == quest_types.item)
        {
            if (_characterStats.Inventory.Any(a => a == _quest.objective))
            {

                return true;
            }
        }
        return false;
    }

    public bool haveCompletedQuest()
    {
        for (int i = 0; i < _characterStats.accepted_quests.Length; i++)
        {
            var _quest = quests[_characterStats.accepted_quests[i]];

            if (_quest.type == quest_types.combat)
            {
                if (_characterStats.defeated_enemies.Contains(GameObject.Find("Game manager").GetComponent<Enemy_manager_script>().enemies[_quest.objective]))
                {
                    return true;
                }
            }
            else if (_quest.type == quest_types.conversation)
            {
                if (_characterStats.completed_conversations.Contains(GameObject.Find("Conversation").GetComponent<Conversation_script>().conversations[_quest.objective]))
                {
                    return true;
                }
            }
            else if (_quest.type == quest_types.item)
            {
                if (_characterStats.Inventory.Any(a => a == _quest.objective))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void questComplete(int slot_id)
    {
        var _quest = quests[_characterStats.accepted_quests[slot_id]];

        if (_quest.type == quest_types.combat)
        {
            if (isQuestCompleted(slot_id))
            {
                _quest.Complete();
                _characterStats.completed_quests.Add(_quest);
                _notification.message(_quest.name + " is completed!", 3);

                _characterStats.accepted_quests[slot_id] = 0;
            }
            else
            {
                _notification.message(_quest.name + " is <b>not</b> completed!", 3);
            }
        }
        else if (_quest.type == quest_types.conversation)
        {
            if (isQuestCompleted(slot_id))
            {
                _quest.Complete();
                _characterStats.completed_quests.Add(_quest);
                _notification.message(_quest.name + " is completed!", 3);

                _characterStats.accepted_quests[slot_id] = 0;
            }
            else
            {
                _notification.message(_quest.name + " is <b>not</b> completed!", 3);
            }
        }
        else if (_quest.type == quest_types.item)
        {
            if (isQuestCompleted(slot_id))
            {

                _quest.Complete();
                _characterStats.completed_quests.Add(_quest);
                _notification.message(_quest.name + " is completed!", 3);
                _characterStats.accepted_quests[slot_id] = 0;

                for (int i = 0; i < _characterStats.Inventory.Length; i++)
                {
                    if (_characterStats.Inventory[i] == _quest.objective)
                    {
                        _characterStats.Inventory[i] = 0;
                        break;
                    }
                }
            }
            else
            {
                _notification.message(_quest.name + " is <b>not</b> completed!", 3);
            }
        }

        GameObject.Find("Quest_preview").GetComponent<Quest_preview_script>().closeQuestPreview();
        sortQuests();
    }


}
[System.Serializable]
public class Quest
{
    public int id;
    public int level_requirement;
    public string name;
    public quest_types type;
    public string description;
    public string long_description;
    public int objective;

    public int xp;
    public int item;
    public int money;

    public Quest(int id, int giver_id, string name, quest_types type, string description, string long_description, int objective, int xp, int item)
    {
        this.id = id;
        this.name = name;
        this.type = type;
        this.description = description;
        this.long_description = long_description;
        this.objective = objective;
        this.xp = xp;
        this.item = item;
    }

    public void Complete()
    {
        Character_stats _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();

        if (xp != 0) { _characterStats.getXP(xp); }
        if (item != 0) { _characterStats.itemPickup(item, true); }
        if (money != 0) { _characterStats.getMoney(money); }
    }

}
