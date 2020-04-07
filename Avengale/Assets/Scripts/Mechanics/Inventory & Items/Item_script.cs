using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum item_type { head, body, legs, left_arm, shoulder, gadget, feet, right_arm, quest }
public enum rarity { poor, common, uncommon, rare, epic, legendary, quest, empty }

public class Item_script : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public List<Item> declared_items = new List<Item>();

    public List<Item> quest_items = new List<Item>();
    public int counter;



    void Awake()
    {


        /*----------------------------------------------------------------------------------
            ID, NAME, TYPE, RARITY, MINIMUM RARITY, MAXIMUM RARITY, LEVEL, DESCRIPTION, ICON, SPRITE, HEALTH, RESOURCE, DAMAGE, VALUE
        ----------------------------------------------------------------------------------*/

        declared_items.AddRange(new List<Item>()
        {
            {new Item(0, "", item_type.body, rarity.empty, -1, -1, 1, "", null, null, null, 0, 0, 0, 0)},
            {new Item(1, "Cultist's hood", item_type.head, rarity.common, 0, 2, 5, "A simple piece of cloth used as a hood by the cultists.", "Item_icons/001", "Item_appearances/001","Item_appearances/001", 10, 5, 0, 100)},
            {new Item(2, "Cultist's robe", item_type.body, rarity.common, 0, 2, 5, "Every cultist gets one robe after the initiation ritual.", "Item_icons/002", "Item_appearances/002","Item_appearances/002_f", 20, 10, 5, 150)},
            {new Item(3, "Cultist's kilt", item_type.legs, rarity.common, 0, 2, 5, "Normal pants with a cloth hanging from it.", "Item_icons/003", "Item_appearances/003","Item_appearances/003", 5, 10, 5, 50)},
            {new Item(4, "Light shield", item_type.left_arm, rarity.common, 1, 3, 1, "Military-grade shield, used by the guards of the station", "Item_icons/004", "Item_appearances/004","Item_appearances/004", 25, 0, 15, 30)},
            {new Item(5, "Cultist's shoulderpad", item_type.shoulder, rarity.common, 0, 2, 5, "A leather shoulderpad. Very old-fashioned.", "Item_icons/005", "Item_appearances/005","Item_appearances/005", 10, 10, 0, 70)},
            {new Item(6, "Fresh water", item_type.gadget, rarity.uncommon, 0, 2, 5, "It's important to be hydrated all the time!", "Item_icons/006", null,null, 0, 25, 0, 50)},
            {new Item(7, "Cultist's boots", item_type.feet, rarity.common, 0, 2, 5, "Leather boots with metal reinforcement.", "Item_icons/007", "Item_appearances/007","Item_appearances/007", 10, 5, 5, 30)},
            {new Item(8, "Simple baton", item_type.right_arm, rarity.epic, 0, 2, 1, "A baton is the recruit's first real weapon. It's simple, but it can hit very hard.", "Item_icons/008", "Item_appearances/008","Item_appearances/008", 0, 5, 15, 50)},
            {new Item(9, "Recruit jacket", item_type.body, rarity.poor, 0, 2, 1, "The color of this jacket is representing the wearer's rank.", "Item_icons/009", "Item_appearances/009","Item_appearances/009_f", 5, 5, 5, 50)},
            {new Item(10, "Recruit pants", item_type.legs, rarity.poor, 0, 2, 2, "Simple military-grade pants.", "Item_icons/010", "Item_appearances/010","Item_appearances/010", 5, 5, 0, 25)},
            {new Item(11, "Recruit jacket", item_type.body, rarity.poor, 0, 2, 1, "The color of this jacket is representing the wearer's rank.", "Item_icons/011", "Item_appearances/011","Item_appearances/011_f", 5, 5, 5, 60)},
            {new Item(12, "Recruit boots", item_type.feet, rarity.poor, 0, 2, 1, "Ergonomic boots for daily work.", "Item_icons/012", "Item_appearances/012","Item_appearances/012", 0, 5, 0, 25)},
            {new Item(13, "Recruit shoulder guard", item_type.shoulder, rarity.poor, 0, 2, 3, "This shoulder guard used by the combat-ready recruits in the station.", "Item_icons/013", "Item_appearances/013","Item_appearances/013", 5, 5, 0, 100)},
            {new Item(14, "Energy sword", item_type.right_arm, rarity.epic, 4, 5, 5, "The recruits who completed the trials and gain ranks in the military receives this sword. It an emblem of honor.", "Item_icons/014", "Item_appearances/014","Item_appearances/014", 15, 20, 45, 300)},
            {new Item(15, "9mm handgun", item_type.right_arm, rarity.uncommon, 2, 3, 3, "Military-grade handgun, used by the guards of the station.", "Item_icons/015", "Item_appearances/015","Item_appearances/015", 15, 20, 30, 150)},
            {new Item(16, "Essential gear", item_type.quest, rarity.quest, 0, 0, 1, "An essential part of something important.", "Item_icons/016", null,null, 0, 0, 0, 100)},

        });
        counter = declared_items.Count - 1;

        foreach (var item in declared_items)
        {
            items.Add(item);
        }
        gameObject.GetComponent<Character_stats>().updateStats();
    }

    public void saveItems()
    {
        Save_script.saveItems(this);
        Debug.Log("Item data saved!");
    }

    public void loadItems()
    {
        ItemData data = Save_script.loadItems();
        if (data != null)
        {
            items = data.items;
        }

    }


}
[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public item_type type;
    public rarity rarity;
    public int min_rarity;
    public int max_rarity;
    public int level;

    public string description;

    public string icon;
    public string sprite_male, sprite_female;
    public int[] attributes;

    public Item(int id, string name, item_type type, rarity rarity, int min_rarity, int max_rarity, int level, string description, string icon, string sprite_male, string sprite_female, int health, int resource, int damage, int value)
    {
        this.id = id;
        this.name = name;
        this.rarity = rarity;
        this.level = level;
        this.description = description;
        this.type = type;

        this.icon = icon;
        this.sprite_male = sprite_male;
        this.sprite_female = sprite_female;

        int[] tmp = new int[4];
        tmp[0] = health;
        tmp[1] = resource;
        tmp[2] = damage;
        tmp[3] = value;
        this.attributes = tmp;
        this.min_rarity = min_rarity;
        this.max_rarity = max_rarity;
        //randomizeRarity(min_rarity, max_rarity);
    }

    public void randomizeRarity(int min_rarity, int max_rarity)
    {
        if (rarity != rarity.quest)
        {
            int rarity_random = Random.Range(min_rarity, max_rarity + 1);
            switch (rarity_random)
            {
                case 0:
                    rarity = rarity.poor;
                    break;
                case 1:
                    rarity = rarity.common;
                    break;
                case 2:
                    rarity = rarity.uncommon;
                    break;
                case 3:
                    rarity = rarity.rare;
                    break;
                case 4:
                    rarity = rarity.epic;
                    break;
                case 5:
                    rarity = rarity.legendary;
                    break;
                default:
                    rarity = rarity.empty;
                    break;
            }
        }
    }
    public void randomizeStats(int h, int r, int d)
    {

        Random rnd = new Random();

        attributes[0] = Random.Range(attributes[0], attributes[0] + h);
        attributes[1] = Random.Range(attributes[1], attributes[1] + r);
        attributes[2] = Random.Range(attributes[2], attributes[2] + d);
        attributes[3] = Random.Range(0, 200);

        var _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        if (_characterStats.Local_level > 3)
        {
            level = Random.Range(_characterStats.Local_level - 2, _characterStats.Local_level + 2);
        }
        else
        {
            level = Random.Range(_characterStats.Local_level, _characterStats.Local_level + 2);
        }
        randomizeRarity(min_rarity, max_rarity);
        //randomizeRarity(min_rarity, max_rarity);
    }

    public void upgradeItem(int attribute, int value)
    {
        attributes[attribute] += value;
    }

}
