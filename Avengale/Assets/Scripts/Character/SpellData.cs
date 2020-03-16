using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpellData
{
    public List<Spell> spells = new List<Spell>();
    public SpellData(Spell_script spellScript)
    {
        spells = spellScript.spells;
    }
}
