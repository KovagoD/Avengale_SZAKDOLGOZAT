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
    public GameObject item_slot;

    [Header("Slot")]
    public bool isOpened = true;
    public GameObject slot;
    public Sprite slot_sprite;
    public Sprite slot_sprite_activated;
    public GameObject slot_border;

    private void Start()
    {

        gameManager = GameObject.Find("Game manager");


        item_id = gameManager.GetComponent<Store_manager>().Store[ID];
        item = gameManager.GetComponent<Item_script>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Game manager").GetComponent<Game_manager>().current_screen.name == "Store_screen_UI")
        {
            item_id = gameManager.GetComponent<Store_manager>().Store[ID];

            if (gameManager.GetComponent<Store_manager>().Store[ID] == 0)
            {
                item_slot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Empty");
                slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().transparent;
            }

            if (gameManager.GetComponent<Store_manager>().Store[ID] != 0 && gameManager.GetComponent<Store_manager>().Store[ID] == item_id)
            {
                item_slot.GetComponent<SpriteRenderer>().sprite = item.items[item_id].icon;
            }

            var items = gameManager.GetComponent<Item_script>().items;

            if (items[item_id].rarity == "poor")
            {
                slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().gray;
            }
            if (items[item_id].rarity == "common")
            {
                slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().white;
            }
            if (items[item_id].rarity == "uncommon")
            {
                slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().green;
            }
            if (items[item_id].rarity == "rare")
            {
                slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().blue;
            }
            if (items[item_id].rarity == "epic")
            {
                slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().purple;
            }
            if (items[item_id].rarity == "legendary")
            {
                slot_border.GetComponent<SpriteRenderer>().color = gameManager.GetComponent<Game_manager>().yellow;
            }
        }

    }

    void OnMouseDown()
    {
        if (GameObject.Find("Game manager").GetComponent<Game_manager>().current_screen.name == "Store_screen_UI")
        {

            slot.GetComponent<SpriteRenderer>().sprite = slot_sprite_activated;
            var item = GameObject.Find("Game manager").GetComponent<Item_script>().items;
            //Debug.Log(item_id+": " + item[item_id].name);



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
