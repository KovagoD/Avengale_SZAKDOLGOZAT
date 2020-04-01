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

    public GameObject countdownText;

    void Start()
    {
        _notification = GameObject.Find("Notification").GetComponent<Ingame_notification_script>();
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
            int random = UnityEngine.Random.Range(1, GameObject.Find("Game manager").GetComponent<Item_script>().items.Count);
            Store[i] = random;
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
