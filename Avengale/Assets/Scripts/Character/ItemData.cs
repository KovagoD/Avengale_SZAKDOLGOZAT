using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{

    public List<Item> items = new List<Item>();
    public ItemData(Item_script itemScript)
    {
        items = itemScript.items;
    }
}
