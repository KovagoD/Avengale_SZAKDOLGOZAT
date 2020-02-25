using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_manager : MonoBehaviour
{
    public bool isPlayer = false;

    [Header("Equipments")]
    public GameObject equimpent_head;
    private SpriteRenderer _equimpent_head;
    public int equipment_head_id;
    public GameObject equimpent_body;
    private SpriteRenderer _equimpent_body;
    public int equipment_body_id;
    public GameObject equimpent_legs;
    private SpriteRenderer _equimpent_legs;
    public int equipment_legs_id;
    public GameObject equimpent_left;
    private SpriteRenderer _equimpent_left;
    public int equipment_left_id;
    public GameObject equimpent_shoulder;
    private SpriteRenderer _equimpent_shoulder;
    public int equipment_shoulder_id;
    public GameObject equimpent_gadget;
    private SpriteRenderer _equimpent_gadget;
    public int equipment_gadget_id;
    public GameObject equimpent_feet;
    private SpriteRenderer _equimpent_feet;
    public int equipment_feet_id;
    public GameObject equimpent_right;
    private SpriteRenderer _equimpent_right;

    public int equipment_right_id;
    public GameObject turn_sign;
    public Animator spell_animation;




    [Header("Stats")]
    public int hair_id;
    public int eyes_id;
    public int nose_id;
    public int mouth_id;
    public int body_id;

    [Header("Appearance")]
    public GameObject hair;
    private SpriteRenderer _hair;
    public GameObject eyes;
    private SpriteRenderer _eyes;
    public GameObject nose;
    private SpriteRenderer _nose;
    public GameObject mouth;
    private SpriteRenderer _mouth;
    public GameObject body;
    private SpriteRenderer _body;



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

        _equimpent_head = equimpent_head.GetComponent<SpriteRenderer>();
        _equimpent_body = equimpent_body.GetComponent<SpriteRenderer>(); ;
        _equimpent_legs = equimpent_legs.GetComponent<SpriteRenderer>(); ;
        _equimpent_left = equimpent_left.GetComponent<SpriteRenderer>(); ;
        _equimpent_shoulder = equimpent_shoulder.GetComponent<SpriteRenderer>(); ;
        _equimpent_gadget = equimpent_gadget.GetComponent<SpriteRenderer>(); ;
        _equimpent_feet = equimpent_feet.GetComponent<SpriteRenderer>(); ;
        _equimpent_right = equimpent_right.GetComponent<SpriteRenderer>(); ;


        _itemScript = GameObject.Find("Game manager").GetComponent<Item_script>();
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        _gameManager = GameObject.Find("Game manager").GetComponent<Game_manager>();
    }

    void Update()
    {
        updateAppearance();
        if (_gameManager.current_screen != GameObject.Find("Combat_screen_UI"))
        {
            turn_sign.GetComponent<SpriteRenderer>().enabled=false;
        }
        
    }

    public void updateAppearance()
    {
        if (isPlayer)
        {
            _equimpent_head.sprite = _itemScript.items[_characterStats.Equipments[0]].sprite;
            _equimpent_body.sprite = _itemScript.items[_characterStats.Equipments[1]].sprite;
            _equimpent_legs.sprite = _itemScript.items[_characterStats.Equipments[2]].sprite;
            _equimpent_left.sprite = _itemScript.items[_characterStats.Equipments[3]].sprite;
            _equimpent_shoulder.sprite = _itemScript.items[_characterStats.Equipments[4]].sprite;
            _equimpent_gadget.sprite = _itemScript.items[_characterStats.Equipments[5]].sprite;
            _equimpent_feet.sprite = _itemScript.items[_characterStats.Equipments[6]].sprite;
            _equimpent_right.sprite = _itemScript.items[_characterStats.Equipments[7]].sprite;


            hair_id = _characterStats.hair_id;
            eyes_id = _characterStats.eyes_id;
            nose_id = _characterStats.nose_id;
            mouth_id = _characterStats.mouth_id;
            body_id = _characterStats.body_id;
        }
        else
        {
            _equimpent_head.sprite = _itemScript.items[equipment_head_id].sprite;
            _equimpent_body.sprite = _itemScript.items[equipment_body_id].sprite;
            _equimpent_legs.sprite = _itemScript.items[equipment_legs_id].sprite;
            _equimpent_left.sprite = _itemScript.items[equipment_left_id].sprite;
            _equimpent_shoulder.sprite = _itemScript.items[equipment_shoulder_id].sprite;
            _equimpent_gadget.sprite = _itemScript.items[equipment_gadget_id].sprite;
            _equimpent_feet.sprite = _itemScript.items[equipment_feet_id].sprite;
            _equimpent_right.sprite = _itemScript.items[equipment_right_id].sprite;
        }

        switch (eyes_id)
        {
            case 0:
                _eyes.sprite = Resources.Load<Sprite>("Character_appearances/Human_eyes_1");
                break;
            case 1:
                _eyes.sprite = Resources.Load<Sprite>("Character_appearances/Human_eyes_2");
                break;
            default:
                break;
        }

        switch (nose_id)
        {
            case 0:
                _nose.sprite = Resources.Load<Sprite>("Character_appearances/Human_nose_1");
                break;
            case 1:
                _nose.sprite = Resources.Load<Sprite>("Character_appearances/Human_nose_2");
                break;
            default:
                break;
        }

        switch (mouth_id)
        {
            case 0:
                _mouth.sprite = Resources.Load<Sprite>("Character_appearances/Human_mouth_1");
                break;
            case 1:
                _mouth.sprite = Resources.Load<Sprite>("Character_appearances/Human_mouth_2");
                break;
            default:
                break;
        }

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
                break;
        }
    }
}
