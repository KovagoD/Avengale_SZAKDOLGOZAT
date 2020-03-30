using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;

public class Character_stats : MonoBehaviour
{
    public int Local_max_health = 0, Local_health = 0, Local_max_resource = 0, Local_resource = 0, Local_damage = 0, Local_money = 0, Local_spell_points = 0;
    public List<Enemy> defeated_enemies = new List<Enemy>();
    public List<Conversation> completed_conversations = new List<Conversation>();
    public int[] accepted_quests = new int[3];
    public List<Quest> completed_quests = new List<Quest>();
    public string Local_name = "Unknown", Local_title = "the Anone";
    public int Local_class = 1, Local_talent = 1;

    public bool sex, showHelmet;
    public int hair_id, eyes_id, nose_id, mouth_id, body_id;
    
    public byte[] hair_color = new byte[3] { 0, 0, 0 };

    public int Local_xp = 0, Local_needed_xp = 150, Local_level = 1;
    public int[] Inventory = new int[10], Equipments = new int[8], starterEquipments = new int[8] { 0, 9, 10, 0, 0, 0, 0, 0 };
    public int[] Spells = new int[5], Talents = new int[10];
    public List<Spell> Passive_spells = new List<Spell>();

    [Header("References")]
    public TextMeshProUGUI NameAndTitle, Statistics;
    public GameObject Money;
    public GameObject XP_bar;


    private Game_manager _gameManager;
    private Item_script _itemScript;
    private Ingame_notification_script _notification;

    public GameObject InventorySlots;

    private void Awake()
    {
        initializePlayer();
    }
    void Start()
    {
        _itemScript = GameObject.Find("Game manager").GetComponent<Item_script>();
        _gameManager = GameObject.Find("Game manager").GetComponent<Game_manager>();
        _notification = GameObject.Find("Notification").GetComponent<Ingame_notification_script>();
    }

    public void savePlayer()
    {
        Save_script.savePlayer(this);
        Debug.Log("Player data saved!");
        _notification.message("Player data saved!", 3, "white");
        Debug.Log(Application.persistentDataPath);
    }

    public void initializePlayer()
    {
        Local_max_health = 50; Local_health = 0; Local_max_resource = 50; Local_resource = 0; Local_damage = 10; Local_money = 100; Local_spell_points = 0;
        defeated_enemies = new List<Enemy>();
        completed_conversations = new List<Conversation>();
        accepted_quests = new int[3];
        completed_quests = new List<Quest>();
        Local_name = "Unknown"; Local_title = "the Unknown";
        Local_class = 1; Local_talent = 1;
        hair_id = 0; eyes_id = 0; nose_id = 0; mouth_id = 0; body_id = 0;
        Local_xp = 0; Local_needed_xp = 150; Local_level = 1;
        Inventory = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; Equipments = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        Spells = new int[5] { 1, 0, 0, 0, 0 };
        //Talents = new int[10];
        Passive_spells = new List<Spell>();
    }

    public void equipStarterItems()
    {
        for (int i = 0; i < starterEquipments.Length; i++)
        {
            if (starterEquipments[i] != 0)
            {
                equipItem(starterEquipments[i]);
            }
        }
    }

    public void loadPlayer()
    {
        CharacterData data = Save_script.loadPlayer();

        if (data == null)
        {
            _notification.message("Save cannot be loaded!", 3, "red");

        }
        else
        {
            //customization
            Local_name = data.Local_name;
            Local_title = data.Local_title;
            Local_class = data.Local_class;
            Local_talent = data.Local_talent;

            sex = data.sex;
            showHelmet = data.showHelmet;
            
            hair_id = data.hair_id;
            eyes_id = data.eyes_id;
            nose_id = data.nose_id;
            mouth_id = data.mouth_id;
            body_id = data.body_id;

            hair_color = data.hair_color;




            //stats
            Local_xp = data.Local_xp;
            Local_needed_xp = data.Local_needed_xp;
            Local_level = data.Local_level;
            Local_money = data.Local_money;
            Inventory = data.Inventory;
            Equipments = data.Equipments;
            Spells = data.Spells;
            Talents = data.Talents;
            Local_spell_points = data.Local_spell_points;
            Local_max_health = data.Local_max_health;
            Local_max_resource = data.Local_max_resource;
            Local_damage = data.Local_damage;
            //defeated_enemies = data.defeated_enemies;
            completed_conversations = data.completed_conversations;
            accepted_quests = data.accepted_quests;
            completed_quests = data.completed_quests;

            _gameManager.Change_screen(_gameManager.Character_screen_UI, true);
            _gameManager.isNewCharacter = true;
            gameObject.GetComponent<Spell_script>().initializeSpells();



            _notification.message("Save loaded!", 3, "white");
        }
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

            getSpellPoint(1);

            _notification.message("You gained a level!", 3, "yellow");
        }

