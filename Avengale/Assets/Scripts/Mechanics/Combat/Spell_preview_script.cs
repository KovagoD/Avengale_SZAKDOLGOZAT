using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spell_preview_script : MonoBehaviour
{
    [Header("Spell properties")]
    public int spell_id;
    public GameObject spell_name;
    public GameObject spell_type;
    public GameObject spell_description;
    public GameObject spell_cost;
    public GameObject spell_effect;
    public GameObject spell_icon;
    public GameObject spell_level_requirement;
    public GameObject assign_button;

    private Character_stats _characterStats;
    private Spell_script _spellScript;

    private void Start()
    {
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        _spellScript = GameObject.Find("Game manager").GetComponent<Spell_script>();
    }
    public void showSpell(int id)
    {
        spell_id = id;

        GameObject.Find("Spell_preview").GetComponent<Open_button_script>().Open();

        var spell = GameObject.Find("Game manager").GetComponent<Spell_script>().spells[id];
        spell_name.GetComponent<Text_animation>().startAnim(spell.name, 1f);
        spell_type.GetComponent<Text_animation>().startAnim("<color=#828282>[" + spell.type + "]</color>", 0.01f);
        spell_description.GetComponent<Text_animation>().startAnim(spell.description, 0.01f);
        spell_cost.GetComponent<Text_animation>().startAnim("<color=#2E5AB3>-" + spell.resource_cost + " resource", 0.01f);
        spell_effect.GetComponent<Text_animation>().startAnim("<color=#00FF00>+" + spell.attribute + " " + spell.attribute_type + " " + spell.attribute_name, 0.01f);

        
        StopCoroutine("Wait");
        StartCoroutine("Wait");

    }

    public void showSpell(int id, GameObject sender)
    {
        spell_id = id;

        GameObject.Find("Spell_preview_talent").GetComponent<Open_button_script>().Open();


        var spell = GameObject.Find("Game manager").GetComponent<Spell_script>().spells[id];

        spell_name.GetComponent<Text_animation>().startAnim(spell.name, 1f);
        spell_type.GetComponent<Text_animation>().startAnim("<color=#828282>[" + spell.type + "]</color>", 0.01f);
        spell_description.GetComponent<Text_animation>().startAnim(spell.description, 0.01f);
        spell_cost.GetComponent<Text_animation>().startAnim("<color=#2E5AB3>-" + spell.resource_cost + " resource", 0.01f);
        spell_effect.GetComponent<Text_animation>().startAnim("<color=#00FF00>+" + spell.attribute + " " + spell.attribute_type + " " + spell.attribute_name, 0.01f);

        spell_icon.GetComponent<SpriteRenderer>().sprite = spell.icon;

        Colors colors = new Colors();


        gameObject.GetComponent<Animator>().Play("Spell_preview_talent_slide_in_anim");
        if (spell.level_requirement > _characterStats.Local_level)
        {
            spell_level_requirement.GetComponent<TextMeshPro>().color = colors.red;
        }
        else { spell_level_requirement.GetComponent<TextMeshPro>().color = colors.white; }
        spell_level_requirement.GetComponent<Text_animation>().startAnim("requires <b>level " + spell.level_requirement.ToString(), 0.01f);

        if (sender.GetComponent<Talent_slot_script>().spell_points > 0)
        {
            assign_button.GetComponent<Visibility_script>().setVisible();
        }
        else
        {
            assign_button.GetComponent<Visibility_script>().setInvisible();
        }



        StopCoroutine("Wait");
        StartCoroutine("Wait");

    }

    public void showSpellSlotSelect()
    {
        GameObject.Find("Spell_slot_select").GetComponent<Spell_slot_select_script>().showSlotSelect(spell_id);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);

        gameObject.GetComponent<Close_button_script>().Close();

    }
}
