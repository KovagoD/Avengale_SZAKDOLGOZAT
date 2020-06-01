using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bar_script_gui : MonoBehaviour
{
    public GameObject bar;
    public TextMeshProUGUI left, right, center;
    public string mode;

    public float size;
    private float _percentage;

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

        var pos = bar.transform.position;
        pos.x = _percentage;
        bar.transform.position = pos;
    }

    public void updateBar(string left_text, string center_text, string right_text)
    {
        left.GetComponent<Text_animation>().startAnim(left_text, 1f);
        center.GetComponent<Text_animation>().startAnim(center_text, 1f);
        right.GetComponent<Text_animation>().startAnim(right_text, 0.01f);
    }

    public void updateHealth()
    {
        updateBar(_characterStats.Player_max_health + "/" + _characterStats.Player_health.ToString()
        , "",
        (((float)_characterStats.Player_health / (float)_characterStats.Player_max_health) * 100f).ToString("0") + " %");
    }

    public void updateResource()
    {
        updateBar(_characterStats.Player_max_resource + "/" + _characterStats.Player_resource.ToString()
        , "",
        (((float)_characterStats.Player_resource / (float)_characterStats.Player_max_resource) * 100f).ToString("0") + " %");
    }
}
