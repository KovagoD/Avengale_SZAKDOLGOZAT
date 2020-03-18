using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_select_script : MonoBehaviour
{
    // Start is called before the first frame update
    public int ID;
    public GameObject slot_select;

    void OnMouseDown()
    {
        selectSlot();
    }

    public void selectSlot()
    {
        slot_select.GetComponent<Spell_slot_select_script>().chooseSlot(ID);
        slot_select.GetComponent<Spell_slot_select_script>().closeSlotSelect();        
    }
}
