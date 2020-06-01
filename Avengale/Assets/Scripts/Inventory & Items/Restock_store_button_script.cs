using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restock_store_button_script : MonoBehaviour
{
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.Find("Game manager").GetComponent<Store_manager>().restockItems();
        }
    }
}
