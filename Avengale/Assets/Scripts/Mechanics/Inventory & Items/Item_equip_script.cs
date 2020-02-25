﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_equip_script : MonoBehaviour
{
    public int slot_id;
    public string mode;

    void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0) && GameObject.Find("Item_preview").GetComponent<Visibility_script>().isOpened)
        {
            if (mode == "equip")
            {
                var gameManager = GameObject.Find("Game manager").GetComponent<Item_script>();
                var item = gameManager.items[gameManager.GetComponent<Character_stats>().Inventory[slot_id]];

                switch (item.type)
                {
                    case "head":
                        gameManager.GetComponent<Character_stats>().equipItem(0, item.id, slot_id);
                        break;
                    case "body":
                        gameManager.GetComponent<Character_stats>().equipItem(1, item.id, slot_id);
                        break;
                    case "legs":
                        gameManager.GetComponent<Character_stats>().equipItem(2, item.id, slot_id);
                        break;
                    case "left arm":
                        gameManager.GetComponent<Character_stats>().equipItem(3, item.id, slot_id);
                        break;
                    case "shoulder":
                        gameManager.GetComponent<Character_stats>().equipItem(4, item.id, slot_id);
                        break;
                    case "gadget":
                        gameManager.GetComponent<Character_stats>().equipItem(5, item.id, slot_id);
                        break;
                    case "feet":
                        gameManager.GetComponent<Character_stats>().equipItem(6, item.id, slot_id);
                        break;
                    case "right arm":
                        gameManager.GetComponent<Character_stats>().equipItem(7, item.id, slot_id);
                        break;
                    default:
                        break;
                }

                var exit_btn = GameObject.Find("Exit button item_preview");
                exit_btn.GetComponent<Close_button_script>().otherFunctions();


            }
            else if (mode == "unequip")
            {
                var gameManager = GameObject.Find("Game manager").GetComponent<Item_script>();
                var item = gameManager.items[gameManager.GetComponent<Character_stats>().Equipments[slot_id]];

                switch (item.type)
                {
                    case "head":
                        gameManager.GetComponent<Character_stats>().unequipItem(0);
                        break;
                    case "body":
                        gameManager.GetComponent<Character_stats>().unequipItem(1);
                        break;
                    case "legs":
                        gameManager.GetComponent<Character_stats>().unequipItem(2);
                        break;
                    case "left arm":
                        gameManager.GetComponent<Character_stats>().unequipItem(3);
                        break;
                    case "shoulder":
                        gameManager.GetComponent<Character_stats>().unequipItem(4);
                        break;
                    case "gadget":
                        gameManager.GetComponent<Character_stats>().unequipItem(5);
                        break;
                    case "feet":
                        gameManager.GetComponent<Character_stats>().unequipItem(6);
                        break;
                    case "right arm":
                        gameManager.GetComponent<Character_stats>().unequipItem(7);
                        break;
                    default:
                        break;
                }

                var exit_btn = GameObject.Find("Exit button item_preview");
                exit_btn.GetComponent<Close_button_script>().otherFunctions();
            }
            else if (mode == "buy")
            {
                var gameManager = GameObject.Find("Game manager");
                gameManager.GetComponent<Character_stats>().buyItem(slot_id);

                var exit_btn = GameObject.Find("Exit button item_preview");
                exit_btn.GetComponent<Close_button_script>().otherFunctions();
            }
        }
    }
}
