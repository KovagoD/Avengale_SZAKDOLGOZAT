using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talent_slot_script : MonoBehaviour
{

    public GameObject spell_points_text;
    public GameObject spell_icon;
    public int ID;
    public int row;
    public int spell_id;
    public int spell_points = 0;
    public int max_spell_points = 5;

    public GameObject slot;
    public GameObject unavailable;
    public GameObject addpoint;

    private Spell_script _spellScript;
    private Character_stats _characterStats;

    void Start()
    {
        _spellScript = GameObject.Find("Game manager").GetComponent<Spell_script>();
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        spell_id = GameObject.Find("Game manager").GetComponent<Character_stats>().Talents[ID];
        spell_icon.GetComponent<SpriteRenderer>().sprite = _spellScript.spells[spell_id].icon;

        setEnabled();
        if (row==2)
        {
            setDisabled();
        }

    }
    void Update()
    {
        //if (row == 2 && !_spellScript.secondRowEnabled)
        if (_characterStats.Local_spell_points == 0)
        {
            setDisabled();
        }


    }
    void OnMouseDown()
    {
        GameObject.Find("Spell_preview").GetComponent<Spell_preview_script>().showSpell(spell_id);
    }

    public void setEnabled()
    {
        unavailable.GetComponent<SpriteRenderer>().enabled = false;
        addpoint.GetComponent<SpriteRenderer>().enabled = true;

    }

    public void setDisabled()
    {
        unavailable.GetComponent<SpriteRenderer>().enabled = true;
        addpoint.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void addSpellPoint()
    {
        if (_characterStats.Local_spell_points > 0 && spell_points < max_spell_points)
        {
            _characterStats.Local_spell_points--;

            spell_points++;
            spell_points_text.GetComponent<Text_animation>().startAnim(spell_points.ToString(), 0.5f);
            Debug.Log(_characterStats.Local_spell_points);
            GameObject.Find("spellpoints_text").GetComponent<Text_animation>().startAnim("Available spellpoints: " + _characterStats.Local_spell_points, 0.05f);

        }



        /*
        if (row == 2 && _spellScript.secondRowEnabled && _characterStats.Local_spell_points > 0)
        {
            setEnabled();
        }
        else { setDisabled(); }
        */
    }

    public void showSpellSlotSelect()
    {
        GameObject.Find("Spell_slot_select").GetComponent<Spell_slot_select_script>().showSlotSelect(spell_id, gameObject);
    }

}
