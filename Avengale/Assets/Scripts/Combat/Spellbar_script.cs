using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spellbar_script : MonoBehaviour
{
    public GameObject[] spell_slots;

    void Update()
    {
        var _currentScreen = GameObject.Find("Game manager").GetComponent<Game_manager>().current_screen.name;
        if (_currentScreen=="Combat_screen_UI" || _currentScreen=="Spell_screen_UI")
        {
            gameObject.GetComponent<Image>().enabled=true;
            foreach (var slot in spell_slots)
            {
                slot.GetComponent<Spell_slot_script>().SetEnabled();
            }
        }
        else
        {
            gameObject.GetComponent<Image>().enabled=false;
            foreach (var slot in spell_slots)
            {
                slot.GetComponent<Spell_slot_script>().SetDisabled();
            }
        }
    }
}
