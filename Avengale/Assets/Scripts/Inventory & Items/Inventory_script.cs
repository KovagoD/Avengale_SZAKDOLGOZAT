using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory_script : MonoBehaviour
{
    public int ID = 0, item_id = 0;

    public GameObject item_slot, slot, slot_border, item_availability;
    public bool isOpened = false;
    public Sprite slot_sprite, slot_sprite_activated;


    private Item_script _itemScript;
    private Game_manager _gameManagerScript;
    private Character_stats _characterStats;
    private void Start()
    {
        _itemScript = GameObject.Find("Game manager").GetComponent<Item_script>();
        _gameManagerScript = GameObject.Find("Game manager").GetComponent<Game_manager>();
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
    }

    void Update()
    {
        item_id = _characterStats.Inventory[ID];

        SpriteRenderer _slotBorder = slot_border.GetComponent<SpriteRenderer>();

        Colors colors = new Colors();

        if (_characterStats.Inventory[ID] == 0)
        {
            item_slot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Empty");
            _slotBorder.color = colors.transparent;
        }

        if (_characterStats.Inventory[ID] != 0 && _characterStats.Inventory[ID] == item_id)
        {
            item_slot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(_itemScript.items[item_id].icon);



            switch (_itemScript.items[item_id].rarity)
            {
                case rarity.poor:
                    _slotBorder.color = colors.gray;
                    break;
                case rarity.common:
                    _slotBorder.color = colors.white;
                    break;
                case rarity.uncommon:
                    _slotBorder.color = colors.green;
                    break;
                case rarity.rare:
                    _slotBorder.color = colors.blue;
                    break;
                case rarity.epic:
                    _slotBorder.color = colors.purple;
                    break;
                case rarity.legendary:
                    _slotBorder.color = colors.yellow;
                    break;
                case rarity.quest:
                    _slotBorder.color = colors.quest;
                    break;
                default:
                    break;
            }


        }

        if (_itemScript.items[item_id].level > _characterStats.Player_level && gameObject.GetComponent<Visibility_script>().isOpened)
        {
            item_availability.GetComponent<SpriteRenderer>().enabled = true;
        }
        else { item_availability.GetComponent<SpriteRenderer>().enabled = false; }

    }

    void OnMouseDown()
    {
        slot.GetComponent<SpriteRenderer>().sprite = slot_sprite_activated;
        //Debug.Log(item_id+": " + item[item_id].name);

        if (Input.GetMouseButtonDown(0) && item_id != 0)
        {

            var item_equip_button = GameObject.Find("Equip_button").GetComponent<Item_equip_script>().slot_id = ID;
            var item_delete_button = GameObject.Find("Delete_button").GetComponent<Item_delete_script>().slot_id = ID;

            var item_preview = GameObject.Find("Item_preview");
            item_preview.GetComponent<Open_button_script>().Open();
            item_preview.GetComponent<Item_preview>().showItem(item_id, false);


        }
        else if (Input.GetMouseButtonDown(0) && item_id == 0)
        {
            var exit_btn = GameObject.Find("Exit button item_preview");
            exit_btn.GetComponent<Close_button_script>().Close();
        }

    }

    void OnMouseExit()
    {
        slot.GetComponent<SpriteRenderer>().sprite = slot_sprite;
    }

}