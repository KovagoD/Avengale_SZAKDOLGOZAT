using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_character_button_script : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var _gameManager = GameObject.Find("Game manager").GetComponent<Game_manager>();
            _gameManager.isNewCharacter = true;
            _gameManager.Change_screen(_gameManager.Character_customization_screen, true);
        }
    }
}
