using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum spell_types { damage, heal, support, passive }
public enum spell_attribute_types { health, resource, damage }
public enum spell_attribute_value_types { percentage, number }
public class Spell_script : MonoBehaviour
{
    public List<Spell> spells = new List<Spell>();
    public GameObject target;

    public int firstRowPoints; public GameObject[] firstRow;
    public int secondRowPoints; public int secondRowNeededPoints = 5; public GameObject[] secondRow; public bool secondRowEnabled = false;
    public int thirdRowPoints; public int thirdRowNeededPoints = 10; public GameObject[] thirdRow; public bool thirdRowEnabled = false;
    void Awake()
    {

    }

    public void initializeSpells()
    {
        var stats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        spells.Clear();
        /*
            ID, NAME, TYPE, CHARACTER CLASS, DESCRIPTION, ATTRIBUTE, ATTRIBUTE VALUE, RESOURCE COST, ICON, ANIMATION
        */
        string _currentRightHand;
        if (gameObject.GetComponent<Character_stats>().Equipments[7] != 0)
        {
            _currentRightHand = gameObject.GetComponent<Item_script>().items[gameObject.GetComponent<Character_stats>().Equipments[7]].icon;
        } else
        {
            _currentRightHand = "Spell_icons/5";
        }

        spells.AddRange(new List<Spell>()
        {
            {new Spell(0, "", spell_types.damage, "", "", spell_attribute_types.damage, spell_attribute_value_types.number, 0, 0, 0, "nothing", null, 0, 0, 0)},
            {new Spell(1, "Simple attack", spell_types.damage, "warrior", "Deals ", spell_attribute_types.damage, spell_attribute_value_types.number, 0, 0, 10, _currentRightHand, "attack_1", 1, 5, 2)},
            {new Spell(2, "Enraged blow", spell_types.damage, "warrior", "attacks the <b>target</b> furiously.", spell_attribute_types.damage, spell_attribute_value_types.number, 15, 15, 10, "Spell_icons/2", "attack_1", 2, 3, 0)},
            {new Spell(3, "Fast regeneration", spell_types.heal, "warrior", "Heal <b>yourself</b>.", spell_attribute_types.health, spell_attribute_value_types.percentage, 20, 20, 10, "Spell_icons/3", "heal_1", 2, 5, 0)},
            {new Spell(4, "Energy surge", spell_types.support, "warrior", "In metus ante, malesuada nec libero non, laoreet condimentum lectus. ", spell_attribute_types.resource, spell_attribute_value_types.number, 25, 25, 10, "Spell_icons/4", null, 5, 5, 0)},
            {new Spell(5, "Multi planetary healthcare", spell_types.passive, "warrior", "grants extra health passively.", spell_attribute_types.health, spell_attribute_value_types.number, 50, 100, 0, "Spell_icons/5", "attack_1", 1, 5, 0)},
            {new Spell(6, "Third row explosion", spell_types.passive, "warrior", "grants extra health passively.", spell_attribute_types.health, spell_attribute_value_types.number, 50, 100, 0, "Spell_icons/5", "attack_1", 1, 5, 0)},
        });
    }
    public void saveSpells()
    {
        Save_script.saveSpells(this);
        Debug.Log("Item data saved!");
    }

    public void loadSpells()
    {
        SpellData data = Save_script.loadSpells();
        if (data != null)
        {
            spells = data.spells;
        }
    }

    public void setTarget(GameObject input_target)
    {
        target = input_target;
    }

