using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_delete_script : MonoBehaviour
{
    public int slot_id;
    public string mode;

    void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0) && mode != "buy" && GameObject.Find("Item_preview").GetComponent<Visibility_script>().isOpened)
        {
            //GameObject.Find("Game manager").GetComponent<Character_stats>().deleteItem(mode, slot_id);
            GameObject.Find("Authorization").GetComponent<Authorization_script>().ShowAuthorization("deleteItem", slot_id);
        }
    }
}