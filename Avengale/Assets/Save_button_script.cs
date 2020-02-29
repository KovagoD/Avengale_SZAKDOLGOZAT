using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_button_script : MonoBehaviour
{
  void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0) && !GameObject.Find("Item_preview").GetComponent<Visibility_script>().isOpened)
        {
            GameObject.Find("Game manager").GetComponent<Character_stats>().savePlayer();
            GameObject.Find("Game manager").GetComponent<Item_script>().saveItems();

        }

    }
}
