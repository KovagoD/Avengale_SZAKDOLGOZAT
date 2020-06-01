using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spell_preview_script : MonoBehaviour
{
    [Header("Spell properties")]

    public bool preview_mode;
    public int spell_id;
    public GameObject spell_name, spell_type, spell_rank, spell_description, spell_cost, spell_effect, spell_icon, spell_level_requirement, assign_button, button;
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
        spell_type.GetComponent<Text_animation>().startAnim("[" + spell.type + "]", 0.01f);
        spell_rank.GetComponent<Text_animation>().startAnim("rank " + spell.current_spell_points, 0.01f);
        spell_description.GetComponent<Text_animation>().startAnim(spell.description, 0.01f);


        gameObject.GetComponent<Animator>().Play("Spell_preview");


        string effect_text = "";
        if (spell.attribute_name == spell_attribute_types.money)
        {
            effect_text = "Effect: <color=#00FF00>+" + spell.attribute + " % more credits";
        }
        else if (spell.attribute_type == spell_attribute_value_types.percentage)
        {
            effect_text = "Effect: <color=#00FF00>+" + spell.attribute + " % " + spell.attribute_name;
        }
        else if (spell.attribute_type == spell_attribute_value_types.number)
        {
            effect_text = "Effect: <color=#00FF00>+" + spell.attribute + " " + spell.attribute_name;
        }

        spell_effect.GetComponent<Text_animation>().startAnim(effect_text, 0.01f);


        spell_cost.GetComponent<Text_animation>().startAnim("<color=#2E5AB3>-" + spell.resource_cost + " energy", 0.01f);


        StopCoroutine("Wait");
        StartCoroutine("Wait");

    }

    public void showSpell(int id, GameObject sender)
    {
        spell_id = id;

        GameObject.Find("Spell_preview_talent").GetComponent<Open_button_script>().Open();


        var spell = GameObject.Find("Game manager").GetComponent<Spell_script>().spells[id];

        spell_name.GetComponent<Text_animation>().startAnim(spell.name, 1f);
        spell_type.GetComponent<Text_animation>().startAnim("[" + spell.type + "]", 0.01f);
        spell_rank.GetComponent<Text_animation>().startAnim("rank " + spell.current_spell_points, 0.01f);
        spell_description.GetComponent<Text_animation>().startAnim(spell.description, 0.01f);

        if (spell.type != spell_types.passive)
        {
            spell_cost.GetComponent<Visibility_script>().setVisible();
            spell_cost.GetComponent<Text_animation>().startAnim("Cost: <color=#2E5AB3>-" + spell.resource_cost + " energy", 0.01f);
        }
        else
        {
            spell_cost.GetComponent<Visibility_script>().setInvisible();
        }



        string effect_text = "";
        if (spell.attribute_name == spell_attribute_types.money)
        {
            effect_text = "Effect: <color=#00FF00>+" + spell.attribute + " % more credits";
        }
        else if (spell.attribute_name == spell_attribute_types.penalty)
        {
            effect_text = "Effect: <color=#00FF00>" + spell.attribute + " % less penalty from dying";
        }
        else if (spell.attribute_type == spell_attribute_value_types.percentage)
        {
            effect_text = "Effect: <color=#00FF00>+" + spell.attribute + " % " + spell.attribute_name;
        }
        else if (spell.attribute_type == spell_attribute_value_types.number)
        {
            effect_text = "Effect: <color=#00FF00>+" + spell.attribute + " " + spell.attribute_name;
        }

        spell_effect.GetComponent<Text_animation>().startAnim(effect_text, 0.01f);


        spell_icon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spell.icon);

        Colors colors = new Colors();


        gameObject.GetComponent<Animator>().Play("Spell_preview_talent_slide_in_anim");
        if (spell.level_requirement > _characterStats.Player_level)
        {
            spell_level_requirement.GetComponent<TextMeshPro>().color = colors.red;
        }
        else { spell_level_requirement.GetComponent<TextMeshPro>().color = colors.white; }
        spell_level_requirement.GetComponent<Text_animation>().startAnim("requires <b>level " + spell.level_requirement.ToString(), 0.01f);

        if (spell.current_spell_points > 0 && spell.type != spell_types.passive)
        {
            assign_button.GetComponent<Visibility_script>().setVisible();
            button.GetComponentInChildren<BoxCollider2D>().enabled = true;
        }
        else
        {
            assign_button.GetComponent<Visibility_script>().setInvisible();
            button.GetComponentInChildren<BoxCollider2D>().enabled = false;

        }

        /*
        StopCoroutine("Wait");
        StartCoroutine("Wait");
        */

    }

    public void closeSpellPreview()
    {
        if (preview_mode)
        {
            gameObject.GetComponent<Animator>().Play("Spell_preview_talent_slide_out_anim");
        }
        else
        {
            gameObject.GetComponent<Animator>().Play("Spell_preview_out");
        }
    }

    public void showSpellSlotSelect()
    {
        GameObject.Find("Spell_slot_select").GetComponent<Spell_slot_select_script>().showSlotSelect(spell_id);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        closeSpellPreview();
        //gameObject.GetComponent<Close_button_script>().Close();

    }
}
