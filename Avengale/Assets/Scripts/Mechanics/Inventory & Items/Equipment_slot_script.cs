using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Equipment_slot_script : MonoBehaviour
{
    public int ID = 0, item_id = 0;
    public bool isOpened = true;
    public GameObject item_slot, slot_text, slot, slot_border;
    public Sprite slot_sprite, slot_sprite_activated;
    private Item_script _itemScript;
    private Game_manager _gameManagerScript;
    private Character_stats _characterStats;
    void Start()
    {
        _itemScript = GameObject.Find("Game manager").GetComponent<Item_script>();
        _gameManagerScript = GameObject.Find("Game manager").GetComponent<Game_manager>();
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();

        var tmp = slot_text.GetComponent<TextMeshPro>();

        switch (ID)
        {
            case 0:
                tmp.SetText("Head");
                break;
            case 1:
                tmp.SetText("Body");
                break;
            case 2:
                tmp.SetText("Legs");
                break;
            case 3:
                tmp.SetText("Left");
                break;
            case 4:
                tmp.SetText("Shoulder");
                break;
            case 5:
                tmp.SetText("Gadget");
                break;
            case 6:
                tmp.SetText("Feet");
                break;
            case 7:
                tmp.SetText("Right");
                break;
            default:
                break;
        }
        slot_text.GetComponent<Text_animation>().startAnim(tmp.text, 0.01f);
    }

    void Update()
    {
        item_id = _characterStats.Equipments[ID];
        SpriteRenderer _slotBorder = slot_border.GetComponent<SpriteRenderer>();
        Colors colors = new Colors();

        if (_characterStats.Equipments[ID] != 0 && _characterStats.Equipments[ID] == item_id)
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
                default:
                    break;
            }

        }

        if (_characterStats.Equipments[ID] == 0)
        {
            item_slot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Empty");
            _slotBorder.color = colors.transparent;
        }

    }

    void OnMouseDown()
    {
        Debug.Log("clicked +"+ID);
        slot.GetComponent<SpriteRenderer>().sprite = slot_sprite_activated;

        if (Input.GetMouseButtonDown(0) && item_id != 0 && !GameObject.Find("Item_preview").GetComponent<Visibility_script>().isOpened && !GameObject.Find("Conversation").GetComponent<Visibility_script>().isOpened)
        {

            var item_equip_button = GameObject.Find("Equip_button").GetComponent<Item_equip_script>().slot_id = ID;
            var item_delete_button = GameObject.Find("Delete_button").GetComponent<Item_delete_script>().slot_id = ID;

            var item_preview = GameObject.Find("Item_preview");
            item_preview.GetComponent<Open_button_script>().Open();
            item_preview.GetComponent<Item_preview>().showEquippedItem(item_id);
        }
        else if (Input.GetMouseButtonDown(0) && item_id == 0 && !GameObject.Find("Item_preview").GetComponent<Visibility_script>().isOpened && !GameObject.Find("Conversation").GetComponent<Visibility_script>().isOpened)
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
