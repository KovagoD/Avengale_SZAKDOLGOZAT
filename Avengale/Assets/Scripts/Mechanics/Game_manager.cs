using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Game_manager : MonoBehaviour
{
    [Header("References")]
    public GameObject[] screens;
    public GameObject[] buttons;
    public GameObject spell_button;
    public GameObject current_screen;

    private Spell_script _spellScript;
    private Character_stats _characterStats;

    void Start()
    {
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        current_screen = GameObject.Find("Character_screen_UI");
        _spellScript = gameObject.GetComponent<Spell_script>();

        GameObject.Find("Character_screen_UI").SetActive(true);
        GameObject.Find("Store_screen_UI").SetActive(false);
        GameObject.Find("Combat_screen_UI").SetActive(false);
        GameObject.Find("Quest_screen_UI").SetActive(false);
        GameObject.Find("Map_screen_UI").SetActive(false);
        GameObject.Find("Spell_screen_UI").SetActive(false);


    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Change_screen(screens[0]);
        }

        if (_characterStats.Local_spell_points > 0)
        {
            spell_button.GetComponent<Screen_change_button_script>().setNotification();
        }
        else
        {
            spell_button.GetComponent<Screen_change_button_script>().clearNotification();
        }


    }
    public void Change_screen(GameObject target)
    {
        //---------------- current_screen.SetActive(false);

        if (current_screen.GetComponent<Screen_script>().order < target.GetComponent<Screen_script>().order)
        {
            current_screen.GetComponent<Animator>().Play("Screen_to_inactive_left_anim");
        }
        else if (current_screen.GetComponent<Screen_script>().order > target.GetComponent<Screen_script>().order)
        {
            current_screen.GetComponent<Animator>().Play("Screen_to_inactive_right_anim");
        }
        /*
        if (current_screen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Screen_to_inactive"))
        {
            current_screen.SetActive(false);
        }
        */
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
            GameObject.Find("Spell_preview_talent").GetComponent<Animator>().Play("Spell_preview_talent_slide_out_anim");
        }


        if (GameObject.Find("Spell_slot_select") && GameObject.Find("Spell_slot_select").GetComponent<Visibility_script>().isOpened)
        {
            GameObject.Find("Spell_slot_select").GetComponent<Animator>().Play("Slot_select_slide_out_anim");
        }







        target.SetActive(true);

        if (current_screen.GetComponent<Screen_script>().order < target.GetComponent<Screen_script>().order)
        {
            target.GetComponent<Animator>().Play("Screen_to_active_left_anim");
        }
        else if (current_screen.GetComponent<Screen_script>().order > target.GetComponent<Screen_script>().order)
        {
            target.GetComponent<Animator>().Play("Screen_to_active_right_anim");
        }

        //target.GetComponent<Animator>().Play("Screen_to_active_right_anim");
        current_screen = target;

        if (current_screen == GameObject.Find("Combat_screen_UI"))
        {
            foreach (var button in buttons)
            {
                button.GetComponent<Visibility_script>().setInvisible();
                GameObject.Find("nav_button_background").GetComponent<Visibility_script>().setInvisible();
            }
        }
        else
        {
            foreach (var button in buttons)
            {
                button.GetComponent<Visibility_script>().setVisible();
                GameObject.Find("nav_button_background").GetComponent<Visibility_script>().setVisible();
            }
            gameObject.GetComponent<Combat_manager_script>().stopBattle();
        }

        if (current_screen == GameObject.Find("Character_screen_UI"))
        {
            _characterStats.updateStats();
            GameObject.Find("XP_bar").GetComponent<Bar_script>().updateXP();
        }

        if (current_screen == GameObject.Find("Quest_screen_UI"))
        {
            for (int i = 0; i < _characterStats.accepted_quests.Length; i++)
            {
                GameObject.Find("Game manager").GetComponent<Quest_manager_script>().updateQuestSlot(i);
            }
            GameObject.Find("Quest_text").GetComponent<Text_animation>().restartAnim();
        }

        if (current_screen == GameObject.Find("Store_screen_UI"))
        {
            GameObject.Find("Store_text").GetComponent<Text_animation>().restartAnim();
        }

        if (current_screen == GameObject.Find("Combat_screen_UI"))
        {
            GameObject.Find("Game manager").GetComponent<Combat_manager_script>().initializeBattle(Random.Range(0, 3));

            _characterStats.Local_health = _characterStats.Local_max_health;
            _characterStats.Local_resource = _characterStats.Local_max_resource;
            GameObject.Find("Spell_preview").GetComponent<Visibility_script>().setInvisible();
        }

        if (current_screen == GameObject.Find("Spell_screen_UI"))
        {
            GameObject.Find("spellpoints_text").GetComponent<Text_animation>().startAnim("Available spellpoints: " + _characterStats.Local_spell_points, 0.05f);

            /*
            GameObject.Find("row_2_unlock_text").GetComponent<Text_animation>().startAnim("Spend " + _spellScript.neededForSecond() + " to unlock these: ", 0.05f);
            GameObject.Find("row_3_unlock_text").GetComponent<Text_animation>().startAnim("Spend " + _spellScript.neededForThird() + " to unlock these: ", 0.05f);
            */


            _spellScript.setupAttributes();
            _spellScript.checkRowAvailability();

            foreach (var slot in _spellScript.firstRow)
            {
                slot.GetComponent<Talent_slot_script>().spell_points_text.GetComponent<Text_animation>().startAnim(_spellScript.spells[slot.GetComponent<Talent_slot_script>().spell_id].current_spell_points + "/" + _spellScript.spells[slot.GetComponent<Talent_slot_script>().spell_id].max_spell_points, 0.05f);
            }
            foreach (var slot in _spellScript.secondRow)
            {
                slot.GetComponent<Talent_slot_script>().spell_points_text.GetComponent<Text_animation>().startAnim(_spellScript.spells[slot.GetComponent<Talent_slot_script>().spell_id].current_spell_points + "/" + _spellScript.spells[slot.GetComponent<Talent_slot_script>().spell_id].max_spell_points, 0.05f);
            }

        }



        _characterStats.updateMoneyStat();

    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}


