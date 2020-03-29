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
    public GameObject character_button, hub_button, shop_button, spell_button, quest_button;
    public GameObject current_screen;

    public bool isNewCharacter;


    public GameObject Main_screen, Character_customization_screen, Character_screen_UI, Combat_screen;
    private Spell_script _spellScript;
    private Character_stats _characterStats;
    private Quest_manager_script _questManager;

    public GameObject Inventory, conversationWindow;

    public bool vibrationEnabled;

    void Start()
    {
        Application.targetFrameRate = 300;

        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        _questManager = gameObject.GetComponent<Quest_manager_script>();
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

        if ((Input.GetKey(KeyCode.Escape) && current_screen != Combat_screen && (GameObject.Find("Inventory_back").GetComponent<Visibility_script>().isOpened ||
        GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().isOpened ||
        GameObject.Find("Item_preview").GetComponent<Visibility_script>().isOpened ||
        GameObject.Find("Spell_preview_talent") && GameObject.Find("Spell_preview_talent").GetComponent<Visibility_script>().isOpened ||
        GameObject.Find("Conversation") && GameObject.Find("Conversation").GetComponent<Visibility_script>().isOpened ||
        GameObject.Find("Spell_slot_select") && GameObject.Find("Spell_slot_select").GetComponent<Visibility_script>().isOpened ||
        GameObject.Find("Quest_preview").GetComponent<Quest_preview_script>().isOpened
        )))
        {
            closeOpenedWindows();
            Debug.Log("0");
        }
        else if (Input.GetKey(KeyCode.Escape) && current_screen != Combat_screen && GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().isOpened)
        {
            Debug.Log("1");
            GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().closePauseMenu();
        }
        else if (Input.GetKey(KeyCode.Escape) && current_screen == Combat_screen && GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().isOpened)
        {
            Debug.Log("2");
            gameObject.GetComponent<Combat_manager_script>().resumeBattle();
            GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().closePauseMenu();
        }
        else if (Input.GetKey(KeyCode.Escape) && current_screen == Combat_screen)
        {
            Debug.Log("3");
            GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().showPauseMenu();
            gameObject.GetComponent<Combat_manager_script>().pauseBattle();
        }
        else if (Input.GetKey(KeyCode.Escape) && current_screen == Character_screen_UI)
        {
            Debug.Log("4");
            GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().showPauseMenu();
        }
        else if (Input.GetKey(KeyCode.Escape) && current_screen != Main_screen && current_screen != Character_customization_screen)
        {
            Debug.Log("5");
            Change_screen(Character_screen_UI, false);
        }
        else if (Input.GetKey(KeyCode.Escape) && current_screen == Character_customization_screen && !isNewCharacter)
        {
            Debug.Log("6");
            Change_screen(Character_screen_UI, false);
        }
        else if (Input.GetKey(KeyCode.Escape) && current_screen == Character_customization_screen && isNewCharacter)
        {
            Debug.Log("7");
            Change_screen(Main_screen, false);
        }


        if (_characterStats.Local_spell_points > 0
        && current_screen != GameObject.Find("Combat_screen_UI")
        && current_screen != GameObject.Find("Main_screen")
        && current_screen != GameObject.Find("Character_customization_screen")
        )
        {
            spell_button.GetComponent<Screen_change_button_script>().setNotification();
            //_characterStats.updateMoneyStat();
        }
        else
        {
            spell_button.GetComponent<Screen_change_button_script>().clearNotification();
        }

        if (_questManager.haveCompletedQuest()
        && current_screen != GameObject.Find("Combat_screen_UI")
        && current_screen != GameObject.Find("Main_screen")
        && current_screen != GameObject.Find("Character_customization_screen"))
        {
            quest_button.GetComponent<Screen_change_button_script>().setNotification();
        }
        else
        {
            quest_button.GetComponent<Screen_change_button_script>().clearNotification();
        }
    }

    public void loadSave()
    {
        gameObject.GetComponent<Character_stats>().loadPlayer();
        gameObject.GetComponent<Item_script>().loadItems();
        gameObject.GetComponent<Spell_script>().loadSpells();
        conversationWindow.GetComponent<Conversation_script>().initializeConversations();
    }

    public void closeOpenedWindows()
    {
        if (GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().isOpened)
        {
            GameObject.Find("Pause_menu").GetComponent<Pause_menu_script>().closePauseMenu();
        }

        if (GameObject.Find("Overlay").GetComponent<Overlay_script>().isOpen)
        {
            GameObject.Find("Overlay").GetComponent<Overlay_script>().closeOverlay();
        }

        if (Inventory.GetComponent<Inventory_ui_script>().isOpened)
        {
            Inventory.GetComponent<Inventory_ui_script>().closeInventory();
        }
        
        if (GameObject.Find("Quest_preview").GetComponent<Quest_preview_script>().isOpened)
        {
            GameObject.Find("Quest_preview").GetComponent<Quest_preview_script>().closeQuestPreview();
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

        closeOpenedWindows();


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
            //isNewCharacter = true;
        }
        else if (current_screen == GameObject.Find("Character_customization_screen"))
        {
            //GameObject.Find("Customization_controller").GetComponent<Character_customization_script>().initializeCustomization(isNewCharacter);
        }
        else if (current_screen == GameObject.Find("Character_screen_UI"))
        {
            _characterStats.updateStats();

            GameObject.Find("experience_bar").GetComponent<Bar_script>().updateXP();
            GameObject.Find("Conversation").GetComponent<Conversation_script>().initializeConversations();
            gameObject.GetComponent<Spell_script>().initializeSpells();
        }
        else if (current_screen == GameObject.Find("Quest_screen_UI"))
        {
            gameObject.GetComponent<Quest_manager_script>().updateQuestSlots();
            _characterStats.updateMoneyStat();
        }
        else if (current_screen == GameObject.Find("Store_screen_UI"))
        {
            _characterStats.updateMoneyStat();
        }
        else if (current_screen == GameObject.Find("Map_screen_UI"))
        {
            _characterStats.updateMoneyStat();
        }
        else if (current_screen == GameObject.Find("Combat_screen_UI"))
        {
            GameObject.Find("Game manager").GetComponent<Combat_manager_script>().initializeBattle(UnityEngine.Random.Range(0, 4));

            _characterStats.Local_health = _characterStats.Local_max_health;
            _characterStats.Local_resource = _characterStats.Local_max_resource;
            GameObject.Find("Spell_preview").GetComponent<Visibility_script>().setInvisible();

        }
        else if (current_screen == GameObject.Find("Spell_screen_UI"))
        {

            GameObject.Find("spellpoints_text").GetComponent<Text_animation>().startAnim("Available spellpoints: " + _characterStats.Local_spell_points, 0.05f);
            GameObject.Find("Spellbar").GetComponent<Animator>().Play("Spellbar_talent_slide_in_anim");
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
            _characterStats.updateMoneyStat();
            gameObject.GetComponent<Combat_manager_script>().stopBattle();
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}


