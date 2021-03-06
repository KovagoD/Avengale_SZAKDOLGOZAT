﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_slot_select_script : MonoBehaviour
{
    public GameObject[] selectable_slots;
    private Character_stats _characterStats;
    private Spell_script _spellScript;
    public int spell_id;
    public bool isOpened;

    private GameObject _spellPreview;
    private GameObject _sender;
    void Start()
    {
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        _spellScript = GameObject.Find("Game manager").GetComponent<Spell_script>();
        _spellPreview = GameObject.Find("Spell_preview_talent");
    }

    public void showSlotSelect(int input_spell_id)
    {
        isOpened = true;
        spell_id = input_spell_id;
        gameObject.GetComponent<Visibility_script>().setVisible();
        gameObject.GetComponent<Animator>().Play("Slot_select_slide_in_anim");

        GameObject.Find("Overlay").GetComponent<Overlay_script>().showOverlay();

        GameObject.Find("Title").GetComponent<Text_animation>().restartAnim();

        foreach (var slot in selectable_slots)
        {
            slot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(_spellScript.spells[_characterStats.Spells[slot.GetComponent<Slot_select_script>().ID]].icon);
        }

    }

    public void closeSlotSelect()
    {
        isOpened = false;
        gameObject.GetComponent<Animator>().Play("Slot_select_slide_out_anim");
        _spellPreview.GetComponent<Animator>().Play("Spell_preview_talent_slide_out_anim");

        GameObject.Find("Overlay").GetComponent<Overlay_script>().closeOverlay();

    }

    public void chooseSlot(int ID)
    {
        for (int i = 0; i < _characterStats.Spells.Length; i++)
        {
            if (_characterStats.Spells[i] == spell_id)
            {
                _characterStats.Spells[i]=0;
            }
        }
        _characterStats.Spells[ID] = spell_id;
    }
}