    public void setupAttributes()
    {
        var _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        foreach (var spell in spells)
        {
            double _multiplier = 1;

            switch (spell.current_spell_points)
            {
                case 0:
                    _multiplier = 1;
                    break;
                case 1:
                    _multiplier = 1;
                    break;
                case 2:
                    _multiplier = 1.25;
                    break;
                case 3:
                    _multiplier = 1.5;
                    break;
                case 4:
                    _multiplier = 1.75;
                    break;
                case 5:
                    _multiplier = 2;
                    break;
            }



            if (spell.type == spell_types.damage)
            {
                spell.attribute = Convert.ToInt32((spell.starterAttribute + _characterStats.Local_damage) * _multiplier);
                spell.description = "Deals +" + spell.attribute + " damage to the <b>target<b>.";
            }

            if (spell.type == spell_types.heal)
            {
                if (spell.attribute_type == spell_attribute_value_types.percentage)
                {

                    spell.lastAttribute = spell.attribute;
                    spell.attribute = Convert.ToInt32(spell.starterAttribute * _multiplier);
                    spell.description = "Heals you +" + spell.attribute + "% health.";

                }
                else if (spell.attribute_type == spell_attribute_value_types.number)
                {

                    spell.lastAttribute = spell.attribute;
                    spell.attribute = Convert.ToInt32((spell.starterAttribute * _multiplier));
                    spell.description = "Heals you +" + spell.attribute + " health.";

                }
            }

            if (spell.type == spell_types.support)
            {
                spell.lastAttribute = spell.attribute;
                spell.attribute = Convert.ToInt32((spell.starterAttribute * _multiplier));
                spell.description = "Gives you +" + spell.attribute + " resource.";
            }

            if (spell.type == spell_types.passive)
            {
                if (spell.attribute_type == spell_attribute_value_types.percentage)
                {
                    if (spell.attribute_name == spell_attribute_types.health)
                    {
                        spell.lastAttribute = spell.attribute;
                        spell.attribute = Convert.ToInt32(spell.starterAttribute * _multiplier);
                        spell.description = "Grants you +" + spell.attribute + "% health passively.";
                    }
                }
                else if (spell.attribute_type == spell_attribute_value_types.number)
                {
                    if (spell.attribute_name == spell_attribute_types.health)
                    {
                        spell.lastAttribute = spell.attribute;
                        spell.attribute = Convert.ToInt32((spell.starterAttribute * _multiplier));
                        spell.description = "Grants you +" + spell.attribute + " health passively.";
                    }
                }
            }
        }
    }

    private void Update()
    {
        //checkRowAvailability();
    }

    public void checkRowAvailability()
    {
        firstRowPoints = 0;
        secondRowPoints = 0;

        foreach (var slot in firstRow)
        {
            firstRowPoints += spells[slot.GetComponent<Talent_slot_script>().spell_id].current_spell_points;
        }

        foreach (var slot in secondRow)
        {
            secondRowPoints += spells[slot.GetComponent<Talent_slot_script>().spell_id].current_spell_points;
        }

        if (firstRowPoints >= secondRowNeededPoints)
        {
            secondRowEnabled = true;
            foreach (var slot in secondRow)
            {
                slot.GetComponent<Talent_slot_script>().setEnabled();
            }
        }

        if ((firstRowPoints + secondRowPoints) >= thirdRowNeededPoints)
        {
            thirdRowEnabled = true;
            foreach (var slot in thirdRow)
            {
                slot.GetComponent<Talent_slot_script>().setEnabled();
            }
        }

        var _row_2_text = GameObject.Find("row_2_unlock_text").GetComponent<Text_animation>();
        var _row_3_text = GameObject.Find("row_3_unlock_text").GetComponent<Text_animation>();

        if (neededForSecond() > 0)
        {
            _row_2_text.startAnim("Spend " + neededForSecond() + " more points to unlock §", 0.05f);
        }
        else
        {
            _row_2_text.startAnim("Unlocked ±", 0.05f);
        }

        if (neededForThird() > 0)
        {
            _row_3_text.startAnim("Spend " + neededForThird() + " more points to unlock §", 0.05f);
        }
        else
        {
            _row_3_text.startAnim("Unlocked ±", 0.05f);
        }
    }
    public int neededForSecond()
    {
        return secondRowNeededPoints - firstRowPoints;
    }

    public int neededForThird()
    {
        return thirdRowNeededPoints - (firstRowPoints + secondRowPoints);
    }
}


[System.Serializable]
public class Spell
{
    public int id;
    public string name;
    public spell_types type;
    public string char_class;
    public string description;
    public spell_attribute_types attribute_name;
    public spell_attribute_value_types attribute_type;
    public int attribute;

    public int starterAttribute;
    public int lastAttribute;

    public string animation;

    public int resource_cost;


