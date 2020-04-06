using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class Talent_slot_script : MonoBehaviour
{

    public GameObject spell_points_text;
    public GameObject spell_icon;
    public int ID;
    public int row;
    public int spell_id;
    public GameObject slot;
    public GameObject unavailable;
    public GameObject spell_lock;
    public GameObject addpoint;

    private Spell_script _spellScript;
    private Character_stats _characterStats;
    private Spell_slot_select_script _spellSlotSelect;

    public Sprite sprite_normal;
    public Sprite sprite_activated;

    public Sprite passive_spell_normal;
    public Sprite passive_spell_activated;


    void OnMouseOver()
    {
        if (sprite_normal != null)
        {
            if (_spellScript.spells[spell_id].type == spell_types.passive)
            {
                slot.GetComponent<SpriteRenderer>().sprite = passive_spell_activated;
            }
            else
            {
                slot.GetComponent<SpriteRenderer>().sprite = sprite_activated;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_spellScript.spells[spell_id].type == spell_types.passive)
            {
                slot.GetComponent<SpriteRenderer>().sprite = passive_spell_normal;
            }
            else
            {
                slot.GetComponent<SpriteRenderer>().sprite = sprite_normal;
            }
        }
    }

    void OnMouseExit()
    {
        if (sprite_normal != null)
        {
            if (_spellScript.spells[spell_id].type == spell_types.passive)
            {
                slot.GetComponent<SpriteRenderer>().sprite = passive_spell_normal;
            }
            else
            {
                slot.GetComponent<SpriteRenderer>().sprite = sprite_normal;
            }
        }
    }

    void Start()
    {
        _spellScript = GameObject.Find("Game manager").GetComponent<Spell_script>();
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        _spellSlotSelect = GameObject.Find("Spell_slot_select").GetComponent<Spell_slot_select_script>();
        spell_id = GameObject.Find("Game manager").GetComponent<Character_stats>().Talents[ID];

        spell_icon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(_spellScript.spells[spell_id].icon);


        spell_points_text.GetComponent<Text_animation>().startAnim(_spellScript.spells[spell_id].current_spell_points + "/" + _spellScript.spells[spell_id].max_spell_points, 0.05f);

        if (_spellScript.spells[spell_id].type == spell_types.passive)
        {
            slot.GetComponent<SpriteRenderer>().sprite = passive_spell_normal;
        }
        else
        {
            slot.GetComponent<SpriteRenderer>().sprite = sprite_normal;
        }
    }
    void Update()
    {
        var spell = _spellScript.spells[spell_id];
        if (isAvailable() && spell.current_spell_points > 0)
        {
            setEnabled();
        }
        else if (isAvailable() && spell.current_spell_points == 0)
        {
            setDisabledButLocked();
        }
        else if (!isAvailable() && spell.current_spell_points == 0)
        {
            setDisabled();
        }
        else if (!isAvailable() && spell.current_spell_points > 0)
        {
            setDisabledButUnlocked();
        }

        if (spell.current_spell_points == spell.max_spell_points)
        {
            addpoint.GetComponent<SpriteRenderer>().enabled = false;
            addpoint.GetComponent<BoxCollider2D>().enabled = false;
        }
        spell_icon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(_spellScript.spells[spell_id].icon);

    }
    public bool isAvailable()
    {
        if ((spell_id == 0) ||
        (_characterStats.Local_spell_points == 0) ||
        (row == 2 && !_spellScript.secondRowEnabled) ||
        (row == 3 && !_spellScript.thirdRowEnabled) ||
        (_spellScript.spells[spell_id].level_requirement > _characterStats.Local_level))
        {
            return false;
        }
        return true;
    }
    void OnMouseDown()
    {
        if (!_spellSlotSelect.isOpened && spell_id != 0)
        {
            GameObject.Find("Spell_preview_talent").GetComponent<Spell_preview_script>().showSpell(spell_id, gameObject);
            spell_icon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(_spellScript.spells[spell_id].icon);
        }
    }


    public void setEnabled()
    {
        unavailable.GetComponent<SpriteRenderer>().enabled = false;
        spell_lock.GetComponent<SpriteRenderer>().enabled = false;

        addpoint.GetComponent<SpriteRenderer>().enabled = true;
        addpoint.GetComponent<BoxCollider2D>().enabled = true;

    }


    public void setDisabled()
    {
        unavailable.GetComponent<SpriteRenderer>().enabled = true;
        spell_lock.GetComponent<SpriteRenderer>().enabled = true;

        addpoint.GetComponent<SpriteRenderer>().enabled = false;
        addpoint.GetComponent<BoxCollider2D>().enabled = false;
    }
    public void setDisabledButUnlocked()
    {
        unavailable.GetComponent<SpriteRenderer>().enabled = false;
        spell_lock.GetComponent<SpriteRenderer>().enabled = false;

        addpoint.GetComponent<SpriteRenderer>().enabled = false;
        addpoint.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void setDisabledButLocked()
    {
        unavailable.GetComponent<SpriteRenderer>().enabled = true;
        spell_lock.GetComponent<SpriteRenderer>().enabled = false;

        addpoint.GetComponent<SpriteRenderer>().enabled = true;
        addpoint.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void addSpellPoint()
    {
        if (_characterStats.Local_spell_points > 0 && _spellScript.spells[spell_id].current_spell_points < _spellScript.spells[spell_id].max_spell_points)
        {
            _characterStats.Local_spell_points--;
            _spellScript.spells[spell_id].current_spell_points++;

            spell_points_text.GetComponent<Text_animation>().startAnim(_spellScript.spells[spell_id].current_spell_points + "/" + _spellScript.spells[spell_id].max_spell_points, 0.05f);

            GameObject.Find("spellpoints_text").GetComponent<Text_animation>().startAnim("Available spellpoints: " + _characterStats.Local_spell_points, 0.05f);

            _spellScript.setupAttributes();

            if (_spellScript.spells[spell_id].type == spell_types.passive)
            {
                _spellScript.spells[spell_id].passiveActivate();
            }


            if (GameObject.Find("Spell_preview_talent").GetComponent<Visibility_script>().isOpened)
            {
                GameObject.Find("Spell_preview_talent").GetComponent<Spell_preview_script>().showSpell(spell_id, gameObject);
            }

            /*
            if ((!_characterStats.Spells.Contains(spell_id) && _spellScript.spells[spell_id].type != spell_types.passive) || GameObject.Find("Spell_preview_talent").GetComponent<Visibility_script>().isOpened)
            {
                GameObject.Find("Spell_preview_talent").GetComponent<Spell_preview_script>().showSpell(spell_id, gameObject);
            }
            */
        }
        _spellScript.checkRowAvailability();

    }

    public void showSpellSlotSelect()
    {
        GameObject.Find("Spell_slot_select").GetComponent<Spell_slot_select_script>().showSlotSelect(spell_id);
    }

}
