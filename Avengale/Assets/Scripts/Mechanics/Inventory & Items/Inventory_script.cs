using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory_script : MonoBehaviour
{
    public int ID = 0;

    [Header("Item")]
    public int item_id = 0;
    public GameObject item_slot;

    [Header("Slot")]
    public bool isOpened = false;
    public GameObject slot;
    public Sprite slot_sprite;
    public Sprite slot_sprite_activated;
    public GameObject slot_border;
    public GameObject item_availability;

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

        if (_characterStats.Inventory[ID] == 0)
        {
            item_slot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Empty");
            _slotBorder.color = _gameManagerScript.transparent;
        }

        if (_characterStats.Inventory[ID] != 0 && _characterStats.Inventory[ID] == item_id)
        {
            item_slot.GetComponent<SpriteRenderer>().sprite = _itemScript.items[item_id].icon;


           switch (_itemScript.items[item_id].rarity)
            {
                case "poor":
                    _slotBorder.color = _gameManagerScript.gray;
                    break;
                case "common":
                    _slotBorder.color = _gameManagerScript.white;
                    break;
                case "uncommon":
                   _slotBorder.color = _gameManagerScript.green;
                    break;
                case "rare":
                    _slotBorder.color = _gameManagerScript.blue;
                    break;
                case "epic":
                    _slotBorder.color = _gameManagerScript.purple;
                    break;
                case "legendary":
                   _slotBorder.color = _gameManagerScript.yellow;
                    break;
                case "quest":
                   _slotBorder.color = _gameManagerScript.quest;
                    break;
                default:
                    break;
            }


        }

        if (_itemScript.items[item_id].level > _characterStats.Local_level && gameObject.GetComponent<Visibility_script>().isOpened)
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