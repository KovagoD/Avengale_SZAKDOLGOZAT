using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;

public class Character_stats : MonoBehaviour
{
    [Header("Combat")]
    public int Local_max_health = 0;
    public int Local_health = 0;
    public int Local_max_resource = 0;
    public int Local_resource = 0;
    public int Local_damage = 0;


    [Header("Completed things")]

    public List<Enemy> defeated_enemies = new List<Enemy>();
    public List<Conversation> completed_conversations = new List<Conversation>();
    public int[] accepted_quests = new int[3];
    public List<Quest> completed_quests = new List<Quest>();




    [Header("Customization")]
    public string Local_name = "Unknown";
    public string Local_title = "the Anone";
    public int Local_class = 1;
    public int Local_talent = 1;

    public int hair_id;
    public int eyes_id;
    public int nose_id;
    public int mouth_id;
    public int body_id;

    [Header("Stats")]
    public int Local_xp = 0;
    public int Local_needed_xp = 150;
    public int Local_level = 1;
    public int Local_money = 0;

    [Header("Inventory")]
    public int[] Inventory = new int[10];
    public int[] Equipments = new int[8];

    [Header("Spells")]
    public int Local_spell_points = 5;
    public int[] Spells = new int[5];
    public int[] Talents = new int[10];

    [Header("References")]
    public TextMeshProUGUI NameAndTitle;
    public TextMeshProUGUI Statistics;
    public GameObject Money;
    public GameObject XP_bar;


    private Item_script _itemScript;
    private Ingame_notification_script _notification;

    public TMP_InputField iField;


    void Start()
    {
        _itemScript = GameObject.Find("Game manager").GetComponent<Item_script>();
        _notification = GameObject.Find("Notification").GetComponent<Ingame_notification_script>();
    }

    public void savePlayer()
    {
        Save_script.savePlayer(this);
        Debug.Log("Player data saved!");
        _notification.message("Player data saved!", 3, "white");
    }

    public void loadPlayer()
    {
        CharacterData data = Save_script.loadPlayer();

        //customization
        Local_name = data.Local_name;
        Local_title = data.Local_title;
        Local_class = data.Local_class;
        Local_talent = data.Local_talent;
        hair_id = data.hair_id;
        eyes_id = data.eyes_id;
        nose_id = data.nose_id;
        mouth_id = data.mouth_id;
        body_id = data.body_id;
        //stats
        Local_xp = data.Local_xp;
        Local_needed_xp = data.Local_needed_xp;
        Local_level = data.Local_level;
        Local_money = data.Local_money;
        Inventory = data.Inventory;
        Equipments = data.Equipments;
        Spells = data.Spells;
        Local_max_health = data.Local_max_health;
        Local_max_resource = data.Local_max_resource;
        Local_damage = data.Local_damage;
        //defeated_enemies = data.defeated_enemies;
        completed_conversations = data.completed_conversations;
        accepted_quests = data.accepted_quests;
        completed_quests = data.completed_quests;


        //updateStats();

        _notification.message("Save loaded!", 3, "white");
    }
    public void getXP(int xp)
    {
        Local_xp += xp;
        _notification.message("+" + xp + " xp!", 3);
        while (Local_xp >= Local_needed_xp)
        {
            Local_xp = Local_xp - Local_needed_xp;
            Local_level++;
            Local_needed_xp += 50;

            _notification.message("You gained a level!", 3, "yellow");
        }

        XP_bar.GetComponent<Bar_script>().updateXP();
    }

    public void updateStats()
    {
        var _nameandtitle = NameAndTitle.GetComponent<TextMeshProUGUI>();
        NameAndTitle.GetComponent<Text_animation>().startAnim(_nameandtitle.text = Local_name + " " + Local_title, 0.01f);

        var _statistics = Statistics.GetComponent<TextMeshProUGUI>();
        Statistics.GetComponent<Text_animation>().startAnim("Health: " + Local_max_health + "\n" +
            "Resource: " + Local_max_resource + "\n" +
            "Damage: " + Local_damage, 5f);


        //GameObject.Find("XP_bar").GetComponent<Bar_script>().updateXP();
        updateMoneyStat();

    }

    public void updateMoneyStat()
    {
        Money.GetComponent<Text_animation>().startAnim(Local_money + " credit", 0.01f);
    }

    public void buyItem(int slot_id)
    {
        if (Local_money >= gameObject.GetComponent<Item_script>().items[gameObject.GetComponent<Store_manager>().Store[slot_id]].attributes[3])
        {
            giveMoney(gameObject.GetComponent<Item_script>().items[gameObject.GetComponent<Store_manager>().Store[slot_id]].attributes[3]);
            itemPickup(gameObject.GetComponent<Store_manager>().Store[slot_id], false);
            gameObject.GetComponent<Store_manager>().Store[slot_id] = 0;
        }
        else
        {
            _notification.message("You don't have enough credit to buy this!", 3, "red");
        }
    }

