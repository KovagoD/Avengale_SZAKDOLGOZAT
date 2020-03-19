using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class XP_bar_script : MonoBehaviour
{
    public GameObject bar;
    public GameObject game_manager;
    public TextMeshProUGUI xp, level, percentage;
    public Character_stats _characterStats;


    public float size;
    private float _percentage;

    void Start()
    {
        Camera cam = Camera.main;
        size = (cam.aspect * 2f) * 10f;
        updateXP();
    }

    void Update()
    {
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();

        _percentage = ((float)_characterStats.Local_xp / (float)_characterStats.Local_needed_xp) * size;
        var pos = bar.transform.position;
        pos.x = _percentage;
        bar.transform.position = pos;

        if (GameObject.Find("Item_preview").GetComponent<Visibility_script>().isOpened)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else { gameObject.GetComponent<BoxCollider2D>().enabled = true; }
    }

    public void updateXP()
    {
        xp.GetComponent<Text_animation>().startAnim(_characterStats.Local_xp.ToString() + "/" + _characterStats.Local_needed_xp.ToString(), 1f);
        level.GetComponent<Text_animation>().startAnim("Level " + _characterStats.Local_level.ToString(), 0.01f);
        percentage.GetComponent<Text_animation>().startAnim(_characterStats.getPercentOfXP().ToString() + " %", 1f);
    }

    void OnMouseUp()
    {
        if (GameObject.Find("Item_preview").GetComponent<Visibility_script>().isOpened == false)
        {
            System.Random rnd = new System.Random();
            _characterStats.getXP(rnd.Next(10, 100));
            updateXP();
        }

    }

}
