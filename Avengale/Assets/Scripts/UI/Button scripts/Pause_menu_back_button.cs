using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_menu_back_button : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var _gameManager = GameObject.Find("Game manager").GetComponent<Game_manager>();
            if (_gameManager.current_screen == _gameManager.Combat_screen)
            {
                GameObject.Find("Authorization").GetComponent<Authorization_script>().ShowAuthorization("surrenderBattle", 0);
            }
            else
            {
                _gameManager.Change_screen(_gameManager.Main_screen, false);
            }
        }
    }
}
