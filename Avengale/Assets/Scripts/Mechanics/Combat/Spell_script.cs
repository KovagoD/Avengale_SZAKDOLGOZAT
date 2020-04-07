using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum spell_types { damage, heal, support, passive }
public enum spell_attribute_types { health, resource, damage, money, penalty }
public enum spell_attribute_value_types { percentage, number }
public class Spell_script : MonoBehaviour
{
    public List<Spell> spells = new List<Spell>();
    public GameObject target;

    public GameObject row_2_unlock_text, row_3_unlock_text;
    public int firstRowPoints; public GameObject[] firstRow;
    public int secondRowPoints; public int secondRowNeededPoints = 5; public GameObject[] secondRow; public bool secondRowEnabled = false;
    public int thirdRowPoints; public int thirdRowNeededPoints = 10; public GameObject[] thirdRow; public bool thirdRowEnabled = false;
    void Awake()
    {
        initializeSpells();
    }

    public void initializeSpells()
    {
        spells.Clear();

        actualizeSpells();
    }
    public void actualizeSpells()
    {

        var stats = GameObject.Find("Game manager").GetComponent<Character_stats>();


        List<int> _spentSpellpoints = new List<int>();

        foreach (var spell in spells)
        {
            _spentSpellpoints.Add(spell.current_spell_points);
        }

        spells.Clear();

        /*
            ID, NAME, TYPE, CHARACTER CLASS, DESCRIPTION, ATTRIBUTE, ATTRIBUTE VALUE, RESOURCE COST, ICON, ANIMATION
        */
        string _currentRightHand;
        if (gameObject.GetComponent<Character_stats>().Equipments[7] != 0)
        {
            _currentRightHand = gameObject.GetComponent<Item_script>().items[gameObject.GetComponent<Character_stats>().Equipments[7]].icon;
        }
        else
        {
            _currentRightHand = "Item_icons/def_right";
        }

        spells.AddRange(new List<Spell>()
        {
            {new Spell(0, "", spell_types.damage, spell_attribute_types.damage, spell_attribute_value_types.number, 0, 0, 0, "nothing", null, 0, 0, 0)},
            {new Spell(1, "Simple attack", spell_types.damage, spell_attribute_types.damage, spell_attribute_value_types.number, 0, 0, 5, _currentRightHand, "attack_1", 1, 5, 1)},
            {new Spell(2, "Enraged blow", spell_types.damage, spell_attribute_types.damage, spell_attribute_value_types.number, 15, 15, 10, "Spell_icons/2", "attack_2", 2, 3, 0)},
            {new Spell(3, "Fast regeneration", spell_types.heal, spell_attribute_types.health, spell_attribute_value_types.number, 20, 20, 10, "Spell_icons/3", "heal_1", 2, 5, 0)},
            {new Spell(4, "Energy surge", spell_types.support, spell_attribute_types.resource, spell_attribute_value_types.number, 25, 25, 10, "Spell_icons/4", "resource_1", 3, 5, 0)},
            {new Spell(5, "Multi planetary healthcare", spell_types.passive, spell_attribute_types.health, spell_attribute_value_types.number, 50, 100, 0, "Spell_icons/5", null, 5, 3, 0)},
            {new Spell(6, "Rested state", spell_types.passive, spell_attribute_types.resource, spell_attribute_value_types.number, 50, 100, 0, "Spell_icons/6", null, 5, 3, 0)},
            {new Spell(7, "Constant rage", spell_types.passive, spell_attribute_types.damage, spell_attribute_value_types.number, 50, 100, 0, "Spell_icons/7", null, 5, 3, 0)},
            {new Spell(8, "Decimating blow", spell_types.damage,spell_attribute_types.damage, spell_attribute_value_types.percentage, 50, 50, 25, "Spell_icons/8", "attack_3", 5, 1, 0)},
            {new Spell(9, "Inspiration", spell_types.support,  spell_attribute_types.resource, spell_attribute_value_types.percentage, 50, 50, 10, "Spell_icons/9", "resource_1", 7, 1, 0)},
            {new Spell(10, "Healing meditation", spell_types.heal, spell_attribute_types.health, spell_attribute_value_types.percentage, 50, 50, 30, "Spell_icons/10", "heal_1", 7, 2, 0)},
            {new Spell(11, "Natural charm", spell_types.passive, spell_attribute_types.money, spell_attribute_value_types.number, 10, 10, 30, "Spell_icons/11", null, 1, 7, 0)},
            {new Spell(12, "Special treatment", spell_types.passive, spell_attribute_types.penalty, spell_attribute_value_types.number, 10, 10, 30, "Spell_icons/12", null, 1, 5, 0)},
        });

        for (int i = 0; i < _spentSpellpoints.Count; i++)
        {
            spells[i].current_spell_points = _spentSpellpoints[i];
        }
    }
    public void saveSpells()
    {
        Save_script.saveSpells(this);
        Debug.Log("Spell data saved!");
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
                /*
                spell.attribute = Convert.ToInt32((spell.starterAttribute + _characterStats.Local_damage) * _multiplier);
                spell.description = "Deals +" + spell.attribute + " damage to the <b>target<b>.";
                */

                if (spell.attribute_type == spell_attribute_value_types.percentage)
                {
                    spell.lastAttribute = spell.attribute;
                    spell.attribute = Convert.ToInt32(spell.starterAttribute * _multiplier);
                    spell.description = "Deals +" + spell.attribute + "% damage to the <b>target<b>.";

                }
                else if (spell.attribute_type == spell_attribute_value_types.number)
                {
                    spell.lastAttribute = spell.attribute;
                    spell.attribute = Convert.ToInt32((spell.starterAttribute + _characterStats.Local_damage) * _multiplier);
                    spell.description = "Deals +" + spell.attribute + " damage to the <b>target<b>.";
                }
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
                if (spell.attribute_type == spell_attribute_value_types.percentage)
                {
                    spell.lastAttribute = spell.attribute;
                    spell.attribute = Convert.ToInt32(spell.starterAttribute * _multiplier);
                    spell.description = "You regain +" + spell.attribute + "% resource.";
                }
                else if (spell.attribute_type == spell_attribute_value_types.number)
                {
                    spell.lastAttribute = spell.attribute;
                    spell.attribute = Convert.ToInt32((spell.starterAttribute * _multiplier));
                    spell.description = "You regain +" + spell.attribute + " resource.";
                }
            }

