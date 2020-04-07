using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_pick_up_script : MonoBehaviour
{
    void OnMouseDown()
    {
        GameObject.Find("Game manager").GetComponent<Character_stats>().randomItemPickup();
    }

}
