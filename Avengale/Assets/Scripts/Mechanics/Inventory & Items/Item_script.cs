using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum item_type { head, body, legs, left_arm, shoulder, gadget, feet, right_arm, quest }
public enum rarity { poor, common, uncommon, rare, epic, legendary, quest }

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
            {new Item(0, "", item_type.body, rarity.poor, 0, 0, 1, "", null, null, 0, 0, 0, 0)},
            {new Item(1, "Test head", item_type.head, rarity.common, 0, 2, 1, "\tDuis facilisis sodales urna, et ultricies nisl. Nullam mattis erat sed quam blandit vehicula. Mauris et iaculis massa.", "Item_icons/001", "Item_appearances/001", 10, 0, 200, 100)},
            {new Item(2, "test_body", item_type.body, rarity.uncommon, 0, 2, 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec nec est eget est mollis venenatis. Praesent consectetur ut tortor vitae rutrum. ", "Item_icons/002", "Item_appearances/002", 50, 9, 1, 210)},
            {new Item(3, "test_legs", item_type.legs, rarity.poor, 0, 2, 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin aliquam tortor a augue pulvinar ornare. Nulla pellentesque porttitor erat, semper ultrices ligula pretium quis.", "Item_icons/003", "Item_appearances/003", 0, 0, 0, 10)},
            {new Item(4, "test_left", item_type.left_arm, rarity.epic, 4, 5, 1, " Fusce eu orci condimentum, dictum ante in, venenatis dui. In in aliquam justo. Curabitur lorem nulla, efficitur vel quam sit amet, dignissim consectetur purus.", "Item_icons/Icon2", "Item_appearances/004", 0, 0, 0, 30)},
            {new Item(5, "test_shoulder", item_type.shoulder, rarity.rare, 0, 2, 1, "Phasellus sit amet diam malesuada, volutpat nulla non, pretium elit. Maecenas egestas mauris vel dui ultricies tincidunt. ", "Item_icons/005", "Item_appearances/005", 0, 0, 0, 40)},
            {new Item(6, "test_gadget", item_type.gadget, rarity.uncommon, 1, 2, 1, "Nullam enim dolor, posuere quis lacus ut, sodales aliquet est. Quisque sed dolor non ex porta pulvinar.", "Item_icons/Icon2", "Item_appearances/legs", 0, 0, 0, 15)},
            {new Item(7, "test_feet", item_type.feet, rarity.legendary, 4, 5, 1, "Etiam dapibus leo vehicula ipsum hendrerit sodales. Phasellus nec neque nibh.", "Item_icons/007", "Item_appearances/007", 0, 0, 0, 5)},
            {new Item(8, "test_right", item_type.right_arm, rarity.epic, 0, 2, 1, "In metus ante, malesuada nec libero non, laoreet condimentum lectus. ", "Item_icons/Icon2", "Item_appearances/003", 0, 0, 0, 100)},
            {new Item(9, "Worn jacket", item_type.body, rarity.legendary, 5, 5, 1, "Nam nisi diam, egestas vitae odio ut, commodo facilisis lectus. Suspendisse efficitur sodales erat nec molestie.", "Item_icons/Icon", "Item_appearances/002", 32, 100, 15, 200)},
            {new Item(10, "Padoru's hat", item_type.head, rarity.legendary, 5, 5, 2, "Hasire sori yo kaze no you ni tsukimihara wo\nPADORU PADORU", "Item_icons/001", "Item_appearances/001", 100, 100, 100, 125)},
            {new Item(11, "Test quest item", item_type.quest, rarity.quest, 0, 0, 1, "Test quest item description.", "Item_icons/001", null, 0, 0, 0, 0)},
            {new Item(12, "Test quest 2", item_type.quest, rarity.quest, 0, 0, 1, " venenatis dui. In in aliquam justo. Curabitur lorem.", "Item_icons/002", null, 0, 0, 0, 0)}
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
        items = data.items;

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
    public string sprite;
    public int[] attributes;

    public Item(int id, string name, item_type type, rarity rarity, int min_rarity, int max_rarity, int level, string description, string icon, string sprite, int health, int resource, int damage, int value)
    {
        this.id = id;
        this.name = name;
        this.rarity = rarity;
        this.level = level;
        this.description = description;
        this.type = type;

        this.icon = icon;
        this.sprite = sprite;

        int[] tmp = new int[4];
        tmp[0] = health;
        tmp[1] = resource;
        tmp[2] = damage;
        tmp[3] = value;
        this.attributes = tmp;
        randomizeRarity(min_rarity, max_rarity);
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
                    break;
            }
        }
    }
    public void randomizeStats(int h, int r, int d, int min_rarity, int max_rarity)
    {

        Random rnd = new Random();

        attributes[0] = Random.Range(attributes[0], attributes[0] + h);
        attributes[1] = Random.Range(attributes[1], attributes[1] + r);
        attributes[2] = Random.Range(attributes[2], attributes[2] + d);
        attributes[3] = Random.Range(0, 200);

        level = Random.Range(level, level + 10);

        randomizeRarity(min_rarity, max_rarity);
    }

    public void upgradeItem(int attribute, int value)
    {
        attributes[attribute] += value;
    }

}