    public void unequipItem(int slot_id)
    {
        Local_max_health -= _itemScript.items[Equipments[slot_id]].attributes[0];
        Local_max_resource -= _itemScript.items[Equipments[slot_id]].attributes[1];
        Local_damage -= _itemScript.items[Equipments[slot_id]].attributes[2];

        itemPickup(Equipments[slot_id], false);
        _itemScript.GetComponent<Character_stats>().Equipments[slot_id] = 0;
        updateStats();
    }

    public void replaceItem(int equipment_slot_id, int sender_slot_id, int item_id)
    {
        int tmp = Equipments[equipment_slot_id];

        Local_max_health -= _itemScript.items[Equipments[equipment_slot_id]].attributes[0];
        Local_max_resource -= _itemScript.items[Equipments[equipment_slot_id]].attributes[1];
        Local_damage -= _itemScript.items[Equipments[equipment_slot_id]].attributes[2];


        Equipments[equipment_slot_id] = item_id;

        Local_max_health += _itemScript.items[item_id].attributes[0];
        Local_max_resource += _itemScript.items[item_id].attributes[1];
        Local_damage += _itemScript.items[item_id].attributes[2];

        Inventory[sender_slot_id] = tmp;

        updateStats();
    }

    public void changePlayerName()
    {
        Local_name = iField.text;
        GameObject.Find("Game manager").GetComponent<Game_manager>().Change_screen(GameObject.Find("Game manager").GetComponent<Game_manager>().current_screen);
    }
    public void equipItem(int slot_id, int item_id, int sender_slot_id)
    {
        if (_itemScript.items[item_id].level <= Local_level)
        {
            if (Equipments[slot_id] == 0)
            {
                Equipments[slot_id] = item_id;
                Local_max_health += _itemScript.items[item_id].attributes[0];
                Local_max_resource += _itemScript.items[item_id].attributes[1];
                Local_damage += _itemScript.items[item_id].attributes[2];

                Inventory[sender_slot_id] = 0;

                updateStats();
            }
            else
            {
                replaceItem(slot_id, sender_slot_id, item_id);
            }
        }
        else
        {
            _notification.message("You can't equip that!", 3, "red");
            var exit_btn = GameObject.Find("Exit button item_preview");
            exit_btn.GetComponent<Close_button_script>().otherFunctions();
        }

    }

    public void deleteItem(string mode, int slot_id)
    {
        if (mode == "equip")
        {

            var item = _itemScript.items[_itemScript.GetComponent<Character_stats>().Inventory[slot_id]];

            _itemScript.GetComponent<Character_stats>().getMoney(item.attributes[3]);

            _itemScript.GetComponent<Character_stats>().Inventory[slot_id] = 0;
            var exit_btn = GameObject.Find("Exit button item_preview");
            exit_btn.GetComponent<Close_button_script>().Close();
        }
        else if (mode == "unequip")
        {

            var item = _itemScript.items[_itemScript.GetComponent<Character_stats>().Equipments[slot_id]];

            Local_max_health -= _itemScript.items[Equipments[slot_id]].attributes[0];
            Local_max_resource -= _itemScript.items[Equipments[slot_id]].attributes[1];
            Local_damage -= _itemScript.items[Equipments[slot_id]].attributes[2];

            _itemScript.GetComponent<Character_stats>().getMoney(item.attributes[3]);

            _itemScript.GetComponent<Character_stats>().Equipments[slot_id] = 0;
            var exit_btn = GameObject.Find("Exit button item_preview");
            exit_btn.GetComponent<Close_button_script>().Close();

            updateStats();
        }
    }

    public void sortInventory()
    {
        for (int i = 0; i < Inventory.Length - 1; i++)
        {
            for (int j = i + 1; j < Inventory.Length; j++)
            {
                int _compareQuality_1 = -1;

                switch (_itemScript.items[Inventory[i]].rarity)
                {
                    case "legendary":
                        _compareQuality_1 = 6;
                        break;
                    case "epic":
                        _compareQuality_1 = 5;
                        break;
                    case "rare":
                        _compareQuality_1 = 4;
                        break;
                    case "uncommon":
                        _compareQuality_1 = 3;
                        break;
                    case "common":
                        _compareQuality_1 = 2;
                        break;
                    case "poor":
                        _compareQuality_1 = 1;
                        break;
                    case "quest":
                        _compareQuality_1 = 0;
                        break;
                    default:
                        break;
                }

                int _compareQuality_2 = 0;
                switch (_itemScript.items[Inventory[j]].rarity)
                {
                    case "legendary":
                        _compareQuality_2 = 6;
                        break;
                    case "epic":
                        _compareQuality_2 = 5;
                        break;
                    case "rare":
                        _compareQuality_2 = 4;
                        break;
                    case "uncommon":
                        _compareQuality_2 = 3;
                        break;
                    case "common":
                        _compareQuality_2 = 2;
                        break;
                    case "poor":
                        _compareQuality_2 = 1;
                        break;
                    case "quest":
                        _compareQuality_1 = 0;
                        break;
                    default:
                        break;
                }

                int temp;
                if (_compareQuality_1 < _compareQuality_2)
                {
                    temp = Inventory[i];
                    Inventory[i] = Inventory[j];
                    Inventory[j] = temp;
                }
            }
        }
    }

