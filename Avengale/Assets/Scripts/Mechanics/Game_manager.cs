using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Game_manager : MonoBehaviour
{
    [Header("References")]
    public GameObject[] screens;
    public GameObject[] buttons;
    public GameObject spell_button;
    public GameObject current_screen;

    public bool isNewCharacter;


    public GameObject Main_screen, Character_customization_screen, Character_screen_UI, Combat_screen;
    private Spell_script _spellScript;
    private Character_stats _characterStats;

    public bool vibrationEnabled;

    void Start()
    {
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        _spellScript = gameObject.GetComponent<Spell_script>();

        current_screen = GameObject.Find("Main_screen");

        GameObject.Find("Main_screen").SetActive(true);

        GameObject.Find("Character_customization_screen").SetActive(false);
        GameObject.Find("Character_screen_UI").SetActive(false);
        GameObject.Find("Store_screen_UI").SetActive(false);
        GameObject.Find("Combat_screen_UI").SetActive(false);
        GameObject.Find("Quest_screen_UI").SetActive(false);
        GameObject.Find("Map_screen_UI").SetActive(false);
        GameObject.Find("Spell_screen_UI").SetActive(false);

        navigationBarVisibility();


    }
    void Update()
    {


        if (Input.GetKey(KeyCode.Escape) && current_screen != Combat_screen && GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().isOpen)
        {
            GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().closePauseMenu();
        }
        else if (Input.GetKey(KeyCode.Escape) && current_screen == Combat_screen && GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().isOpen)
        {
            GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().closePauseMenu();
            gameObject.GetComponent<Combat_manager_script>().resumeBattle();
        }
        else if (Input.GetKey(KeyCode.Escape) && current_screen == Combat_screen)
        {
            GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().showPauseMenu();
            gameObject.GetComponent<Combat_manager_script>().pauseBattle();
        }

        else if (Input.GetKey(KeyCode.Escape) && current_screen == Character_screen_UI)
        {
            GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().showPauseMenu();
        }
        else if (Input.GetKey(KeyCode.Escape) && current_screen != Main_screen && current_screen != Character_customization_screen)
        {
            Change_screen(Character_screen_UI, false);
        }
        else if (Input.GetKey(KeyCode.Escape) && current_screen == Character_customization_screen)
        {
            Change_screen(Main_screen, false);
        }

        if (_characterStats.Local_spell_points > 0
        && current_screen != GameObject.Find("Combat_screen_UI")
        && current_screen != GameObject.Find("Main_screen")
        && current_screen != GameObject.Find("Character_customization_screen")
        )
        {
            spell_button.GetComponent<Screen_change_button_script>().setNotification();
            _characterStats.updateMoneyStat();
        }
        else
        {
            spell_button.GetComponent<Screen_change_button_script>().clearNotification();
        }




    }

    public void loadSave()
    {

        gameObject.GetComponent<Character_stats>().loadPlayer();
        gameObject.GetComponent<Item_script>().loadItems();
        gameObject.GetComponent<Spell_script>().loadSpells();

        gameObject.GetComponent<Conversation_script>().initializeConversations();


    }

    public void Change_screen(GameObject target, bool isMenu)
    {
        current_screen.SetActive(true);

        if (!isMenu)
        {
            if (current_screen.GetComponent<Screen_script>().order < target.GetComponent<Screen_script>().order)
            {
                current_screen.GetComponent<Animator>().Play("Screen_to_inactive_left_anim");
            }

            else if (current_screen.GetComponent<Screen_script>().order > target.GetComponent<Screen_script>().order)
            {
                current_screen.GetComponent<Animator>().Play("Screen_to_inactive_right_anim");
            }

        }

        if (GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().isOpen)
        {
            GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().closePauseMenu();
        }

        if (GameObject.Find("Overlay").GetComponent<Overlay_script>().isOpen)
        {
            GameObject.Find("Overlay").GetComponent<Overlay_script>().closeOverlay();
        }

        if (GameObject.Find("Inventory_back").GetComponent<Visibility_script>().isOpened)
        {
            GameObject.Find("Inventory slots").GetComponent<Animator>().Play("Inventory_slide_out_anim");
        }

        if (GameObject.Find("Item_preview").GetComponent<Visibility_script>().isOpened)
        {
            GameObject.Find("Item_preview").GetComponent<Animator>().Play("Item_preview_slide_out_anim");
        }


        if (GameObject.Find("Spell_preview_talent") && GameObject.Find("Spell_preview_talent").GetComponent<Visibility_script>().isOpened)
        {
            GameObject.Find("Spell_preview_talent").GetComponent<Spell_preview_script>().closeSpellPreview();

        }


        if (GameObject.Find("Spell_slot_select") && GameObject.Find("Spell_slot_select").GetComponent<Visibility_script>().isOpened)
        {
            GameObject.Find("Spell_slot_select").GetComponent<Spell_slot_select_script>().closeSlotSelect();
        }

        if (GameObject.Find("Conversation") && GameObject.Find("Conversation").GetComponent<Visibility_script>().isOpened)
        {
            GameObject.Find("Conversation").GetComponent<Conversation_script>().closeConversation();
        }

        //GameObject.Find("Overlay").GetComponent<Animator>().Play("Close_overlay");








        target.SetActive(true);

        if (isMenu)
        {
            current_screen.GetComponent<Animator>().Play("Screen_to_inactive_left_anim");
            target.GetComponent<Animator>().Play("Screen_to_active_right_anim");
        }

        if (current_screen.GetComponent<Screen_script>().order < target.GetComponent<Screen_script>().order)
        {
            target.GetComponent<Animator>().Play("Screen_to_active_left_anim");
        }
        else if (current_screen.GetComponent<Screen_script>().order > target.GetComponent<Screen_script>().order)
        {
            target.GetComponent<Animator>().Play("Screen_to_active_right_anim");
        }

        current_screen = target;

        navigationBarVisibility();



        if (current_screen == GameObject.Find("Main_screen"))
        {
            isNewCharacter = true;

        }

        if (current_screen == GameObject.Find("Character_customization_screen"))
        {
            //GameObject.Find("Customization_controller").GetComponent<Character_customization_script>().initializeCustomization(isNewCharacter);
        }

        if (current_screen == GameObject.Find("Character_screen_UI"))
        {
            GameObject.Find("XP_bar").GetComponent<Bar_script>().updateXP();
            _characterStats.updateStats();
        }

        if (current_screen == GameObject.Find("Quest_screen_UI"))
        {
            for (int i = 0; i < _characterStats.accepted_quests.Length; i++)
            {
                GameObject.Find("Game manager").GetComponent<Quest_manager_script>().updateQuestSlot(i);
            }
            _characterStats.updateMoneyStat();
        }



        if (current_screen == GameObject.Find("Combat_screen_UI"))
        {
            GameObject.Find("Game manager").GetComponent<Combat_manager_script>().initializeBattle(UnityEngine.Random.Range(0, 3));

            _characterStats.Local_health = _characterStats.Local_max_health;
            _characterStats.Local_resource = _characterStats.Local_max_resource;
            GameObject.Find("Spell_preview").GetComponent<Visibility_script>().setInvisible();

        }

        if (current_screen == GameObject.Find("Spell_screen_UI"))
        {

            GameObject.Find("spellpoints_text").GetComponent<Text_animation>().startAnim("Available spellpoints: " + _characterStats.Local_spell_points, 0.05f);

            _spellScript.setupAttributes();
            _spellScript.checkRowAvailability();



            ArrayList slots = new ArrayList();
            slots.AddRange(_spellScript.firstRow);
            slots.AddRange(_spellScript.secondRow);
            slots.AddRange(_spellScript.thirdRow);


            foreach (GameObject slot in slots)
            {
                slot.GetComponent<Talent_slot_script>().spell_points_text.GetComponent<Text_animation>().startAnim(_spellScript.spells[slot.GetComponent<Talent_slot_script>().spell_id].current_spell_points + "/" + _spellScript.spells[slot.GetComponent<Talent_slot_script>().spell_id].max_spell_points, 0.05f);
            }

            _characterStats.updateMoneyStat();
        }

        Text_animation[] allText = target.GetComponentsInChildren<Text_animation>();
        foreach (Text_animation child in allText)
        {
            child.restartAnim();
        }
    }

    public void navigationBarVisibility()
    {
        if (current_screen == GameObject.Find("Combat_screen_UI")
        || current_screen == GameObject.Find("Main_screen")
        || current_screen == GameObject.Find("Character_customization_screen"))
        {
            foreach (var button in buttons)
            {
                button.GetComponent<Visibility_script>().setInvisible();
            }
            GameObject.Find("nav_button_background").GetComponent<Visibility_script>().setInvisible();
            GameObject.Find("credit text").GetComponent<TextMeshPro>().enabled = false;
        }
        else
        {
            foreach (var button in buttons)
            {
                button.GetComponent<Visibility_script>().setVisible();
                GameObject.Find("nav_button_background").GetComponent<Visibility_script>().setVisible();
            }
            GameObject.Find("credit text").GetComponent<TextMeshPro>().enabled = true;
            gameObject.GetComponent<Combat_manager_script>().stopBattle();
        }
    }
     public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}


