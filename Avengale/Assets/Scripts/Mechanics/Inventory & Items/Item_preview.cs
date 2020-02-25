using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item_preview : MonoBehaviour
{
    [Header("Item properties")]
    public GameObject item_name;
    public GameObject item_description;
    public GameObject item_icon;
    public GameObject item_rarity_and_type;
    public GameObject level;

    [Header("Item attributes")]
    public GameObject attr_health;
    public GameObject attr_resource;
    public GameObject attr_damage;
    public GameObject attr_value;

    public GameObject slot_border;


    static Color32 red;
    static Color32 gray;
    static Color32 white;
    static Color32 green;
    static Color32 blue;
    static Color32 purple;
    static Color32 yellow;
    static Color32 quest;


    private void Start()
    {

        var colors = GameObject.Find("Game manager").GetComponent<Game_manager>();

        red = colors.red;
        gray = colors.gray;
        white = colors.white;
        green = colors.green;
        blue = colors.blue;
        purple = colors.purple;
        yellow = colors.yellow;
        quest = colors.quest;

    }

    private void Update() {
        if(gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Item_preview_slide_out_anim") && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            GameObject.Find("Exit button item_preview").GetComponent<Close_button_script>().Close();
        }
        
    }


    public void showEquippedItem(int item_id)
    {
        gameObject.GetComponent<Animator>().Play("Item_preview_slide_in_anim", -1, 0f);
        gameObject.GetComponent<Animator>().Play("Item_preview_slide_in_anim");
        GameObject.Find("Authorization").GetComponent<Animator>().Play("Authorization_slide_out_anim");


        var gameManager = GameObject.Find("Game manager").GetComponent<Item_script>();

        item_name.GetComponent<Text_animation>().startAnim(gameManager.items[item_id].name, 0.05f);
        item_description.GetComponent<Text_animation>().startAnim("<i>" + gameManager.items[item_id].description, 1f);
        item_rarity_and_type.GetComponent<Text_animation>().startAnim("[" + gameManager.items[item_id].rarity + " " + gameManager.items[item_id].type + "]", 1f);

        item_icon.GetComponent<SpriteRenderer>().sprite = gameManager.items[item_id].icon;

        level.GetComponent<Text_animation>().startAnim("<b>level " + gameManager.items[item_id].level, 1f);
        attr_health.GetComponent<Text_animation>().startAnim(gameManager.items[item_id].attributes[0] + " health", 0.05f);
        attr_resource.GetComponent<Text_animation>().startAnim(gameManager.items[item_id].attributes[1] + " resource", 0.05f);
        attr_damage.GetComponent<Text_animation>().startAnim(gameManager.items[item_id].attributes[2] + " damage", 0.05f);

        attr_value.GetComponent<Text_animation>().startAnim(gameManager.items[item_id].attributes[3] + " credit", 0.05f);


        //Attributes
        attr_health.GetComponent<TextMeshPro>().color = white;
        attr_resource.GetComponent<TextMeshPro>().color = white;
        attr_damage.GetComponent<TextMeshPro>().color = white;

        //Quality
        if (gameManager.items[item_id].rarity == "quest")
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = quest;
            item_description.GetComponent<TextMeshPro>().color = quest;
            item_name.GetComponent<TextMeshPro>().color = quest;
            slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().quest;
        }

        if (gameManager.items[item_id].rarity == "poor")
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = gray;
            item_description.GetComponent<TextMeshPro>().color = gray;
            item_name.GetComponent<TextMeshPro>().color = gray;
            slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().gray;

        }
        if (gameManager.items[item_id].rarity == "common")
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = white;
            item_description.GetComponent<TextMeshPro>().color = gray;
            item_name.GetComponent<TextMeshPro>().color = white;
            slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().white;
        }

        if (gameManager.items[item_id].rarity == "uncommon")
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = green;
            item_description.GetComponent<TextMeshPro>().color = gray;
            item_name.GetComponent<TextMeshPro>().color = green;
            slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().green;
        }
        if (gameManager.items[item_id].rarity == "rare")
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = blue;
            item_description.GetComponent<TextMeshPro>().color = gray;
            item_name.GetComponent<TextMeshPro>().color = blue;
            slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().blue;
        }

        if (gameManager.items[item_id].rarity == "epic")
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = purple;
            item_description.GetComponent<TextMeshPro>().color = gray;
            item_name.GetComponent<TextMeshPro>().color = purple;
            slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().purple;
        }

        if (gameManager.items[item_id].rarity == "legendary")
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = yellow;
            item_description.GetComponent<TextMeshPro>().color = gray;
            item_name.GetComponent<TextMeshPro>().color = yellow;
            slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().yellow;
        }


        GameObject.Find("Equip_button").GetComponent<Text_animation>().startAnim("<b>Unequip", 0.05f);
        GameObject.Find("Equip_button").GetComponent<Item_equip_script>().mode = "unequip";
        GameObject.Find("Delete_button").GetComponent<Item_delete_script>().mode = "unequip";

    }
    public void showItem(int item_id, bool isStore)
    {
        gameObject.GetComponent<Animator>().Play("Item_preview_slide_in_anim", -1, 0f);
        gameObject.GetComponent<Animator>().Play("Item_preview_slide_in_anim");

        var gameManager = GameObject.Find("Game manager").GetComponent<Item_script>();
        item_name.GetComponent<Text_animation>().startAnim(gameManager.items[item_id].name, 0.05f);
        item_description.GetComponent<Text_animation>().startAnim("<i>" + gameManager.items[item_id].description, 1f);

        if (gameManager.items[item_id].type != "quest")
        {
            item_rarity_and_type.GetComponent<Text_animation>().startAnim("[" + gameManager.items[item_id].rarity + " " + gameManager.items[item_id].type + "]", 1f);
        }
        else if (gameManager.items[item_id].type == "quest")
        {
            item_rarity_and_type.GetComponent<Text_animation>().startAnim("[" + gameManager.items[item_id].type + "]", 1f);
        }

        item_icon.GetComponent<SpriteRenderer>().sprite = gameManager.items[item_id].icon;

        if (gameManager.items[item_id].level > GameObject.Find("Game manager").GetComponent<Character_stats>().Local_level)
        {
            level.GetComponent<TextMeshPro>().color = red;
        }
        else { level.GetComponent<TextMeshPro>().color = white; }
        level.GetComponent<Text_animation>().startAnim("requires <b>level " + gameManager.items[item_id].level, 1f);

        if (!isStore)
        {
            GameObject.Find("Equip_button").GetComponent<Text_animation>().startAnim("<b>Equip", 0.05f);
            GameObject.Find("Delete_button").GetComponent<Text_animation>().startAnim("Sell", 0.05f);
            GameObject.Find("Equip_button").GetComponent<Item_equip_script>().mode = "equip";
            GameObject.Find("Delete_button").GetComponent<Item_delete_script>().mode = "equip";
        }
        else
        {
            GameObject.Find("Equip_button").GetComponent<Text_animation>().startAnim("<b>Buy", 0.05f);
            GameObject.Find("Delete_button").GetComponent<Text_animation>().startAnim("", 0.05f);

            GameObject.Find("Equip_button").GetComponent<Item_equip_script>().mode = "buy";
            GameObject.Find("Delete_button").GetComponent<Item_delete_script>().mode = "buy";
        }

        var character_stats = gameManager.GetComponent<Character_stats>();
        int health_to_compare = 0;
        int resource_to_compare = 0;
        int damage_to_compare = 0;

        int slot_id = 0;
        #region compare_switch
        switch (gameManager.items[item_id].type)
        {
            case "head":
                health_to_compare = gameManager.items[character_stats.Equipments[0]].attributes[0];
                resource_to_compare = gameManager.items[character_stats.Equipments[0]].attributes[1];
                damage_to_compare = gameManager.items[character_stats.Equipments[0]].attributes[2];

                slot_id = 0;
                break;
            case "body":
                health_to_compare = gameManager.items[character_stats.Equipments[1]].attributes[0];
                resource_to_compare = gameManager.items[character_stats.Equipments[1]].attributes[1];
                damage_to_compare = gameManager.items[character_stats.Equipments[1]].attributes[2];
                slot_id = 1;
                break;
            case "legs":
                health_to_compare = gameManager.items[character_stats.Equipments[2]].attributes[0];
                resource_to_compare = gameManager.items[character_stats.Equipments[2]].attributes[1];
                damage_to_compare = gameManager.items[character_stats.Equipments[2]].attributes[2];
                slot_id = 2;
                break;
            case "left arm":
                health_to_compare = gameManager.items[character_stats.Equipments[3]].attributes[0];
                resource_to_compare = gameManager.items[character_stats.Equipments[3]].attributes[1];
                damage_to_compare = gameManager.items[character_stats.Equipments[3]].attributes[2];
                slot_id = 3;
                break;
            case "shoulder":
                health_to_compare = gameManager.items[character_stats.Equipments[4]].attributes[0];
                resource_to_compare = gameManager.items[character_stats.Equipments[4]].attributes[1];
                damage_to_compare = gameManager.items[character_stats.Equipments[4]].attributes[2];
                slot_id = 4;
                break;
            case "gadget":
                health_to_compare = gameManager.items[character_stats.Equipments[5]].attributes[0];
                resource_to_compare = gameManager.items[character_stats.Equipments[5]].attributes[1];
                damage_to_compare = gameManager.items[character_stats.Equipments[5]].attributes[2];
                slot_id = 5;
                break;
            case "feet":
                health_to_compare = gameManager.items[character_stats.Equipments[6]].attributes[0];
                resource_to_compare = gameManager.items[character_stats.Equipments[6]].attributes[1];
                damage_to_compare = gameManager.items[character_stats.Equipments[6]].attributes[2];
                slot_id = 6;
                break;
            case "right arm":
                health_to_compare = gameManager.items[character_stats.Equipments[6]].attributes[0];
                resource_to_compare = gameManager.items[character_stats.Equipments[6]].attributes[1];
                damage_to_compare = gameManager.items[character_stats.Equipments[6]].attributes[2];
                slot_id = 7;
                break;
            default:
                break;
        }
        #endregion

        if (gameManager.items[item_id].type != "quest")
        {
            attr_health.GetComponent<Text_animation>().startAnim("+" + gameManager.items[item_id].attributes[0] +
            " health (" + (gameManager.items[character_stats.Equipments[slot_id]].attributes[0]) + " ¤ " + (gameManager.items[item_id].attributes[0]) + ")", 0.05f);
            attr_resource.GetComponent<Text_animation>().startAnim("+" + gameManager.items[item_id].attributes[1] +
            " resource (" + (gameManager.items[character_stats.Equipments[slot_id]].attributes[1]) + " ¤ " + (gameManager.items[item_id].attributes[1]) + ")", 0.05f);
            attr_damage.GetComponent<Text_animation>().startAnim("+" + gameManager.items[item_id].attributes[2] +
            " damage (" + (gameManager.items[character_stats.Equipments[slot_id]].attributes[2]) + " ¤ " + (gameManager.items[item_id].attributes[2]) + ")", 0.05f);
            
            attr_value.GetComponent<Text_animation>().startAnim(gameManager.items[item_id].attributes[3] + " credit", 0.05f);
        }
        else
        {
            level.GetComponent<Text_animation>().startAnim("", 1f);
            attr_health.GetComponent<Text_animation>().startAnim("", 1f);
            attr_resource.GetComponent<Text_animation>().startAnim("", 1f);
            attr_damage.GetComponent<Text_animation>().startAnim("", 1f);
            attr_value.GetComponent<Text_animation>().startAnim("", 1f);
        }

        //Attributes
        if (health_to_compare < gameManager.items[item_id].attributes[0])
        { attr_health.GetComponent<TextMeshPro>().color = green; }
        else if (health_to_compare > gameManager.items[item_id].attributes[0])
        { attr_health.GetComponent<TextMeshPro>().color = red; }
        else if (health_to_compare == gameManager.items[item_id].attributes[0])
        { attr_health.GetComponent<TextMeshPro>().color = white; }

        if (resource_to_compare < gameManager.items[item_id].attributes[1])
        { attr_resource.GetComponent<TextMeshPro>().color = green; }
        else if (resource_to_compare > gameManager.items[item_id].attributes[1])
        { attr_resource.GetComponent<TextMeshPro>().color = red; }
        else if (resource_to_compare == gameManager.items[item_id].attributes[1])
        { attr_resource.GetComponent<TextMeshPro>().color = white; }

        if (damage_to_compare < gameManager.items[item_id].attributes[2])
        { attr_damage.GetComponent<TextMeshPro>().color = green; }
        else if (damage_to_compare > gameManager.items[item_id].attributes[2])
        { attr_damage.GetComponent<TextMeshPro>().color = red; }
        else if (damage_to_compare == gameManager.items[item_id].attributes[2])
        { attr_damage.GetComponent<TextMeshPro>().color = white; }


        //Quality

        if (gameManager.items[item_id].rarity == "quest")
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = quest;
            item_description.GetComponent<TextMeshPro>().color = quest;
            item_name.GetComponent<TextMeshPro>().color = quest;
            slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().quest;
        }

        if (gameManager.items[item_id].rarity == "poor")
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = gray;
            item_description.GetComponent<TextMeshPro>().color = gray;
            item_name.GetComponent<TextMeshPro>().color = gray;
            slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().gray;

        }
        if (gameManager.items[item_id].rarity == "common")
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = white;
            item_description.GetComponent<TextMeshPro>().color = gray;
            item_name.GetComponent<TextMeshPro>().color = white;
            slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().white;
        }

        if (gameManager.items[item_id].rarity == "uncommon")
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = green;
            item_description.GetComponent<TextMeshPro>().color = gray;
            item_name.GetComponent<TextMeshPro>().color = green;
            slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().green;
        }
        if (gameManager.items[item_id].rarity == "rare")
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = blue;
            item_description.GetComponent<TextMeshPro>().color = gray;
            item_name.GetComponent<TextMeshPro>().color = blue;
            slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().blue;
        }

        if (gameManager.items[item_id].rarity == "epic")
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = purple;
            item_description.GetComponent<TextMeshPro>().color = gray;
            item_name.GetComponent<TextMeshPro>().color = purple;
            slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().purple;
        }

        if (gameManager.items[item_id].rarity == "legendary")
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = yellow;
            item_description.GetComponent<TextMeshPro>().color = gray;
            item_name.GetComponent<TextMeshPro>().color = yellow;
            slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().yellow;
        }
    }
}