    public void itemPickup(int item_id, bool isNew)
    {
        if (isNew && checkInventorySpace())
        {
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i] == 0)
                {
                    var dec_items = _itemScript.declared_items[item_id];

                    var tmp_item = new Item(_itemScript.items.Count, dec_items.name, dec_items.type, dec_items.rarity, dec_items.min_rarity,
                    dec_items.max_rarity, dec_items.level, dec_items.description, dec_items.icon, dec_items.sprite,
                    dec_items.attributes[0], dec_items.attributes[1], dec_items.attributes[2], dec_items.attributes[3]);
                    tmp_item.randomizeStats(10, 10, 10, 0, 5);

                    _itemScript.items.Add(tmp_item);
                    Inventory[i] = tmp_item.id;
                    _notification.message(_itemScript.items[item_id].name + " picked up.", 3, tmp_item.rarity);

                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i] == 0)
                {
                    Inventory[i] = _itemScript.items[item_id].id;
                    break;
                }
            }
        }

    }
    public void itemPickup(int item_id, bool isNew, bool isQuest)
    {
        if (isNew && checkInventorySpace())
        {
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i] == 0)
                {
                    Inventory[i] = _itemScript.items[item_id].id;
                    _notification.message(_itemScript.items[item_id].name + " picked up.", 3, "quest");
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i] == 0)
                {
                    Inventory[i] = _itemScript.items[item_id].id;
                    break;
                }
            }
        }
    }

    public bool checkInventorySpace()
    {
        if (Inventory[0] != 0 && Inventory[1] != 0 && Inventory[2] != 0 && Inventory[3] != 0 && Inventory[4] != 0 &&
        Inventory[5] != 0 && Inventory[6] != 0 && Inventory[7] != 0 && Inventory[8] != 0 && Inventory[9] != 0)
        {
            _notification.message("Inventory is full!", 3, "red");
            return false;
        }
        else { return true; }
    }
    public int getPercentOfXP()
    {
        return Convert.ToInt32(Math.Round(((double)Local_xp / (double)Local_needed_xp) * 100));
    }

    public int getPercentOfHealth()
    {
        return Convert.ToInt32(Math.Round(((double)Local_health / (double)Local_max_health) * 100));
    }

    public int getPercentOfHealth(double percentage)
    {
        double onePercent = percentage * 0.01;
        return Convert.ToInt32(Math.Round(((double)Local_max_health * onePercent)));
    }


    public int getPercentOfResource(double percentage)
    {
        return Convert.ToInt32(Math.Round(((double)Local_max_resource * percentage)));
    }

    public void getMoney(int amount)
    {
        Local_money += amount;
        _notification.message("+" + amount + " credit", 3);
        updateStats();

    }

    public void giveMoney(int amount)
    {
        Local_money -= amount;
        _notification.message("-" + amount + " credit", 3);
        updateStats();
    }

    public void looseHealth(int amount)
    {
        Local_health -= amount;

        if ((Local_health - amount) < 0)
        {
            Local_health = 0;
        }

        _notification.message("-" + amount + " health", 3, "red");
        GameObject.Find("Health_bar").GetComponent<Bar_script>().updateHealth();
    }

    public void getHealth(int amount)
    {
        if ((Local_health + amount) > Local_max_health)
        {
            Local_health = Local_max_health;
        }
        else { Local_health += amount; }
        _notification.message("+" + amount + " health", 3, "uncommon");
    }

    public void looseResource(int amount)
    {
        Local_resource -= amount;

        if ((Local_resource - amount) < 0)
        {
            Local_resource = 0;
        }

        _notification.message("-" + amount + " resource", 3, "blue");
        GameObject.Find("Resource_bar").GetComponent<Bar_script>().updateResource();
    }

    public void getResource(int amount)
    {
        if ((Local_resource + amount) > Local_max_resource)
        {
            Local_resource = Local_max_health;
        }
        else { Local_resource += amount; }

        _notification.message("+" + amount + " resource", 3, "uncommon");
    }
}



