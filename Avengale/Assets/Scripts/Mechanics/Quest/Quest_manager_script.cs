using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_manager_script : MonoBehaviour
{
    [Header("References")]
    public GameObject[] quest_slots = new GameObject[3];
    public List<Quest> quests = new List<Quest>();
    private Ingame_notification_script _notification;
    private Character_stats _characterStats;


    void Awake()
    {

        /*
            ID, NAME, TYPE, DESCRIPTION, LONG DESCRIPTION, OBJECTIVE, XP, ITEM
        */

        quests.Add(new Quest(0, "", "", "", "", 0, 0, 0));
        quests.Add(new Quest(1, "Combat test quest", "combat", "defeat Szisz", "Ayaya? Aya! AYAYA!...AYAYA\n AYAYA AYAYA AYAYA (yamete)", 1, 100, 5));
        quests.Add(new Quest(2, "Conversation test quest", "conversation", "Speak with David", "gfngfner434534", 3, 100, 6));
        quests.Add(new Quest(3, "Item test quest", "item", "Get the quest item", "sadsd21dasdasdasdsad", 11, 100, 6));
        quests.Add(new Quest(4, "Item reeeee", "item", "Get the quest item", "sadsd21dasdasdasdsad", 12, 100, 6));

    }

    private void Start()
    {
        _notification = GameObject.Find("Notification").GetComponent<Ingame_notification_script>();
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();

    }

    public void updateQuestSlot(int id)
    {
        quest_slots[id].GetComponent<Quest_slot_script>().Quest_name.GetComponent<Text_animation>().startAnim(quests[_characterStats.accepted_quests[id]].name, 0.01f);
        quest_slots[id].GetComponent<Quest_slot_script>().Quest_description.GetComponent<Text_animation>().startAnim("-" + quests[_characterStats.accepted_quests[id]].description, 0.01f);

        GameObject.Find("Exit button quest preview").GetComponent<Close_button_script>().Close();
    }

    public void acceptQuest(int id)
    {
        Debug.Log(_characterStats.accepted_quests[0]);
        if (!_characterStats.completed_quests.Contains(quests[id]) && _characterStats.accepted_quests[0] != id && _characterStats.accepted_quests[1] != id && _characterStats.accepted_quests[2] != id)
        {
            if (_characterStats.accepted_quests[0] != 0 &&
            _characterStats.accepted_quests[1] != 0 &&
            _characterStats.accepted_quests[2] != 0)
            {
                _notification.message("You cannot pick up another quest!", 3, "red");
            }
            for (int i = 0; i < _characterStats.accepted_quests.Length; i++)
            {
                if (_characterStats.accepted_quests[i] == 0)
                {
                    _characterStats.accepted_quests[i] = quests[id].id;
                    updateQuestSlot(i);
                    _notification.message(quests[id].name + " is accepted!", 3);
                    break;
                }
            }
        }
    }

    public void abandonQuest(int slot_id)
    {
        _characterStats.accepted_quests[slot_id] = 0;
        //updateQuestSlot(slot_id);
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
        updateQuestSlot(0);
        updateQuestSlot(1);
        updateQuestSlot(2);


    }

    public void isQuestCompleted(int slot_id, int quest_id)
    {

        if (quests[quest_id].type == "combat")
        {
            if (_characterStats.defeated_enemies.Contains(GameObject.Find("Game manager").GetComponent<Enemy_manager_script>().enemies[quests[quest_id].objective]))
            {
                quests[quest_id].Complete();
                _characterStats.completed_quests.Add(quests[quest_id]);
                _characterStats.accepted_quests[slot_id] = 0;
                _notification.message(quests[quest_id].name + " is completed!", 3);
            }
            else { _notification.message(quests[quest_id].name + " is <b>not</b> completed!", 3, "red"); }
        }
        else if (quests[quest_id].type == "conversation")
        {
            if (_characterStats.completed_conversations.Contains(GameObject.Find("Conversation").GetComponent<Conversation_script>().conversations[quests[quest_id].objective]))
            {
                quests[quest_id].Complete();
                _characterStats.completed_quests.Add(quests[quest_id]);
                _characterStats.accepted_quests[slot_id] = 0;
                _notification.message(quests[quest_id].name + " is completed!", 3);
            }
            else { _notification.message(quests[quest_id].name + " is <b>not</b> completed!", 3, "red"); }
        }
        else if (quests[quest_id].type == "item")
        {
            bool haveItem = false;
            var inventory = _characterStats.Inventory;
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == quests[quest_id].objective)
                {
                    haveItem = true;
                    inventory[i] = 0;
                    break;
                }
            }

            if (haveItem)
            {
                quests[quest_id].Complete();
                _characterStats.completed_quests.Add(quests[quest_id]);
                _characterStats.accepted_quests[slot_id] = 0;
                _notification.message(quests[quest_id].name + " is completed!", 3);
            }
            else { _notification.message(quests[quest_id].name + " is <b>not</b> completed!", 3, "red"); }
        }

        updateQuestSlot(slot_id);

    }


}
[System.Serializable]
public class Quest
{
    public int id;
    public string name;
    public string type;
    public string description;
    public string long_description;
    public int objective;

    public int xp;
    public int item;
    public int money;

    public Quest(int id, string name, string type, string description, string long_description, int objective, int xp, int item)
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
