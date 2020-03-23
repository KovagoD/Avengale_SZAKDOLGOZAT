using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum body_parts { hair, eyes, nose, mouth, body }
public class Character_customization_script : MonoBehaviour
{
    public TMP_InputField character_name;
    public GameObject character;
    public int hair_id, eyes_id, nose_id, mouth_id, body_id;

    private int hair_length = 0, eyes_length = 1, nose_length = 1, mouth_length = 1, body_length = 3;
    private Ingame_notification_script _notification;


    public void checkConfirm()
    {
        _notification = GameObject.Find("Notification").GetComponent<Ingame_notification_script>();

        if (character_name.text.Length >= 1)
        {
            //confirmCustomization();
            GameObject.Find("Authorization").GetComponent<Authorization_script>().ShowAuthorization("confirmCustomization",0);
        }
        else
        {
            _notification.message("Your name cannot be nothing!", 3, "red");
        }
    }
    public void confirmCustomization()
    {
        GameObject _gameManager = GameObject.Find("Game manager");
        Character_stats _character = _gameManager.GetComponent<Character_stats>();

        _character.Local_name = character_name.text;

        _character.hair_id = hair_id;
        _character.eyes_id = eyes_id;
        _character.nose_id = nose_id;
        _character.mouth_id = mouth_id;
        _character.body_id = body_id;

        _gameManager.GetComponent<Game_manager>().Change_screen(_gameManager.GetComponent<Game_manager>().Character_screen_UI, true);
    }
    public void randomizeLook()
    {
        hair_id = Random.Range(0, hair_length + 1);
        eyes_id = Random.Range(0, eyes_length + 1);
        nose_id = Random.Range(0, nose_length + 1);
        mouth_id = Random.Range(0, mouth_length + 1);
        body_id = Random.Range(0, body_length + 1);

        updateLook();
    }
    public void next(body_parts part)
    {
        switch (part)
        {
            case body_parts.hair:
                if (hair_id <= hair_length)
                {
                    hair_id++;
                }
                break;

            case body_parts.eyes:
                if (eyes_id <= eyes_length)
                {
                    eyes_id++;
                }
                break;

            case body_parts.nose:
                if (nose_id <= nose_length)
                {
                    nose_id++;
                }
                break;

            case body_parts.mouth:
                if (mouth_id <= mouth_length)
                {
                    mouth_id++;
                }
                break;

            case body_parts.body:
                if (body_id <= body_length)
                {
                    body_id++;
                }
                break;
        }

        updateLook();


    }

    public void previous(body_parts part)
    {
        switch (part)
        {
            case body_parts.hair:
                if (hair_id > 0)
                {
                    hair_id--;
                }
                break;

            case body_parts.eyes:
                if (eyes_id > 0)
                {
                    eyes_id--;
                }
                break;

            case body_parts.nose:
                if (nose_id > 0)
                {
                    nose_id--;
                }
                break;

            case body_parts.mouth:
                if (mouth_id > 0)
                {
                    mouth_id--;
                }
                break;

            case body_parts.body:
                if (body_id > 0)
                {
                    body_id--;
                }
                break;
        }
        updateLook();
    }

    public void updateLook()
    {
        var _character = character.GetComponent<Character_manager>();

        _character.hair_id = hair_id;
        _character.eyes_id = eyes_id;
        _character.nose_id = nose_id;
        _character.mouth_id = mouth_id;
        _character.body_id = body_id;
    }
}
