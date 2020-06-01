using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item_preview : MonoBehaviour
{
    [Header("Item properties")]
    public GameObject item_name,item_description,item_icon,item_rarity_and_type,level;
    public GameObject attr_health,attr_resource,attr_damage,attr_value;
    public GameObject slot_border;

    private void Update()
    {
        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Item_preview_slide_out_anim") && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
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

        Colors colors = new Colors();

        item_name.GetComponent<Text_animation>().startAnim(gameManager.items[item_id].name, 0.05f);
        item_description.GetComponent<Text_animation>().startAnim("<i>" + gameManager.items[item_id].description, 1f);
        item_rarity_and_type.GetComponent<Text_animation>().startAnim("[" + gameManager.items[item_id].rarity + " " + gameManager.items[item_id].type + "]", 1f);

        item_icon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(gameManager.items[item_id].icon);

        level.GetComponent<Text_animation>().startAnim("<b>level " + gameManager.items[item_id].level, 1f);
        attr_health.GetComponent<Text_animation>().startAnim(gameManager.items[item_id].attributes[0] + " health", 0.05f);
        attr_resource.GetComponent<Text_animation>().startAnim(gameManager.items[item_id].attributes[1] + " energy", 0.05f);
        attr_damage.GetComponent<Text_animation>().startAnim(gameManager.items[item_id].attributes[2] + " damage", 0.05f);

        attr_value.GetComponent<Text_animation>().startAnim(gameManager.items[item_id].attributes[3] + " credit", 0.05f);


        //Attributes
        attr_health.GetComponent<TextMeshPro>().color = colors.white;
        attr_resource.GetComponent<TextMeshPro>().color = colors.white;
        attr_damage.GetComponent<TextMeshPro>().color = colors.white;

        //Quality
        if (gameManager.items[item_id].rarity == rarity.quest)
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = colors.quest;
            item_description.GetComponent<TextMeshPro>().color = colors.gray;
            item_name.GetComponent<TextMeshPro>().color = colors.quest;
            slot_border.GetComponent<SpriteRenderer>().color = colors.quest;
        }

        if (gameManager.items[item_id].rarity == rarity.poor)
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = colors.gray;
            item_description.GetComponent<TextMeshPro>().color = colors.gray;
            item_name.GetComponent<TextMeshPro>().color = colors.gray;
            slot_border.GetComponent<SpriteRenderer>().color = colors.gray;

        }
        if (gameManager.items[item_id].rarity == rarity.common)
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = colors.white;
            item_description.GetComponent<TextMeshPro>().color = colors.white;
            item_name.GetComponent<TextMeshPro>().color = colors.white;
            slot_border.GetComponent<SpriteRenderer>().color = colors.white;
        }

        if (gameManager.items[item_id].rarity == rarity.uncommon)
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = colors.green;
            item_description.GetComponent<TextMeshPro>().color = colors.gray;
            item_name.GetComponent<TextMeshPro>().color = colors.green;
            slot_border.GetComponent<SpriteRenderer>().color = colors.green;
        }
        if (gameManager.items[item_id].rarity == rarity.rare)
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = colors.blue;
            item_description.GetComponent<TextMeshPro>().color = colors.gray;
            item_name.GetComponent<TextMeshPro>().color = colors.blue;
            slot_border.GetComponent<SpriteRenderer>().color = colors.blue;
        }

        if (gameManager.items[item_id].rarity == rarity.epic)
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = colors.purple;
            item_description.GetComponent<TextMeshPro>().color = colors.gray;
            item_name.GetComponent<TextMeshPro>().color = colors.purple;
            slot_border.GetComponent<SpriteRenderer>().color = colors.purple;
        }

        if (gameManager.items[item_id].rarity == rarity.legendary)
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = colors.yellow;
            item_description.GetComponent<TextMeshPro>().color = colors.gray;
            item_name.GetComponent<TextMeshPro>().color = colors.yellow;
            slot_border.GetComponent<SpriteRenderer>().color = colors.yellow;
        }


        GameObject.Find("Equip").GetComponent<Text_animation>().startAnim("<b>Unequip", 0.05f);
        GameObject.Find("Equip_button").GetComponent<Item_equip_script>().mode = "unequip";
        GameObject.Find("Delete_button").GetComponent<Item_delete_script>().mode = "unequip";

    }
    public void showItem(int item_id, bool isStore)
    {
        gameObject.GetComponent<Animator>().Play("Item_preview_slide_in_anim", -1, 0f);
        gameObject.GetComponent<Animator>().Play("Item_preview_slide_in_anim");

        Colors colors = new Colors();

        var gameManager = GameObject.Find("Game manager").GetComponent<Item_script>();
        item_name.GetComponent<Text_animation>().startAnim(gameManager.items[item_id].name, 0.05f);
        item_description.GetComponent<Text_animation>().startAnim("<i>" + gameManager.items[item_id].description, 1f);

        attr_value.GetComponent<Text_animation>().startAnim(gameManager.items[item_id].attributes[3] + " credit", 0.05f);


        if (gameManager.items[item_id].type != item_type.quest)
        {
            item_rarity_and_type.GetComponent<Text_animation>().startAnim("[" + gameManager.items[item_id].rarity + " " + gameManager.items[item_id].type + "]", 1f);
        }
        else if (gameManager.items[item_id].type == item_type.quest)
        {
            item_rarity_and_type.GetComponent<Text_animation>().startAnim("[" + gameManager.items[item_id].type + "]", 1f);
        }

        item_icon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(gameManager.items[item_id].icon);

        if (gameManager.items[item_id].level > GameObject.Find("Game manager").GetComponent<Character_stats>().Player_level)
        {
            level.GetComponent<TextMeshPro>().color = colors.red;
        }
        else { level.GetComponent<TextMeshPro>().color = colors.white; }
        level.GetComponent<Text_animation>().startAnim("requires <b>level " + gameManager.items[item_id].level, 1f);

        if (!isStore)
        {
            GameObject.Find("Equip").GetComponent<Text_animation>().startAnim("<b>Equip", 0.05f);
            GameObject.Find("Delete_button").GetComponent<Text_animation>().startAnim("Sell", 0.05f);
            GameObject.Find("Equip_button").GetComponent<Item_equip_script>().mode = "equip";
            GameObject.Find("Delete_button").GetComponent<Item_delete_script>().mode = "equip";
        }
        else
        {
            GameObject.Find("Equip").GetComponent<Text_animation>().startAnim("<b>Buy", 0.05f);
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
            case item_type.head:
                health_to_compare = gameManager.items[character_stats.Equipments[0]].attributes[0];
                resource_to_compare = gameManager.items[character_stats.Equipments[0]].attributes[1];
                damage_to_compare = gameManager.items[character_stats.Equipments[0]].attributes[2];

                slot_id = 0;
                break;
            case item_type.body:
                health_to_compare = gameManager.items[character_stats.Equipments[1]].attributes[0];
                resource_to_compare = gameManager.items[character_stats.Equipments[1]].attributes[1];
                damage_to_compare = gameManager.items[character_stats.Equipments[1]].attributes[2];
                slot_id = 1;
                break;
            case item_type.legs:
                health_to_compare = gameManager.items[character_stats.Equipments[2]].attributes[0];
                resource_to_compare = gameManager.items[character_stats.Equipments[2]].attributes[1];
                damage_to_compare = gameManager.items[character_stats.Equipments[2]].attributes[2];
                slot_id = 2;
                break;
            case item_type.left_arm:
                health_to_compare = gameManager.items[character_stats.Equipments[3]].attributes[0];
                resource_to_compare = gameManager.items[character_stats.Equipments[3]].attributes[1];
                damage_to_compare = gameManager.items[character_stats.Equipments[3]].attributes[2];
                slot_id = 3;
                break;
            case item_type.shoulder:
                health_to_compare = gameManager.items[character_stats.Equipments[4]].attributes[0];
                resource_to_compare = gameManager.items[character_stats.Equipments[4]].attributes[1];
                damage_to_compare = gameManager.items[character_stats.Equipments[4]].attributes[2];
                slot_id = 4;
                break;
            case item_type.gadget:
                health_to_compare = gameManager.items[character_stats.Equipments[5]].attributes[0];
                resource_to_compare = gameManager.items[character_stats.Equipments[5]].attributes[1];
                damage_to_compare = gameManager.items[character_stats.Equipments[5]].attributes[2];
                slot_id = 5;
                break;
            case item_type.feet:
                health_to_compare = gameManager.items[character_stats.Equipments[6]].attributes[0];
                resource_to_compare = gameManager.items[character_stats.Equipments[6]].attributes[1];
                damage_to_compare = gameManager.items[character_stats.Equipments[6]].attributes[2];
                slot_id = 6;
                break;
            case item_type.right_arm:
                health_to_compare = gameManager.items[character_stats.Equipments[7]].attributes[0];
                resource_to_compare = gameManager.items[character_stats.Equipments[7]].attributes[1];
                damage_to_compare = gameManager.items[character_stats.Equipments[7]].attributes[2];
                slot_id = 7;
                break;
            default:
                break;
        }
        #endregion

        if (gameManager.items[item_id].type != item_type.quest)
        {
            attr_health.GetComponent<Text_animation>().startAnim("+" + gameManager.items[item_id].attributes[0] +
            " health (" + (gameManager.items[character_stats.Equipments[slot_id]].attributes[0]) + " ¤ " + (gameManager.items[item_id].attributes[0]) + ")", 0.05f);
            attr_resource.GetComponent<Text_animation>().startAnim("+" + gameManager.items[item_id].attributes[1] +
            " energy (" + (gameManager.items[character_stats.Equipments[slot_id]].attributes[1]) + " ¤ " + (gameManager.items[item_id].attributes[1]) + ")", 0.05f);
            attr_damage.GetComponent<Text_animation>().startAnim("+" + gameManager.items[item_id].attributes[2] +
            " damage (" + (gameManager.items[character_stats.Equipments[slot_id]].attributes[2]) + " ¤ " + (gameManager.items[item_id].attributes[2]) + ")", 0.05f);

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
        { attr_health.GetComponent<TextMeshPro>().color = colors.green; }
        else if (health_to_compare > gameManager.items[item_id].attributes[0])
        { attr_health.GetComponent<TextMeshPro>().color = colors.red; }
        else if (health_to_compare == gameManager.items[item_id].attributes[0])
        { attr_health.GetComponent<TextMeshPro>().color = colors.white; }

        if (resource_to_compare < gameManager.items[item_id].attributes[1])
        { attr_resource.GetComponent<TextMeshPro>().color = colors.green; }
        else if (resource_to_compare > gameManager.items[item_id].attributes[1])
        { attr_resource.GetComponent<TextMeshPro>().color = colors.red; }
        else if (resource_to_compare == gameManager.items[item_id].attributes[1])
        { attr_resource.GetComponent<TextMeshPro>().color = colors.white; }

        if (damage_to_compare < gameManager.items[item_id].attributes[2])
        { attr_damage.GetComponent<TextMeshPro>().color = colors.green; }
        else if (damage_to_compare > gameManager.items[item_id].attributes[2])
        { attr_damage.GetComponent<TextMeshPro>().color = colors.red; }
        else if (damage_to_compare == gameManager.items[item_id].attributes[2])
        { attr_damage.GetComponent<TextMeshPro>().color = colors.white; }


        //Quality

        if (gameManager.items[item_id].rarity == rarity.quest)
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = colors.quest;
            item_description.GetComponent<TextMeshPro>().color = colors.quest;
            item_name.GetComponent<TextMeshPro>().color = colors.quest;
            slot_border.GetComponent<SpriteRenderer>().color = colors.quest;
        }

        if (gameManager.items[item_id].rarity == rarity.poor)
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = colors.gray;
            item_description.GetComponent<TextMeshPro>().color = colors.gray;
            item_name.GetComponent<TextMeshPro>().color = colors.gray;
            slot_border.GetComponent<SpriteRenderer>().color = colors.gray;

        }
        if (gameManager.items[item_id].rarity == rarity.common)
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = colors.white;
            item_description.GetComponent<TextMeshPro>().color = colors.gray;
            item_name.GetComponent<TextMeshPro>().color = colors.white;
            slot_border.GetComponent<SpriteRenderer>().color = colors.white;
        }

        if (gameManager.items[item_id].rarity == rarity.uncommon)
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = colors.green;
            item_description.GetComponent<TextMeshPro>().color = colors.gray;
            item_name.GetComponent<TextMeshPro>().color = colors.green;
            slot_border.GetComponent<SpriteRenderer>().color = colors.green;
        }
        if (gameManager.items[item_id].rarity == rarity.rare)
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = colors.blue;
            item_description.GetComponent<TextMeshPro>().color = colors.gray;
            item_name.GetComponent<TextMeshPro>().color = colors.blue;
            slot_border.GetComponent<SpriteRenderer>().color = colors.blue;
        }

        if (gameManager.items[item_id].rarity == rarity.epic)
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = colors.purple;
            item_description.GetComponent<TextMeshPro>().color = colors.gray;
            item_name.GetComponent<TextMeshPro>().color = colors.purple;
            slot_border.GetComponent<SpriteRenderer>().color = colors.purple;
        }

        if (gameManager.items[item_id].rarity == rarity.legendary)
        {
            item_rarity_and_type.GetComponent<TextMeshPro>().color = colors.yellow;
            item_description.GetComponent<TextMeshPro>().color = colors.gray;
            item_name.GetComponent<TextMeshPro>().color = colors.yellow;
            slot_border.GetComponent<SpriteRenderer>().color = colors.yellow;
        }
    }
}
