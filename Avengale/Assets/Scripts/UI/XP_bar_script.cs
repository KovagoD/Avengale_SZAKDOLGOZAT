using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class XP_bar_script : MonoBehaviour
{
    public GameObject bar;
    public GameObject game_manager;
    public TextMeshProUGUI xp;
    public TextMeshProUGUI level;
    public TextMeshProUGUI percentage;
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
        var scrpt = game_manager.GetComponent<Character_stats>();
        _percentage = ((float)scrpt.Local_xp / (float)scrpt.Local_needed_xp) * size;
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
        var scrpt = game_manager.GetComponent<Character_stats>();
        xp.GetComponent<Text_animation>().startAnim(scrpt.Local_xp.ToString() + "/" + scrpt.Local_needed_xp.ToString(), 1f);
        level.GetComponent<Text_animation>().startAnim("Level " + scrpt.Local_level.ToString(), 0.01f);
        percentage.GetComponent<Text_animation>().startAnim(scrpt.getPercentOfXP().ToString() + " %", 1f);
    }

    void OnMouseUp()
    {
        if (GameObject.Find("Item_preview").GetComponent<Visibility_script>().isOpened == false)
        {
            System.Random rnd = new System.Random();
            var scrpt = game_manager.GetComponent<Character_stats>();
            scrpt.getXP(rnd.Next(10, 100));
            updateXP();
        }

    }

}
