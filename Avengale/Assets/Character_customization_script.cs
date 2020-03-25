﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum body_parts { hair, eyes, nose, mouth, body }
public class Character_customization_script : MonoBehaviour
{
    public bool isNewCharacter;
    public TMP_InputField character_name;
    public GameObject character;
    public int hair_id, eyes_id, nose_id, mouth_id, body_id;

    public int equipment_head_id, equipment_body_id, equipment_legs_id, equipment_left_id, equipment_shoulder_id, equipment_gadget_id, equipment_feet_id, equipment_right_id;

    private int hair_length = 0, eyes_length = 1, nose_length = 1, mouth_length = 1, body_length = 3;
    private Ingame_notification_script _notification;


    public void initializeCustomization(bool isNewCharacter)
    {
        this.isNewCharacter = isNewCharacter;
        if (isNewCharacter)
        {
            randomizeLook();
            character_name.text = null;
        }
        else
        {
            loadCurrentAppearance();
        }
    }
    public void checkConfirm()
    {
        _notification = GameObject.Find("Notification").GetComponent<Ingame_notification_script>();

        if (character_name.text.Length >= 1)
        {

            if (isNewCharacter)
            {
                GameObject.Find("Authorization").GetComponent<Authorization_script>().ShowAuthorization("confirmCustomization", 0);
            }
            else
            {
                GameObject.Find("Authorization").GetComponent<Authorization_script>().ShowAuthorization("confirmReCustomization", 0);
            }
        }
        else
        {
            _notification.message("You must choose a name!", 3, "red");
        }
    }
    public void confirmCustomization()
    {
        GameObject _gameManager = GameObject.Find("Game manager");
        Character_stats _character = _gameManager.GetComponent<Character_stats>();

        if (isNewCharacter)
        {
            _character.initializePlayer();
        }

        _character.Local_name = character_name.text;

        _character.hair_id = hair_id;
        _character.eyes_id = eyes_id;
        _character.nose_id = nose_id;
        _character.mouth_id = mouth_id;
        _character.body_id = body_id;

        GameObject.Find("Conversation").GetComponent<Conversation_script>().initializeConversations();
        _gameManager.GetComponent<Game_manager>().Change_screen(_gameManager.GetComponent<Game_manager>().Character_screen_UI, true);
    }

    public void loadCurrentAppearance()
    {
        if (!isNewCharacter)
        {
            GameObject _gameManager = GameObject.Find("Game manager");
            Character_stats _localCharacter = _gameManager.GetComponent<Character_stats>();

            hair_id = _localCharacter.hair_id;
            eyes_id = _localCharacter.eyes_id;
            nose_id = _localCharacter.nose_id;
            mouth_id = _localCharacter.nose_id;
            body_id = _localCharacter.body_id;

            equipment_head_id = _localCharacter.Equipments[0];
            equipment_body_id = _localCharacter.Equipments[1];
            equipment_legs_id = _localCharacter.Equipments[2];
            equipment_left_id = _localCharacter.Equipments[3];
            equipment_shoulder_id = _localCharacter.Equipments[4];
            equipment_gadget_id = _localCharacter.Equipments[5];
            equipment_feet_id = _localCharacter.Equipments[6];
            equipment_right_id = _localCharacter.Equipments[7];



            character_name.text = _localCharacter.Local_name;

            updateLook();
        }
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

        _character.equipment_head_id = equipment_head_id;
        _character.equipment_body_id = equipment_body_id;
        _character.equipment_legs_id = equipment_legs_id;
        _character.equipment_left_id = equipment_left_id;
        _character.equipment_shoulder_id = equipment_shoulder_id;
        _character.equipment_gadget_id = equipment_gadget_id;
        _character.equipment_feet_id = equipment_feet_id;
        _character.equipment_right_id = equipment_right_id;
    }
}