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
    public GameObject current_screen;

    private Character_stats _characterStats;

    void Start()
    {
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        current_screen = GameObject.Find("Character_screen_UI");
        
        GameObject.Find("Character_screen_UI").SetActive(true);
        GameObject.Find("Store_screen_UI").SetActive(false);
        GameObject.Find("Combat_screen_UI").SetActive(false);
        GameObject.Find("Quest_screen_UI").SetActive(false);
        GameObject.Find("Map_screen_UI").SetActive(false);

    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Change_screen(screens[0]);
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
                button.GetComponent<Screen_change_button_script>().SetDisabled();
            }
        }
        else
        {
            foreach (var button in buttons)
            {
                button.GetComponent<Screen_change_button_script>().SetEnabled();
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
            GameObject.Find("Health_bar").GetComponent<Bar_script>().updateHealth();
            GameObject.Find("Resource_bar").GetComponent<Bar_script>().updateResource();
            GameObject.Find("Spell_preview").GetComponent<Visibility_script>().setInvisible();
        }

        _characterStats.updateMoneyStat();

    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}


