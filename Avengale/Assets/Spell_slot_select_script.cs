using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_slot_select_script : MonoBehaviour
{
    public GameObject[] selectable_slots;
    private Character_stats _characterStats;
    private Spell_script _spellScript;
    public int spell_id;
    void Start()
    {
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        _spellScript = GameObject.Find("Game manager").GetComponent<Spell_script>();
    }

    public void showSlotSelect(int input_spell_id)
    {
        spell_id=input_spell_id;
        gameObject.GetComponent<Animator>().Play("Slot_select_slide_in_anim");
        foreach (var slot in selectable_slots)
        {
            slot.GetComponent<SpriteRenderer>().sprite = _spellScript.spells[_characterStats.Spells[slot.GetComponent<Slot_select_script>().ID]].icon;
            //_characterStats.Spells[slot.GetComponent<Slot_select_script>().ID]
        }
    }

    public void closeSlotSelect()
    {
        gameObject.GetComponent<Animator>().Play("Slot_select_slide_out_anim");
    }

    public void chooseSlot(int ID)
    {
        _characterStats.Spells[ID] = spell_id;
    }
}
