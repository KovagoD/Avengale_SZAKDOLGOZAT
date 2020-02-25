using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_manager : MonoBehaviour
{
    public int[] Store = new int[10];
    public int storeRestockTime;
    private Ingame_notification_script _notification;

    void Start()
    {
        _notification = GameObject.Find("Notification").GetComponent<Ingame_notification_script>();
        restockItems();
        StartCoroutine(Restock());
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
        for (int i = 0; i < Store.Length; i++)
        {
            int random = Random.Range(1, GameObject.Find("Game manager").GetComponent<Item_script>().items.Count);
            Store[i] = random;
        }
    }

    void Update()
    {
        if (Input.GetKey("up"))
        {
            restockItems();
        }
    }
}
