using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add_spell_point_script : MonoBehaviour
{
    // Start is called before the first frame update
    private Character_stats _characterStats;
    public Talent_slot_script _talentSlot;
    void Start()
    {
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
    }

    void OnMouseDown()
    {
        _talentSlot.addSpellPoint();
    }

}
