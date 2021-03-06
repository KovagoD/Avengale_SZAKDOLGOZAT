﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bar_script : MonoBehaviour
{
    public GameObject bar;
    public TextMeshProUGUI left, right, center;
    public string mode;
    public float size, _percentage;
    static Character_stats _characterStats;
    void Start()
    {
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();

        Camera cam = Camera.main;
        size = (cam.aspect * 2f) * 10f;
        if (mode == "health")
        {
            updateHealth();
        }
        else if (mode == "resource")
        {
            updateResource();
        }
    }
    void Update()
    {
        if (mode == "health")
        {
            _percentage = ((float)_characterStats.Player_health / (float)_characterStats.Player_max_health) * size;
        }
        else if (mode == "resource")
        {
            _percentage = ((float)_characterStats.Player_resource / (float)_characterStats.Player_max_resource) * size;
        }
        else if (mode == "xp")
        {
            _percentage = ((float)_characterStats.Player_xp / (float)_characterStats.Player_needed_xp) * size;
        }

        var pos = bar.transform.position;
        gameObject.GetComponent<Animator>().enabled = true;

        if (Math.Round(pos.x) == Math.Round(_percentage))
        {
            gameObject.GetComponent<Animator>().enabled = false;
            pos.x = _percentage;
            bar.transform.position = pos;
        }
    }

    public void updateBar(string left_text, string center_text, string right_text)
    {
        left.GetComponent<Text_animation>().startAnim(left_text, 1f);
        center.GetComponent<Text_animation>().startAnim(center_text, 1f);
        right.GetComponent<Text_animation>().startAnim(right_text, 0.01f);
    }

    public void updateHealthAddition()
    {
        gameObject.GetComponent<Animator>().Play("Bar_init");
        updateBar(_characterStats.Player_max_health + "/" + _characterStats.Player_health.ToString()
        , "health",
        (((float)_characterStats.Player_health / (float)_characterStats.Player_max_health) * 100f).ToString("0") + " %");
    }
    public void updateHealth()
    {
        gameObject.GetComponent<Animator>().Play("Bar_init_reverse");
        updateBar(_characterStats.Player_max_health + "/" + _characterStats.Player_health.ToString()
        , "health",
        (((float)_characterStats.Player_health / (float)_characterStats.Player_max_health) * 100f).ToString("0") + " %");
    }
    public void updateResourceAddition()
    {
        gameObject.GetComponent<Animator>().Play("Bar_init");
        updateBar(_characterStats.Player_max_resource + "/" + _characterStats.Player_resource.ToString()
        , "resource",
        (((float)_characterStats.Player_resource / (float)_characterStats.Player_max_resource) * 100f).ToString("0") + " %");
    }
    public void updateResource()
    {
        gameObject.GetComponent<Animator>().Play("Bar_init_reverse");
        updateBar(_characterStats.Player_max_resource + "/" + _characterStats.Player_resource.ToString()
        , "resource",
        (((float)_characterStats.Player_resource / (float)_characterStats.Player_max_resource) * 100f).ToString("0") + " %");
    }
    public void updateXP()
    {
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        if (_characterStats.Player_xp > 0)
        {
            gameObject.GetComponent<Animator>().Play("Bar_init");
        }
        else
        {
            gameObject.GetComponent<Animator>().Play("Bar_init_reverse");
        }
        updateBar("Level " + _characterStats.Player_level.ToString()
        , _characterStats.Player_xp + "/" + _characterStats.Player_needed_xp.ToString(),
        _characterStats.getPercentOfXP().ToString() + " %");
        
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