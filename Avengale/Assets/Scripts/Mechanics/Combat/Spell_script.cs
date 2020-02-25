﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_script : MonoBehaviour
{
    public List<Spell> spells = new List<Spell>();
    public GameObject target;




    void Awake()
    {
        var stats = GameObject.Find("Game manager").GetComponent<Character_stats>();

        /*
            ID, NAME, TYPE, CHARACTER CLASS, DESCRIPTION, ATTRIBUTE, ATTRIBUTE VALUE, RESOURCE COST, ICON, ANIMATION
        */

        spells.Add(new Spell(0, "", "", "", "", ".", 0, 0, Resources.Load<Sprite>("nothing"), null));
        spells.Add(new Spell(1, "Enraged attack", "attack", "warrior", "attacks the <b>target</b> furiously.", "damage.", 15, 10, Resources.Load<Sprite>("Item_icons/Icon2"), "attack_1"));
        spells.Add(new Spell(2, "Heal me", "healing", "warrior", "Heal <b>yourself</b>.", "health.%", stats.getPercentOfHealth(50), 10, Resources.Load<Sprite>("Item_icons/Icon"), null));
        spells.Add(new Spell(3, "Shield", "buff", "warrior", "In metus ante, malesuada nec libero non, laoreet condimentum lectus. ", "resource.", 9999, 10, Resources.Load<Sprite>("Item_icons/Icon2"), null));
    }

    public void setTarget(GameObject input_target)
    {
        target = input_target;
    }

    public void setupAttributes()
    {
        foreach (var spell in spells)
        {
            if (spell.type == "attack")
            {
                spell.attribute = spell.attribute + GameObject.Find("Game manager").GetComponent<Character_stats>().Local_damage;
            }

            if (spell.type == "healing")
            {
                spell.attribute = GameObject.Find("Game manager").GetComponent<Character_stats>().Local_max_health;
            }

            if (spell.type == "buff")
            {
                spell.attribute = GameObject.Find("Game manager").GetComponent<Character_stats>().Local_max_resource;
            }

        }
    }


}

public class Spell
{
    public int id;
    public string name;
    public string type;
    public string char_class;
    public string description;
    public string attribute_name;
    public string attribute_type;
    public int attribute;

    public string animation;

    public int resource_cost;

    public Sprite icon;
    private Ingame_notification_script _notification;
    private Character_stats _characterStats;
    private Character_manager _characterManager;


    private void Start()
    {
        _notification = GameObject.Find("Notification").GetComponent<Ingame_notification_script>();
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        _characterManager = GameObject.Find("Character").GetComponent<Character_manager>();
    }
    public Spell(int id, string name, string type, string char_class, string description, string attribute_name, int attribute, int resource_cost, Sprite icon, string animation)
    {
        this.id = id;
        this.name = name;
        this.type = type;
        this.char_class = char_class;
        this.description = description;
        this.attribute_name = attribute_name;
        this.attribute = attribute;
        this.resource_cost = resource_cost;
        this.icon = icon;

        this.animation = animation;

        var split = attribute_name.Split('.');
        this.attribute_name = split[0];
        if (split[1] == "%")
        {
            this.attribute_type = "%";
        }

    }

    public void Activate(GameObject target)
    {

        GameObject.Find("Game manager").GetComponent<Character_stats>().looseResource(resource_cost);

        if (type == "attack" && target != null)
        {
            if (target != null && target.GetComponent<Enemy_script>().enemy_health > 0)
            {
                target.GetComponent<Character_manager>().spell_animation.Play(animation);
                target.GetComponent<Enemy_script>().opponentTakeDamage(attribute);

                int random_hit = UnityEngine.Random.Range(1, 7);
                string _hitAnimation = "";

                switch (random_hit)
                {
                    case 1:
                        _hitAnimation = "hit_1";
                        break;
                    case 2:
                        _hitAnimation = "hit_2";
                        break;
                    case 3:
                        _hitAnimation = "hit_3";
                        break;
                    case 4:
                        _hitAnimation = "hit_4";
                        break;
                    case 5:
                        _hitAnimation = "hit_5";
                        break;
                    case 6:
                        _hitAnimation = "hit_6";
                        break;
                }

                target.GetComponent<Character_manager>().damage_text.GetComponent<Animator>().Play(_hitAnimation);
                target.GetComponent<Character_manager>().damage_text.GetComponent<Text_animation>().startAnim("-" + attribute, 0.05f);
            }
            else { _notification.message("Need to select a target first!", 3, "red"); }
        }

        if (type == "healing")
        {
            if (attribute_type == "%")
            {
                _characterStats.getHealth(_characterStats.getPercentOfHealth(attribute));

                Debug.Log("Done");

            }
            else
            {
                _characterStats.getHealth(attribute);

                Debug.Log("Done");
            }

        }

        if (type == "buff")
        {
            _characterStats.getResource(attribute);
            Debug.Log("Done");
        }



    }

}