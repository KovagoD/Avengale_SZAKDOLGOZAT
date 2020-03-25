using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_slot_script : MonoBehaviour
{
    public int ID = 0;

    [Header("References")]
    private GameObject gameManager;
    private Item_script item;

    [Header("Item")]
    public int item_id = 0;
    [Header("Slot")]
    public bool isOpened = true;
    public GameObject item_slot, slot, slot_border;
    public Sprite slot_sprite, slot_sprite_activated;
    public Colors colors;

    private void Start()
    {
        gameManager = GameObject.Find("Game manager");
        item_id = gameManager.GetComponent<Store_manager>().Store[ID];
        item = gameManager.GetComponent<Item_script>();

    }
    void Update()
    {
        if (GameObject.Find("Game manager").GetComponent<Game_manager>().current_screen.name == "Store_screen_UI")
        {
            item_id = gameManager.GetComponent<Store_manager>().Store[ID];

            if (gameManager.GetComponent<Store_manager>().Store[ID] == 0)
            {
                item_slot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Empty");
                slot_border.GetComponent<SpriteRenderer>().color = colors.transparent;
            }

            if (gameManager.GetComponent<Store_manager>().Store[ID] != 0 && gameManager.GetComponent<Store_manager>().Store[ID] == item_id)
            {
                item_slot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(item.items[item_id].icon);
            }

            var items = gameManager.GetComponent<Item_script>().items;

            if (items[item_id].rarity == rarity.quest)
            {
                slot_border.GetComponent<SpriteRenderer>().color = colors.quest;
            }
            if (items[item_id].rarity == rarity.poor)
            {
                slot_border.GetComponent<SpriteRenderer>().color = colors.gray;
            }
            if (items[item_id].rarity == rarity.common)
            {
                slot_border.GetComponent<SpriteRenderer>().color = colors.white;
            }
            if (items[item_id].rarity == rarity.uncommon)
            {
                slot_border.GetComponent<SpriteRenderer>().color = colors.green;
            }
            if (items[item_id].rarity == rarity.rare)
            {
                slot_border.GetComponent<SpriteRenderer>().color = colors.blue;
            }
            if (items[item_id].rarity == rarity.epic)
            {
                slot_border.GetComponent<SpriteRenderer>().color = colors.purple;
            }
            if (items[item_id].rarity == rarity.legendary)
            {
                slot_border.GetComponent<SpriteRenderer>().color = colors.yellow;
            }
        }

    }

    void OnMouseDown()
    {
        if (GameObject.Find("Game manager").GetComponent<Game_manager>().current_screen.name == "Store_screen_UI")
        {

            slot.GetComponent<SpriteRenderer>().sprite = slot_sprite_activated;
            var item = GameObject.Find("Game manager").GetComponent<Item_script>().items;
            if (Input.GetMouseButtonDown(0) && item_id != 0 && !GameObject.Find("Item_preview").GetComponent<Visibility_script>().isOpened)
            {

                var item_equip_button = GameObject.Find("Equip_button").GetComponent<Item_equip_script>().slot_id = ID;
                var item_delete_button = GameObject.Find("Delete_button").GetComponent<Item_delete_script>().slot_id = ID;

                var item_preview = GameObject.Find("Item_preview");
                item_preview.GetComponent<Open_button_script>().Open();
                item_preview.GetComponent<Item_preview>().showItem(item_id, true);


            }
            else if (Input.GetMouseButtonDown(0) && item_id == 0)
            {
                var exit_btn = GameObject.Find("Exit button item_preview");
                exit_btn.GetComponent<Close_button_script>().Close();
            }
        }
    }

    void OnMouseExit()
    {
        slot.GetComponent<SpriteRenderer>().sprite = slot_sprite;
    }

}
