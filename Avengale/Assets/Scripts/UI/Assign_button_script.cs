using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assign_button_script : MonoBehaviour
{
    private Spell_preview_script _spellPreview;
    void Start()
    {
        _spellPreview = GameObject.Find("Spell_preview_talent").GetComponent<Spell_preview_script>();
    }

    void OnMouseOver()
    {

        if (Input.GetMouseButtonUp(0) && gameObject.GetComponent<SpriteRenderer>().enabled == true)
        {
            _spellPreview.showSpellSlotSelect();
        }
    }
}