using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_pick_up_script : MonoBehaviour
{
    public int item_id;

    void OnMouseDown()
    {
        item_id = UnityEngine.Random.Range(1, 9);
        GameObject.Find("Game manager").GetComponent<Character_stats>().itemPickup(item_id, true);
    }

}
