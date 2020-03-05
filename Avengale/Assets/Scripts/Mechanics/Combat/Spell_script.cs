using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum spell_types { damage, heal, support, passive }
public class Spell_script : MonoBehaviour
{
    public List<Spell> spells = new List<Spell>();
    public GameObject target;



    public int firstRowPoints; public GameObject[] firstRow;
    public int secondRowPoints; public GameObject[] secondRow; public bool secondRowEnabled = false;
    public int thirdRowPoints; public GameObject[] thirdRow;

    void Awake()
    {
        var stats = GameObject.Find("Game manager").GetComponent<Character_stats>();

        /*
            ID, NAME, TYPE, CHARACTER CLASS, DESCRIPTION, ATTRIBUTE, ATTRIBUTE VALUE, RESOURCE COST, ICON, ANIMATION
        */

        spells.Add(new Spell(0, "", spell_types.damage, "", "", ".", 0, 0, 0, Resources.Load<Sprite>("nothing"), null, 0, 0));
        spells.Add(new Spell(1, "Attack", spell_types.damage, "warrior", "ATTACC", "damage.", 0, 0, 10, Resources.Load<Sprite>("Item_icons/Icon"), "attack_1", 1, 5));
        spells.Add(new Spell(2, "Enraged attack", spell_types.damage, "warrior", "attacks the <b>target</b> furiously.", "damage.", 15, 15, 10, Resources.Load<Sprite>("Item_icons/Icon2"), "attack_1", 2, 3));
        spells.Add(new Spell(3, "Heal me", spell_types.heal, "warrior", "Heal <b>yourself</b>.", "health.%", 0, stats.getPercentOfHealth(50), 10, Resources.Load<Sprite>("Item_icons/Icon"), null, 2, 5));
        spells.Add(new Spell(4, "Shield", spell_types.support, "warrior", "In metus ante, malesuada nec libero non, laoreet condimentum lectus. ", "resource.", 9999, 9999, 10, Resources.Load<Sprite>("Item_icons/Icon2"), null, 5, 10));
        spells.Add(new Spell(5, "Multi planetary healthcare", spell_types.passive, "warrior", "grants extra health passively.", "health.", 100, 100, 10, Resources.Load<Sprite>("Item_icons/Icon"), "attack_1", 1, 5));
    }

    public void setTarget(GameObject input_target)
    {
        target = input_target;
    }

    public void setupAttributes()
    {
        foreach (var spell in spells)
        {
            if (spell.type == spell_types.damage)
            {
                spell.attribute = spell.starterAttribute + GameObject.Find("Game manager").GetComponent<Character_stats>().Local_damage;
            }

            if (spell.type == spell_types.heal)
            {
                spell.attribute = GameObject.Find("Game manager").GetComponent<Character_stats>().Local_max_health;
            }

            if (spell.type == spell_types.support)
            {
                spell.attribute = GameObject.Find("Game manager").GetComponent<Character_stats>().Local_max_resource;
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
        foreach (var slot in firstRow)
        {
            firstRowPoints += slot.GetComponent<Talent_slot_script>().spell_points;
        }

        if (firstRowPoints >= 5)
        {
            secondRowEnabled = true;
            foreach (var slot in secondRow)
            {
                slot.GetComponent<Talent_slot_script>().setEnabled();
            }
        }
    }
}



public class Spell
{
    public int id;
    public string name;
    public spell_types type;
    public string char_class;
    public string description;
    public string attribute_name;
    public string attribute_type;
    public int attribute;

    public int starterAttribute;
    public string animation;

    public int resource_cost;


    public int spell_points;
    public int level_requirement;
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
    public Spell(int id, string name, spell_types type, string char_class, string description, string attribute_name, int starterAttribute, int attribute, int resource_cost, Sprite icon, string animation, int level_requirement, int spell_points)
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

        var split = attribute_name.Split('.');
        this.attribute_name = split[0];
        if (split[1] == "%")
        {
            this.attribute_type = "%";
        }

        this.level_requirement = level_requirement;
        this.spell_points = spell_points;

    }

    public void Activate(GameObject target)
    {

        GameObject.Find("Game manager").GetComponent<Character_stats>().looseResource(resource_cost);

        if (type == spell_types.damage && target != null)
        {
            if (target != null && target.GetComponent<Enemy_script>().enemy_health > 0)
            {
                target.GetComponent<Character_manager>().spell_animation.Play(animation);
                GameObject.Find("Battle_scene").GetComponent<Animator>().Play("Screen_shake_1");

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
                Handheld.Vibrate();
                target.GetComponent<Character_manager>().damage_text.GetComponent<Text_animation>().startAnim("-" + attribute, 0.05f);
            }
            else { _notification.message("Need to select a target first!", 3, "red"); }
        }

        if (type == spell_types.heal)
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

        if (type == spell_types.support)
        {
            _characterStats.getResource(attribute);
            Debug.Log("Done");
        }



    }

}