﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_manager : MonoBehaviour
{
    public bool isPlayer = false;

    public GameObject equipment_head, equipment_body, equipment_legs, equipment_left, equipment_shoulder, equipment_gadget, equipment_feet, equipment_right;
    private SpriteRenderer _equipment_head, _equipment_body, _equipment_legs, _equipment_left, _equipment_shoulder, _equipment_gadget, _equipment_feet, _equipment_right;
    public int equipment_head_id, equipment_body_id, equipment_legs_id, equipment_left_id, equipment_shoulder_id, equipment_gadget_id, equipment_feet_id, equipment_right_id;

    public GameObject turn_sign;
    public Animator spell_animation;

    public GameObject damage_text;



    public bool sex, hideHelmet;
    public int hair_id, eyes_id, nose_id, mouth_id, body_id;

    [Range(0, 255)]
    public byte hair_color_r = 255;
    [Range(0, 255)]
    public byte hair_color_g = 0;
    [Range(0, 255)]
    public byte hair_color_b = 0;



    public GameObject hair, eyes, nose, mouth, body;
    private SpriteRenderer _hair, _eyes, _nose, _mouth, _body;

    private Item_script _itemScript;
    private Character_stats _characterStats;
    private Game_manager _gameManager;

    // Update is called once per frame
    void Start()
    {
        _hair = hair.GetComponent<SpriteRenderer>();
        _eyes = eyes.GetComponent<SpriteRenderer>();
        _nose = nose.GetComponent<SpriteRenderer>();
        _mouth = mouth.GetComponent<SpriteRenderer>();
        _body = body.GetComponent<SpriteRenderer>();

        _equipment_head = equipment_head.GetComponent<SpriteRenderer>();
        _equipment_body = equipment_body.GetComponent<SpriteRenderer>(); ;
        _equipment_legs = equipment_legs.GetComponent<SpriteRenderer>(); ;
        _equipment_left = equipment_left.GetComponent<SpriteRenderer>(); ;
        _equipment_shoulder = equipment_shoulder.GetComponent<SpriteRenderer>(); ;
        _equipment_gadget = equipment_gadget.GetComponent<SpriteRenderer>(); ;
        _equipment_feet = equipment_feet.GetComponent<SpriteRenderer>(); ;
        _equipment_right = equipment_right.GetComponent<SpriteRenderer>(); ;


        _itemScript = GameObject.Find("Game manager").GetComponent<Item_script>();
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        _gameManager = GameObject.Find("Game manager").GetComponent<Game_manager>();
    }

    void Update()
    {
        updateAppearance();

        if (_gameManager.current_screen != GameObject.Find("Combat_screen_UI"))
        {
            turn_sign.GetComponent<SpriteRenderer>().enabled = false;
        }

    }

    public void updateAppearance()
    {
        if (isPlayer)
        {

            sex = _characterStats.sex;
            hair_id = _characterStats.hair_id;
            eyes_id = _characterStats.eyes_id;
            nose_id = _characterStats.nose_id;
            mouth_id = _characterStats.mouth_id;
            body_id = _characterStats.body_id;

            hair_color_r = _characterStats.hair_color[0];
            hair_color_g = _characterStats.hair_color[1];
            hair_color_b = _characterStats.hair_color[2];

            hideHelmet = _characterStats.hideHelmet;

            if (sex)
            {
                _equipment_head.sprite = Resources.Load<Sprite>(_itemScript.items[_characterStats.Equipments[0]].sprite_male);
                _equipment_body.sprite = Resources.Load<Sprite>(_itemScript.items[_characterStats.Equipments[1]].sprite_male);
                _equipment_legs.sprite = Resources.Load<Sprite>(_itemScript.items[_characterStats.Equipments[2]].sprite_male);
                _equipment_left.sprite = Resources.Load<Sprite>(_itemScript.items[_characterStats.Equipments[3]].sprite_male);
                _equipment_shoulder.sprite = Resources.Load<Sprite>(_itemScript.items[_characterStats.Equipments[4]].sprite_male);
                _equipment_gadget.sprite = Resources.Load<Sprite>(_itemScript.items[_characterStats.Equipments[5]].sprite_male);
                _equipment_feet.sprite = Resources.Load<Sprite>(_itemScript.items[_characterStats.Equipments[6]].sprite_male);
                _equipment_right.sprite = Resources.Load<Sprite>(_itemScript.items[_characterStats.Equipments[7]].sprite_male);
            }
            else
            {
                _equipment_head.sprite = Resources.Load<Sprite>(_itemScript.items[_characterStats.Equipments[0]].sprite_female);
                _equipment_body.sprite = Resources.Load<Sprite>(_itemScript.items[_characterStats.Equipments[1]].sprite_female);
                _equipment_legs.sprite = Resources.Load<Sprite>(_itemScript.items[_characterStats.Equipments[2]].sprite_female);
                _equipment_left.sprite = Resources.Load<Sprite>(_itemScript.items[_characterStats.Equipments[3]].sprite_female);
                _equipment_shoulder.sprite = Resources.Load<Sprite>(_itemScript.items[_characterStats.Equipments[4]].sprite_female);
                _equipment_gadget.sprite = Resources.Load<Sprite>(_itemScript.items[_characterStats.Equipments[5]].sprite_female);
                _equipment_feet.sprite = Resources.Load<Sprite>(_itemScript.items[_characterStats.Equipments[6]].sprite_female);
                _equipment_right.sprite = Resources.Load<Sprite>(_itemScript.items[_characterStats.Equipments[7]].sprite_female);
            }

            if (hideHelmet)
            {
                _equipment_head.sprite = Resources.Load<Sprite>("");
            }
        }
        else
        {
            if (sex)
            {
                _equipment_head.sprite = Resources.Load<Sprite>(_itemScript.items[equipment_head_id].sprite_male);
                _equipment_body.sprite = Resources.Load<Sprite>(_itemScript.items[equipment_body_id].sprite_male);
                _equipment_legs.sprite = Resources.Load<Sprite>(_itemScript.items[equipment_legs_id].sprite_male);
                _equipment_left.sprite = Resources.Load<Sprite>(_itemScript.items[equipment_left_id].sprite_male);
                _equipment_shoulder.sprite = Resources.Load<Sprite>(_itemScript.items[equipment_shoulder_id].sprite_male);
                _equipment_gadget.sprite = Resources.Load<Sprite>(_itemScript.items[equipment_gadget_id].sprite_male);
                _equipment_feet.sprite = Resources.Load<Sprite>(_itemScript.items[equipment_feet_id].sprite_male);
                _equipment_right.sprite = Resources.Load<Sprite>(_itemScript.items[equipment_right_id].sprite_male);
            }
            else
            {
                _equipment_head.sprite = Resources.Load<Sprite>(_itemScript.items[equipment_head_id].sprite_female);
                _equipment_body.sprite = Resources.Load<Sprite>(_itemScript.items[equipment_body_id].sprite_female);
                _equipment_legs.sprite = Resources.Load<Sprite>(_itemScript.items[equipment_legs_id].sprite_female);
                _equipment_left.sprite = Resources.Load<Sprite>(_itemScript.items[equipment_left_id].sprite_female);
                _equipment_shoulder.sprite = Resources.Load<Sprite>(_itemScript.items[equipment_shoulder_id].sprite_female);
                _equipment_gadget.sprite = Resources.Load<Sprite>(_itemScript.items[equipment_gadget_id].sprite_female);
                _equipment_feet.sprite = Resources.Load<Sprite>(_itemScript.items[equipment_feet_id].sprite_female);
                _equipment_right.sprite = Resources.Load<Sprite>(_itemScript.items[equipment_right_id].sprite_female);
            }

            if (hideHelmet)
            {
                _equipment_head.sprite = Resources.Load<Sprite>("");
            }

        }



        _hair.color = new Color32(hair_color_r, hair_color_g, hair_color_b, 255);

        #region hair
        switch (hair_id)
        {
            case 0:
                _hair.sprite = Resources.Load<Sprite>("");
                break;
            case 1:
                _hair.sprite = Resources.Load<Sprite>("Character_appearances/Human_hair_1");
                break;
            case 2:
                _hair.sprite = Resources.Load<Sprite>("Character_appearances/Human_hair_2");
                break;
            case 3:
                _hair.sprite = Resources.Load<Sprite>("Character_appearances/Human_hair_3");
                break;
            case 4:
                _hair.sprite = Resources.Load<Sprite>("Character_appearances/Human_hair_4");
                break;
            case 5:
                _hair.sprite = Resources.Load<Sprite>("Character_appearances/Human_hair_5");
                break;
            case 6:
                _hair.sprite = Resources.Load<Sprite>("Character_appearances/Human_hair_6");
                break;
            case 7:
                _hair.sprite = Resources.Load<Sprite>("Character_appearances/Human_hair_7");
                break;
            case 8:
                _hair.sprite = Resources.Load<Sprite>("Character_appearances/Human_hair_8");
                break;
            default:
                _hair.sprite = Resources.Load<Sprite>("");
                break;
        }
        #endregion

        if (!hideHelmet && ((isPlayer && _characterStats.Equipments[0] != 0) || (equipment_head_id != 0)))
        {
            _hair.sprite = Resources.Load<Sprite>("");
        }


        #region eyes
        switch (eyes_id)
        {
            case 0:
                _eyes.sprite = Resources.Load<Sprite>("Character_appearances/Human_eyes_1");
                break;
            case 1:
                _eyes.sprite = Resources.Load<Sprite>("Character_appearances/Human_eyes_2");
                break;
            case 2:
                _eyes.sprite = Resources.Load<Sprite>("Character_appearances/Human_eyes_3");
                break;
            case 3:
                _eyes.sprite = Resources.Load<Sprite>("Character_appearances/Human_eyes_4");
                break;
            case 4:
                _eyes.sprite = Resources.Load<Sprite>("Character_appearances/Human_eyes_5");
                break;
            default:
                _eyes.sprite = Resources.Load<Sprite>("Character_appearances/Human_eyes_1");
                break;
        }
        #endregion

        #region nose
        switch (nose_id)
        {
            case 0:
                _nose.sprite = Resources.Load<Sprite>("Character_appearances/Human_nose_1");
                break;
            case 1:
                _nose.sprite = Resources.Load<Sprite>("Character_appearances/Human_nose_2");
                break;
            case 2:
                _nose.sprite = Resources.Load<Sprite>("Character_appearances/Human_nose_3");
                break;
            case 3:
                _nose.sprite = Resources.Load<Sprite>("Character_appearances/Human_nose_4");
                break;
            default:
                _nose.sprite = Resources.Load<Sprite>("Character_appearances/Human_nose_1");
                break;
        }
        #endregion

        #region mouth
        switch (mouth_id)
        {
            case 0:
                _mouth.sprite = Resources.Load<Sprite>("Character_appearances/Human_mouth_1");
                break;
            case 1:
                _mouth.sprite = Resources.Load<Sprite>("Character_appearances/Human_mouth_2");
                break;
            case 2:
                _mouth.sprite = Resources.Load<Sprite>("Character_appearances/Human_mouth_3");
                break;
            case 3:
                _mouth.sprite = Resources.Load<Sprite>("Character_appearances/Human_mouth_4");
                break;
            default:
                _mouth.sprite = Resources.Load<Sprite>("Character_appearances/Human_mouth_1");
                break;
        }
        #endregion

        if (sex)
        {
            switch (body_id)
            {
                case 0:
                    _body.sprite = Resources.Load<Sprite>("Character_appearances/Human_body_1");
                    break;
                case 1:
                    _body.sprite = Resources.Load<Sprite>("Character_appearances/Human_body_2");
                    break;
                case 2:
                    _body.sprite = Resources.Load<Sprite>("Character_appearances/Human_body_3");
                    break;
                case 3:
                    _body.sprite = Resources.Load<Sprite>("Character_appearances/Human_body_4");
                    break;
                default:
                    _body.sprite = Resources.Load<Sprite>("Character_appearances/Human_body_1");
                    break;
            }
        }
        else
        {
            switch (body_id)
            {
                case 0:
                    _body.sprite = Resources.Load<Sprite>("Character_appearances/Human_body_1_female");
                    break;
                case 1:
                    _body.sprite = Resources.Load<Sprite>("Character_appearances/Human_body_2_female");
                    break;
                case 2:
                    _body.sprite = Resources.Load<Sprite>("Character_appearances/Human_body_3_female");
                    break;
                case 3:
                    _body.sprite = Resources.Load<Sprite>("Character_appearances/Human_body_4_female");
                    break;
                default:
                    _body.sprite = Resources.Load<Sprite>("Character_appearances/Human_body_1_female");
                    break;
            }
        }
    }

}


