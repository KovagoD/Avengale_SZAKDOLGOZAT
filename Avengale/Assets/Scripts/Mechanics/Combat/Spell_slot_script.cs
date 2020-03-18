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

    private Spell_script _spellScript;
    private Combat_manager_script _combatManager;
    private Ingame_notification_script _notification;
    private Game_manager _gameManager;
    void Start()
    {
        _gameManager = GameObject.Find("Game manager").GetComponent<Game_manager>();
        _combatManager = GameObject.Find("Game manager").GetComponent<Combat_manager_script>();
        _notification = GameObject.Find("Notification").GetComponent<Ingame_notification_script>();
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        _spellScript = GameObject.Find("Game manager").GetComponent<Spell_script>();
    }

    private void Update()
    {
        spell_id = _characterStats.Spells[id];
        spell = _spellScript.spells[spell_id];
        spell_slot.GetComponent<Image>().sprite = Resources.Load<Sprite>(spell.icon);
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
        slot.GetComponent<Image>().sprite = slot_sprite_activated;
        if (spell_id != 0)
        {
            GameObject.Find("Spell_preview").GetComponent<Spell_preview_script>().showSpell(spell_id);
        }
    }
    void OnMouseUp()
    {

        slot.GetComponent<Image>().sprite = slot_sprite;
        if (!_combatManager.isPaused && _gameManager.current_screen.name == "Combat_screen_UI" && spell_id != 0)
        {
            GameObject.Find("Spell_preview").GetComponent<Close_button_script>().Close();

            if (_combatManager.getRound() == battleRound.Player)
            {
                slot.GetComponent<Image>().sprite = slot_sprite_activated;
                if ((spell.resource_cost <= _characterStats.Local_resource))
                {

                    spell.Activate(_spellScript.target);
                    _combatManager.changeRound();

                    GameObject.Find("Health_bar").GetComponent<Bar_script>().updateHealth();
                    GameObject.Find("Resource_bar").GetComponent<Bar_script>().updateResource();
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
