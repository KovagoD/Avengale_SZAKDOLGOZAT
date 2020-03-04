using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talent_slot_script : MonoBehaviour
{
    
    public GameObject spell_points_text;
    public GameObject spell_icon;
    public int ID;
    public int spell_id;
    public int spell_points=0;
    public int max_spell_points=5;

    private Spell_script _spellScript;
    private Character_stats _characterStats;

    void Start()
    {
        _spellScript= GameObject.Find("Game manager").GetComponent<Spell_script>();
        _characterStats=GameObject.Find("Game manager").GetComponent<Character_stats>();


        spell_id = GameObject.Find("Game manager").GetComponent<Character_stats>().Talents[ID];
        spell_icon.GetComponent<SpriteRenderer>().sprite = _spellScript.spells[spell_id].icon;
    }

     void OnMouseDown()
    {
        addSpellPoint();
    }

    public void addSpellPoint()
    {
        if (spell_points<max_spell_points)
        {
        spell_points++;
        spell_points_text.GetComponent<Text_animation>().startAnim(spell_points.ToString(), 0.5f);
        //GameObject.Find("Spell_preview").GetComponent<Spell_preview_script>().showSpell(spell_id);
        }


        if (spell_points>0)
        {
            //_characterStats.Spells[0]=spell_id;
            Debug.Log("Spell set");
        }

        GameObject.Find("Spell_slot_select").GetComponent<Spell_slot_select_script>().showSlotSelect(spell_id);
    }
}
