using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory_open_button : MonoBehaviour
{
    public GameObject inventory;
    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            inventory.GetComponent<Inventory_ui_script>().showInventory();
        }
    }
}
