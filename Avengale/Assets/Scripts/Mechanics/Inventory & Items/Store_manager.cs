using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Store_manager : MonoBehaviour
{
    public int[] Store = new int[10];

    public float storeRestockTime;
    public float currentTime;
    private Ingame_notification_script _notification;
    private Item_script _itemScript;

    public GameObject countdownText;

    void Start()
    {
        _notification = GameObject.Find("Notification").GetComponent<Ingame_notification_script>();
        _itemScript = GameObject.Find("Game manager").GetComponent<Item_script>();
        restockItems();
        //StartCoroutine(Restock());
    }

    IEnumerator Restock()
    {
        yield return new WaitForSeconds(storeRestockTime);
        restockItems();
        Debug.Log("The store has been restocked!");
        _notification.message("The store has been restocked!", 3);
        StartCoroutine(Restock());
    }


    public void restockItems()
    {
        currentTime = storeRestockTime;
        for (int i = 0; i < Store.Length; i++)
        {
            int random = UnityEngine.Random.Range(1, GameObject.Find("Game manager").GetComponent<Item_script>().declared_items.Count);

            if (random != 16)
            {
                var dec_items = _itemScript.declared_items[random];

                var tmp_item = new Item(_itemScript.items.Count, dec_items.name, dec_items.type, dec_items.rarity, dec_items.min_rarity,
                dec_items.max_rarity, dec_items.level, dec_items.description, dec_items.icon, dec_items.sprite_male, dec_items.sprite_female,
                dec_items.attributes[0], dec_items.attributes[1], dec_items.attributes[2], dec_items.attributes[3]);

                tmp_item.randomizeStats(10, 10, 10);

                _itemScript.items.Add(tmp_item);
                Store[i] = tmp_item.id;
            }
            else { Store[i] = random; }
        }
    }

    void Update()
    {
        if (Input.GetKey("up"))
        {
            restockItems();
        }
        currentTime -= Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(currentTime);

        if (currentTime < 0)
        {
            restockItems();
        }

        if (gameObject.GetComponent<Game_manager>().current_screen == gameObject.GetComponent<Game_manager>().Shop_screen)
        {
            countdownText.GetComponent<TextMeshPro>().SetText(time.ToString("hh':'mm':'ss"));
        }
    }
}
