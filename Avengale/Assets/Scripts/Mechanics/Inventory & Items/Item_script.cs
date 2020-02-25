using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_script : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public List<Item> declared_items = new List<Item>();

    public List<Item> quest_items = new List<Item>();
    public int counter;



    void Awake()
    {

        /*----------------------------------------------------------------------------------
            ID, NAME, TYPE, RARITY, MINIMUM RARITY, MAXIMUM RARITY, LEVEL, DESCRIPTION, ICON, SPRITE, HEALTH, RESOURCE, RANGED DAMAGE, PHYSICAL DAMAGE, VALUE
        ----------------------------------------------------------------------------------*/

        declared_items.Add(new Item(0, "", "", "", 0, 0, 1, "", null, null, 0, 0, 0, 0, 0));
        declared_items.Add(new Item(1, "Test head", "head", "common", 0, 2, 1, "\tDuis facilisis sodales urna, et ultricies nisl. Nullam mattis erat sed quam blandit vehicula. Mauris et iaculis massa.", Resources.Load<Sprite>("Item_icons/001"), Resources.Load<Sprite>("Item_appearances/001"), 10, 0, 0, 200, 100));
        declared_items.Add(new Item(2, "test_body", "body", "rare", 0, 2, 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec nec est eget est mollis venenatis. Praesent consectetur ut tortor vitae rutrum. ", Resources.Load<Sprite>("Item_icons/002"), Resources.Load<Sprite>("Item_appearances/002"), 50, 9, 2, 1, 210));
        declared_items.Add(new Item(3, "test_legs", "legs", "poor", 0, 2, 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin aliquam tortor a augue pulvinar ornare. Nulla pellentesque porttitor erat, semper ultrices ligula pretium quis.", Resources.Load<Sprite>("Item_icons/003"), Resources.Load<Sprite>("Item_appearances/003"), 0, 0, 0, 0, 10));
        declared_items.Add(new Item(4, "test_left", "left arm", "epic", 4, 5, 1, " Fusce eu orci condimentum, dictum ante in, venenatis dui. In in aliquam justo. Curabitur lorem nulla, efficitur vel quam sit amet, dignissim consectetur purus.", Resources.Load<Sprite>("Item_icons/Icon2"), Resources.Load<Sprite>("Item_appearances/004"), 0, 0, 0, 0, 30));
        declared_items.Add(new Item(5, "test_shoulder", "shoulder", "common", 0, 2, 1, "Phasellus sit amet diam malesuada, volutpat nulla non, pretium elit. Maecenas egestas mauris vel dui ultricies tincidunt. ", Resources.Load<Sprite>("Item_icons/005"), Resources.Load<Sprite>("Item_appearances/005"), 0, 0, 0, 0, 40));
        declared_items.Add(new Item(6, "test_gadget", "gadget", "uncommon", 1, 2, 1, "Nullam enim dolor, posuere quis lacus ut, sodales aliquet est. Quisque sed dolor non ex porta pulvinar.", Resources.Load<Sprite>("Item_icons/Icon2"), Resources.Load<Sprite>("Item_appearances/legs"), 0, 0, 0, 0, 15));
        declared_items.Add(new Item(7, "test_feet", "feet", "legendary", 4, 5, 1, "Etiam dapibus leo vehicula ipsum hendrerit sodales. Phasellus nec neque nibh.", Resources.Load<Sprite>("Item_icons/007"), Resources.Load<Sprite>("Item_appearances/007"), 0, 0, 0, 0, 5));
        declared_items.Add(new Item(8, "test_right", "right arm", "uncommon", 0, 2, 1, "In metus ante, malesuada nec libero non, laoreet condimentum lectus. ", Resources.Load<Sprite>("Item_icons/Icon2"), Resources.Load<Sprite>("Item_appearances/003"), 0, 0, 0, 0, 100));
        declared_items.Add(new Item(9, "Worn jacket", "body", "legendary", 5, 5, 1, "Nam nisi diam, egestas vitae odio ut, commodo facilisis lectus. Suspendisse efficitur sodales erat nec molestie.", Resources.Load<Sprite>("Item_icons/Icon"), Resources.Load<Sprite>("Item_appearances/002"), 32, 10, 100, 15, 200));
        declared_items.Add(new Item(10, "Padoru's hat", "head", "legendary", 5, 5, 2, "Hasire sori yo kaze no you ni tsukimihara wo\nPADORU PADORU", Resources.Load<Sprite>("Item_icons/001"), Resources.Load<Sprite>("Item_appearances/001"), 100, 100, 100, 100, 125));
        declared_items.Add(new Item(11, "Test quest item", "quest", "quest", 0, 0, 1, "Test quest item description.", Resources.Load<Sprite>("Item_icons/001"), null, 0, 0, 0, 0, 0));
        declared_items.Add(new Item(12, "Test quest 2", "quest", "quest", 0, 0, 1, " venenatis dui. In in aliquam justo. Curabitur lorem.", Resources.Load<Sprite>("Item_icons/002"), null, 0, 0, 0, 0, 0));

        counter = declared_items.Count - 1;

        foreach (var item in declared_items)
        {
            items.Add(item);
        }
        gameObject.GetComponent<Character_stats>().updateStats();
    }

}
public class Item
{
    public int id;
    public string name;
    public string type;
    public string rarity;
    public int min_rarity;
    public int max_rarity;
    public int level;

    public string description;
    public Sprite icon;
    public Sprite sprite;
    public int[] attributes;

    public Item(int id, string name, string type, string rarity, int min_rarity, int max_rarity, int level, string description, Sprite icon, Sprite sprite, int health, int resource, int ranged_damage, int physical_damage, int value)
    {
        this.id = id;
        this.name = name;
        this.rarity = rarity;
        this.level = level;
        this.description = description;
        this.type = type;
        this.icon = icon;
        this.sprite = sprite;
        int[] tmp = new int[5];
        tmp[0] = health;
        tmp[1] = resource;
        tmp[2] = ranged_damage;
        tmp[3] = physical_damage;
        tmp[4] = value;
        this.attributes = tmp;
        randomizeRarity(min_rarity, max_rarity);
    }

    public void randomizeRarity(int min_rarity, int max_rarity)
    {
        if (rarity != "" && rarity != "quest")
        {
            int rarity_random = Random.Range(min_rarity, max_rarity + 1);
            switch (rarity_random)
            {
                case 0:
                    rarity = "poor";
                    break;
                case 1:
                    rarity = "common";
                    break;
                case 2:
                    rarity = "uncommon";
                    break;
                case 3:
                    rarity = "rare";
                    break;
                case 4:
                    rarity = "epic";
                    break;
                case 5:
                    rarity = "legendary";
                    break;
                default:
                    break;
            }
        }
    }
    public void randomizeStats(int h, int r, int r_d, int p_d, int min_rarity, int max_rarity)
    {

        Random rnd = new Random();

        attributes[0] = Random.Range(attributes[0], attributes[0] + h);
        attributes[1] = Random.Range(attributes[1], attributes[1] + r);
        attributes[2] = Random.Range(attributes[2], attributes[2] + r_d);
        attributes[3] = Random.Range(attributes[3], attributes[3] + p_d);
        attributes[4] = Random.Range(0, 200);

        level = Random.Range(level, level + 10);

        randomizeRarity(min_rarity, max_rarity);
    }

    public void upgradeItem(int attribute, int value)
    {
        attributes[attribute] += value;
    }

}
