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

    public GameObject[] npcs = new GameObject[3];
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

        quests.Add(new Quest(0, 0, 0, "", quest_types.combat, "", "", 0, 0, 0, 0));
        quests.Add(new Quest(1, 1, 1, "Welcome to the HUB", quest_types.combat, "Speak with <b>David</b> in the HUB and complete his task", "<b>Zachary</b> asked you to speak with <b>David</b> about your recruitment.", 4, 100, 100, 8));
        quests.Add(new Quest(2, 2, 2, "Repel the cultists", quest_types.combat, "Defeat a cultist", "Cultists ambushed the station. Defeat them and defend the station!", 1, 100, 200, 0));
        quests.Add(new Quest(3, 0, 2, "Supply problem", quest_types.combat, "Defeat a <b>Supply looter<B>", "After <b>Zachary</b> investigated about the missing supplies, he found two looters.", 6, 100, 50, 0));
        quests.Add(new Quest(4, 0, 2, "The essential gear", quest_types.item, "Get the '<b>Essential gear</b>' from the store", "An important part is missing! <b>Zachary</b> asked you to buy it from the store.", 16, 100, 200, 0));
        quests.Add(new Quest(5, 0, 2, "Interrogation", quest_types.conversation, "Interrogate the <b>Thief</b> in the HUB", "After the guards arrested one of the thieves they took him immediately to the HUB for interrogation. ", 9, 100, 150, 0));

    }

    void Update()
    {
        /*
        if (available_quests.Count > 0)
        {
            _gameManager.hub_button.GetComponent<Screen_change_button_script>().setNotification();
        }
        else
        {
            _gameManager.hub_button.GetComponent<Screen_change_button_script>().clearNotification();
        }
        */
    }

    public void checkAvailableQuests()
    {
        available_quests.Clear();
        foreach (var quest in quests)
        {
            if (quest.id != 0 && quest.level_requirement <= _characterStats.Player_level && !_characterStats.isInCompletedQuests(quest.id) && !_characterStats.isOnQuest(quest.id))
            {
                available_quests.Add(quest);
            }
        }
    }

    private void Start()
    {
        _notification = GameObject.Find("Notification").GetComponent<Ingame_notification_script>();
        _gameManager = GameObject.Find("Game manager").GetComponent<Game_manager>();
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();

        checkAvailableQuests();

    }

    public void updateQuestSlots()
    {
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();

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
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();

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
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
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
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();

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

    public void acceptRandomQuest()
    {
        int quest_id = UnityEngine.Random.Range(3, 6);
        if (!isQuestSlotsFull())
        {
            if (!_characterStats.isOnQuest(quest_id) && _characterStats.Player_level >= quests[quest_id].level_requirement)
            {
                acceptQuest(quest_id);
            }
            else
            {
                acceptRandomQuest();
            }
        }
        else
        {
            _notification.message("You cannot pick up another quest!", 3, "red");
        }

    }

    public void abandonQuest(int slot_id)
    {
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        _characterStats.accepted_quests[slot_id] = 0;
        sortQuests();
        checkAvailableQuests();
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
            if (_characterStats.isInDefeatedEnemies(_quest.objective))
            {
                return true;
            }
        }
        else if (_quest.type == quest_types.conversation)
        {
            if (_characterStats.isInCompletedConversations(_quest.objective))
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
                if (_characterStats.isInDefeatedEnemies(_quest.objective))
                {
                    return true;
                }
            }
            else if (_quest.type == quest_types.conversation)
            {
                if (_characterStats.isInCompletedConversations(_quest.objective))
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
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();

        if (isQuestCompleted(slot_id))
        {
            _quest.Complete();

            _characterStats.completed_quests.Add(_quest.id);
            _notification.message(_quest.name + " is completed!", 3);

            _characterStats.accepted_quests[slot_id] = 0;
            if (_quest.type == quest_types.combat)
            {
                for (int i = 0; i < _characterStats.defeated_enemies.Count; i++)
                {
                    if (_characterStats.defeated_enemies[i] == _quest.objective)
                    {
                        _characterStats.defeated_enemies.RemoveAt(i);
                    }
                }
            }
            else if (_quest.type == quest_types.conversation)
            {
                for (int i = 0; i < _characterStats.completed_conversations.Count; i++)
                {
                    Debug.Log(_characterStats.completed_conversations[i] + " " + _quest.objective);

                    if (_characterStats.completed_conversations[i] == _quest.objective)
                    {
                        _characterStats.completed_conversations.RemoveAt(i);
                    }
                }
            }
            else if (_quest.type == quest_types.item)
            {
                for (int i = 0; i < _characterStats.Inventory.Length; i++)
                {
                    if (_characterStats.Inventory[i] == _quest.objective)
                    {
                        _characterStats.Inventory[i] = 0;
                        break;
                    }
                }
            }
        }
        else
        {
            _notification.message(_quest.name + " is <b>not</b> completed!", 3, "red");
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

    public int quest_giver;
    public string name;
    public quest_types type;
    public string description;
    public string long_description;
    public int objective;

    public int xp;
    public int item;
    public int money;

    public Quest(int id, int level_requirement, int quest_giver, string name, quest_types type, string description, string long_description, int objective, int xp, int money, int item)
    {
        this.id = id;
        this.level_requirement = level_requirement;
        this.quest_giver = quest_giver;
        this.name = name;
        this.type = type;
        this.description = description;
        this.long_description = long_description;
        this.objective = objective;
        this.xp = xp;
        this.money = money;
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