    public int max_spell_points;
    public int current_spell_points;
    public int level_requirement;
    public string icon;
    public Spell(int id, string name, spell_types type, string char_class, string description, spell_attribute_types attribute_name, spell_attribute_value_types attribute_type, int starterAttribute, int attribute, int resource_cost, string icon, string animation, int level_requirement, int max_spell_points, int current_spell_points)
    {
        this.id = id;
        this.name = name;
        this.type = type;
        this.char_class = char_class;
        this.description = description;
        this.attribute_name = attribute_name;

        this.attribute = attribute;
        this.starterAttribute = starterAttribute;

        this.resource_cost = resource_cost;
        this.icon = icon;

        this.animation = animation;

        this.attribute_name = attribute_name;
        this.attribute_type = attribute_type;

        this.level_requirement = level_requirement;
        this.max_spell_points = max_spell_points;
        this.current_spell_points = current_spell_points;

    }
    public void passiveActivate()
    {
        var _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();


        if (type == spell_types.passive)
        {
            if (attribute_name == spell_attribute_types.health)
            {
                if (current_spell_points >= 2)
                {
                    _characterStats.Local_max_health -= lastAttribute;
                }
                _characterStats.Local_max_health += attribute;
            }
        }
    }

    public void Activate(GameObject target)
    {
        var _notification = GameObject.Find("Notification").GetComponent<Ingame_notification_script>();
        var _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();

        GameObject.Find("Game manager").GetComponent<Character_stats>().looseResource(resource_cost);

        if (type == spell_types.damage && target != null)
        {
            if (target != null && target.GetComponent<Enemy_script>().enemy_health > 0)
            {
                target.GetComponent<Character_manager>().spell_animation.Play(animation);
                GameObject.Find("Battle_scene").GetComponent<Animator>().Play("Screen_shake_1");


                string _hitAnimation = "";
                int random_crit = UnityEngine.Random.Range(0, 100);

                if (random_crit > 90)
                {
                    target.GetComponent<Enemy_script>().opponentTakeDamage(attribute * 2);

                    int random_hit = UnityEngine.Random.Range(1, 3);
                    switch (random_hit)
                    {
                        case 1:
                            _hitAnimation = "crit_hit_1";
                            break;
                        case 2:
                            _hitAnimation = "crit_hit_2";
                            break;
                    }

                    target.GetComponent<Character_manager>().damage_text.GetComponent<Text_animation>().startAnim("-" + attribute * 2 + " CRITICAL!", 0.05f);
                }
                else
                {
                    target.GetComponent<Enemy_script>().opponentTakeDamage(attribute);

                    int random_hit = UnityEngine.Random.Range(1, 7);


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
                    target.GetComponent<Character_manager>().damage_text.GetComponent<Text_animation>().startAnim("-" + attribute, 0.05f);
                }


                target.GetComponent<Character_manager>().damage_text.GetComponent<Animator>().Play(_hitAnimation);
                if (GameObject.Find("Game manager").GetComponent<Game_manager>().vibrationEnabled)
                {
                    Handheld.Vibrate();
                }
            }
            else
            {
                _notification.message("You need a <b>target</b> first!", 3, "red");
            }
        }

        if (type == spell_types.heal)
        {
            target = GameObject.Find("Game manager");
            GameObject local_character = GameObject.Find("Character");

            if (attribute_type == spell_attribute_value_types.percentage)
            {
                target.GetComponent<Character_stats>().getHealth(target.GetComponent<Character_stats>().getPercentOfHealth(attribute));
                local_character.GetComponent<Character_manager>().damage_text.GetComponent<Text_animation>().startAnim("+" + target.GetComponent<Character_stats>().getPercentOfHealth(attribute) + " health", 0.05f);
                GameObject.Find("Health_bar").GetComponent<Bar_script>().updateHealthAddition();
            }
            else
            {
                target.GetComponent<Character_stats>().getHealth(attribute);
                local_character.GetComponent<Character_manager>().damage_text.GetComponent<Text_animation>().startAnim("+" + attribute + " health", 0.05f);
                GameObject.Find("Health_bar").GetComponent<Bar_script>().updateHealthAddition();
            }
            local_character.GetComponent<Character_manager>().damage_text.GetComponent<Animator>().Play(animation);
        }
        if (type == spell_types.support)
        {
            target = GameObject.Find("Game manager");
            GameObject local_character = GameObject.Find("Character");

            target.GetComponent<Character_stats>().getResource(attribute);
            GameObject.Find("Resource_bar").GetComponent<Bar_script>().updateResourceAddition();
            Debug.Log("Done");
        }
    }


}