            if (spell.type == spell_types.passive)
            {
                if (spell.attribute_type == spell_attribute_value_types.number)
                {
                    spell.lastAttribute = spell.attribute;
                    spell.attribute = Convert.ToInt32((spell.starterAttribute * _multiplier));
                    if (spell.attribute_name == spell_attribute_types.health)
                    {

                        spell.description = "Grants you +" + spell.attribute + " health passively.";
                    }
                    else if (spell.attribute_name == spell_attribute_types.resource)
                    {
                        spell.description = "Grants you +" + spell.attribute + " resource passively.";
                    }
                    else if (spell.attribute_name == spell_attribute_types.damage)
                    {
                        spell.description = "Grants you +" + spell.attribute + " damage passively.";
                    }
                    else if (spell.attribute_name == spell_attribute_types.money)
                    {
                        spell.description = "Grants you +" + spell.attribute + "% more credits from quests, rewards and selling items.";
                    }
                    else if (spell.attribute_name == spell_attribute_types.penalty)
                    {
                        spell.description = "-" + spell.attribute + "% penalty when you are defeated.";
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

        var _row_2_text = row_2_unlock_text.GetComponent<Text_animation>();
        var _row_3_text = row_3_unlock_text.GetComponent<Text_animation>();

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
    public Spell(int id, string name, spell_types type, spell_attribute_types attribute_name, spell_attribute_value_types attribute_type, int starterAttribute, int attribute, int resource_cost, string icon, string animation, int level_requirement, int max_spell_points, int current_spell_points)
    {
        this.id = id;
        this.name = name;
        this.type = type;
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
            else if (attribute_name == spell_attribute_types.resource)
            {
                if (current_spell_points >= 2)
                {
                    _characterStats.Local_max_resource -= lastAttribute;
                }
                _characterStats.Local_max_resource += attribute;
            }
            else if (attribute_name == spell_attribute_types.damage)
            {
                if (current_spell_points >= 2)
                {
                    _characterStats.Local_damage -= lastAttribute;
                }
                _characterStats.Local_damage += attribute;
            }
            else if (attribute_name == spell_attribute_types.money)
            {
                if (current_spell_points >= 2)
                {
                    _characterStats.Local_plus_money_rate -= lastAttribute;
                }
                _characterStats.Local_plus_money_rate += attribute;
            }
            else if (attribute_name == spell_attribute_types.penalty)
            {
                if (current_spell_points >= 2)
                {
                    _characterStats.Local_penalty_rate -= lastAttribute;
                }
                _characterStats.Local_penalty_rate += attribute;
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
                    if (attribute_type == spell_attribute_value_types.percentage)
                    {
                        target.GetComponent<Enemy_script>().opponentTakeDamage(target.GetComponent<Enemy_script>().getPercentOfHealth(attribute) * 2);
                        target.GetComponent<Character_manager>().damage_text.GetComponent<Text_animation>().startAnim("-" + target.GetComponent<Enemy_script>().getPercentOfHealth(attribute) * 2 + " CRITICAL!", 0.05f);
                    }
                    else
                    {
                        target.GetComponent<Enemy_script>().opponentTakeDamage(attribute);
                        target.GetComponent<Character_manager>().damage_text.GetComponent<Text_animation>().startAnim("-" + attribute * 2 + " CRITICAL!", 0.05f);
                    }

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

                }
                else
                {
                    if (attribute_type == spell_attribute_value_types.percentage)
                    {
                        Debug.Log(target.GetComponent<Enemy_script>().getPercentOfHealth(attribute));
                        target.GetComponent<Enemy_script>().opponentTakeDamage(target.GetComponent<Enemy_script>().getPercentOfHealth(attribute));
                        target.GetComponent<Character_manager>().damage_text.GetComponent<Text_animation>().startAnim("-" + target.GetComponent<Enemy_script>().getPercentOfHealth(attribute), 0.05f);

                    }
                    else
                    {
                        target.GetComponent<Enemy_script>().opponentTakeDamage(attribute);
                        target.GetComponent<Character_manager>().damage_text.GetComponent<Text_animation>().startAnim("-" + attribute, 0.05f);

                    }

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

                }

                target.GetComponent<Character_manager>().damage_text.GetComponent<Animator>().Play(_hitAnimation);


                GameObject local_character = GameObject.Find("Character");
                local_character.GetComponent<Character_manager>().damage_text.GetComponent<Text_animation>().startAnim("-" + resource_cost + " resource", 0.05f);
                local_character.GetComponent<Character_manager>().damage_text.GetComponent<Animator>().Play("resource_1");




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

            if (attribute_type == spell_attribute_value_types.percentage)
            {
                Debug.Log(target.GetComponent<Character_stats>().getPercentOfResource(attribute));
                target.GetComponent<Character_stats>().getResource(target.GetComponent<Character_stats>().getPercentOfResource(attribute));
                local_character.GetComponent<Character_manager>().damage_text.GetComponent<Text_animation>().startAnim("+" + target.GetComponent<Character_stats>().getPercentOfResource(attribute) + " resource", 0.05f);
                GameObject.Find("Resource_bar").GetComponent<Bar_script>().updateResourceAddition();
            }
            else
            {
                target.GetComponent<Character_stats>().getResource(attribute);
                local_character.GetComponent<Character_manager>().damage_text.GetComponent<Text_animation>().startAnim("+" + attribute + " resource", 0.05f);
                GameObject.Find("Resource_bar").GetComponent<Bar_script>().updateResourceAddition();
            }
            local_character.GetComponent<Character_manager>().damage_text.GetComponent<Animator>().Play(animation);
        }

    }


}