        XP_bar.GetComponent<Bar_script>().updateXP();
    }

    public void updateStats()
    {
        NameAndTitle.GetComponent<Text_animation>().startAnim(Local_name, 0.01f);
        
        Statistics.GetComponent<Text_animation>().startAnim("Health: " + Local_max_health + "\n" +
            "Resource: " + Local_max_resource + "\n" +
            "Damage: " + Local_damage, 5f);

        updateMoneyStat();
    }

    public void updateMoneyStat()
    {
        Money.GetComponent<Text_animation>().startAnim(Local_money + " credit", 0.01f);
    }

    public bool isInventoryFull()
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == 0)
            {
                return false;
            }
        }
        _notification.message("Inventory is full!", 3, "red");
        InventorySlots.GetComponent<Inventory_ui_script>().showInventory();

        return true;
    }
    public void buyItem(int slot_id)
    {
        if (Local_money >= gameObject.GetComponent<Item_script>().items[gameObject.GetComponent<Store_manager>().Store[slot_id]].attributes[3])
        {
            if (!isInventoryFull())
            {
                itemPickup(gameObject.GetComponent<Store_manager>().Store[slot_id], false);
                giveMoney(gameObject.GetComponent<Item_script>().items[gameObject.GetComponent<Store_manager>().Store[slot_id]].attributes[3]);
                gameObject.GetComponent<Store_manager>().Store[slot_id] = 0;
            }
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

        if (!isInventoryFull())
        {
            itemPickup(Equipments[slot_id], false);
            _itemScript.GetComponent<Character_stats>().Equipments[slot_id] = 0;
            updateStats();
        }

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
        GameObject.Find("Game manager").GetComponent<Game_manager>().Change_screen(GameObject.Find("Game manager").GetComponent<Game_manager>().current_screen, false);
    }

    public void equipItem(int item_id)
    {
        _itemScript = GameObject.Find("Game manager").GetComponent<Item_script>();
        int slot_id = 0;
        switch (_itemScript.items[item_id].type)
        {
            case item_type.head:
                slot_id = 0;
                break;
            case item_type.body:
                slot_id = 1;
                break;
            case item_type.legs:
                slot_id = 2;
                break;
            case item_type.left_arm:
                slot_id = 3;
                break;
            case item_type.shoulder:
                slot_id = 4;
                break;
            case item_type.gadget:
                slot_id = 5;
                break;
            case item_type.feet:
                slot_id = 6;
                break;
            case item_type.right_arm:
                slot_id = 7;
                break;
        }
        Equipments[slot_id] = item_id;
        Local_max_health += _itemScript.items[item_id].attributes[0];
        Local_max_resource += _itemScript.items[item_id].attributes[1];
        Local_damage += _itemScript.items[item_id].attributes[2];

        gameObject.GetComponent<Spell_script>().initializeSpells();
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
                int _compareQuality_1 = 0;

                switch (_itemScript.items[Inventory[i]].rarity)
                {
                    case rarity.legendary:
                        _compareQuality_1 = 7;
                        break;
                    case rarity.epic:
                        _compareQuality_1 = 6;
                        break;
                    case rarity.rare:
                        _compareQuality_1 = 5;
                        break;
                    case rarity.uncommon:
                        _compareQuality_1 = 4;
                        break;
                    case rarity.common:
                        _compareQuality_1 = 3;
                        break;
                    case rarity.poor:
                        _compareQuality_1 = 2;
                        break;
                    case rarity.quest:
                        _compareQuality_1 = 1;
                        break;
                    case rarity.empty:
                        _compareQuality_1 = 0;
                        break;
                }

                int _compareQuality_2 = 0;
                switch (_itemScript.items[Inventory[j]].rarity)
                {
                    case rarity.legendary:
                        _compareQuality_2 = 7;
                        break;
                    case rarity.epic:
                        _compareQuality_2 = 6;
                        break;
                    case rarity.rare:
                        _compareQuality_2 = 5;
                        break;
                    case rarity.uncommon:
                        _compareQuality_2 = 4;
                        break;
                    case rarity.common:
                        _compareQuality_2 = 3;
                        break;
                    case rarity.poor:
                        _compareQuality_2 = 2;
                        break;
                    case rarity.quest:
                        _compareQuality_2 = 1;
                        break;
                    case rarity.empty:
                        _compareQuality_2 = 0;
                        break;
                }

                int temp;
                if (_compareQuality_1 < _compareQuality_2 || Inventory[i] == 0)
                {
                    temp = Inventory[i];
                    Inventory[i] = Inventory[j];
                    Inventory[j] = temp;
                }
            }
        }

        /*
        for (int i = 0; i < Inventory.Length; i++)
        {
            Debug.Log(i + " : " + _itemScript.items[Inventory[i]].rarity);
        }
        */
    }

    public void itemPickup(int item_id, bool isNew)
    {
        if (isNew && !isInventoryFull())
        {
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i] == 0)
                {
                    var dec_items = _itemScript.declared_items[item_id];

                    var tmp_item = new Item(_itemScript.items.Count, dec_items.name, dec_items.type, dec_items.rarity, dec_items.min_rarity,
                    dec_items.max_rarity, dec_items.level, dec_items.description, dec_items.icon, dec_items.sprite_male, dec_items.sprite_female,
                    dec_items.attributes[0], dec_items.attributes[1], dec_items.attributes[2], dec_items.attributes[3]);
                    tmp_item.randomizeStats(10, 10, 10, 0, 5);

                    _itemScript.items.Add(tmp_item);
                    Inventory[i] = tmp_item.id;
                    _notification.message(_itemScript.items[item_id].name + " picked up.", 3, tmp_item.rarity.ToString());

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
        if (isNew && !isInventoryFull())
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


    public void getSpellPoint(int amount)
    {
        Local_spell_points += amount;
        _notification.message("+" + amount + " skill point", 3);
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
        GameObject.Find("Health_bar").GetComponent<Bar_script>().updateHealthAddition();

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
        GameObject.Find("Resource_bar").GetComponent<Bar_script>().updateResourceAddition();

    }

}



