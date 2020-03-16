using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_button_script : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0) && !GameObject.Find("Item_preview").GetComponent<Visibility_script>().isOpened)
        {
            var _gameManager =  GameObject.Find("Game manager");
            _gameManager.GetComponent<Character_stats>().loadPlayer();
            _gameManager.GetComponent<Item_script>().loadItems();
            _gameManager.GetComponent<Spell_script>().loadSpells();

        }

    }
}
