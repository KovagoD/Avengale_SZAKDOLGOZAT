using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell_slot_script : MonoBehaviour
{
    public int id = 0;
    public int spell_id = 0;
    public Sprite slot_sprite;
    public Sprite slot_sprite_activated;


    public GameObject slot;
    public GameObject spell_slot;

    private Spell spell;
    private Character_stats _characterStats;
    private Combat_manager_script _combatManager;
    private Ingame_notification_script _notification;
    void Start()
    {
        _combatManager = GameObject.Find("Game manager").GetComponent<Combat_manager_script>();
        _notification = GameObject.Find("Notification").GetComponent<Ingame_notification_script>();
        spell_id = GameObject.Find("Game manager").GetComponent<Character_stats>().Spells[id];
        spell = GameObject.Find("Game manager").GetComponent<Spell_script>().spells[spell_id];
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        spell_slot.GetComponent<Image>().sprite = spell.icon;
    }
    public void SetEnabled()
    {
        slot.GetComponent<Image>().enabled = true;
        spell_slot.GetComponent<Image>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void SetDisabled()
    {
        slot.GetComponent<Image>().enabled = false;
        spell_slot.GetComponent<Image>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
    void OnMouseDown()
    {
        GameObject.Find("Spell_preview").GetComponent<Spell_preview_script>().showSpell(spell_id);
    }
    void OnMouseUp()
    {
        GameObject.Find("Spell_preview").GetComponent<Close_button_script>().Close();

        if (!_combatManager.isPaused)
        {
            if (_combatManager.getRound() == "Player")
            {
                slot.GetComponent<Image>().sprite = slot_sprite_activated;
                //Debug.Log(spell.resource_cost + " " + _characterStats.Local_resource);

                if ((spell.resource_cost <= _characterStats.Local_resource))
                {
                    if (GameObject.Find("Game manager").GetComponent<Spell_script>().target != null)
                    {
                        spell.Activate(GameObject.Find("Game manager").GetComponent<Spell_script>().target);

                        GameObject.Find("Health_bar").GetComponent<Bar_script>().updateHealth();
                        GameObject.Find("Resource_bar").GetComponent<Bar_script>().updateResource();
                        _combatManager.changeRound();
                    }
                    else
                    {
                        _notification.message("You need to select a target first!", 3, "red");
                    }
                }
                else
                {
                    _notification.message("You don't have enough resource to use <b>that!", 3, "red");
                }
            }
            else
            {
                _notification.message("it's not your turn!", 3, "red");
            }
        }
    }

    void OnMouseExit()
    {
        slot.GetComponent<Image>().sprite = slot_sprite;
    }

}
