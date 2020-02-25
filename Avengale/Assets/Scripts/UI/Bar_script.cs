using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bar_script : MonoBehaviour
{
    public GameObject bar;
    static GameObject game_manager;
    public TextMeshProUGUI left;
    public TextMeshProUGUI right;
    public TextMeshProUGUI center;

    public string mode;
    public float size;
    private float _percentage;

    static Character_stats local;
    void Start()
    {
        game_manager=GameObject.Find("Game manager");
        Camera cam = Camera.main;
        size = (cam.aspect  * 2f)*10f ;

        local=game_manager.GetComponent<Character_stats>();

        if (mode=="health")
        {
            updateHealth();
        }
        else if (mode=="resource")
        {
            updateResource();
        }
    }
    
    void Update()
    {
        var scrpt = game_manager.GetComponent<Character_stats>();

        if (mode=="health")
        {
            _percentage = ((float)scrpt.Local_health / (float)scrpt.Local_max_health) * size;
        }
        else if (mode=="resource")
        {
            _percentage = ((float)scrpt.Local_resource / (float)scrpt.Local_max_resource) * size;
        }
        else if (mode=="xp")
        {
            _percentage = ((float)scrpt.Local_xp / (float)scrpt.Local_needed_xp) * size;
        }
        /*
        if (GameObject.Find("Character_screen_UI").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            var pos = bar.transform.position;
            pos.x = _percentage;
            bar.transform.position = pos;

            if (Math.Round(pos.x) == Math.Round(_percentage))
            {

                && mode=="xp"
        */
        
        var pos = bar.transform.position;
        /*
        if (_percentage != 0 )
        {
             gameObject.GetComponent<Animator>().enabled=true;
        }
        */


        gameObject.GetComponent<Animator>().enabled=true;
        
        if (Math.Round(pos.x) == Math.Round(_percentage))
        {
            //Debug.Log(_percentage+" "+pos.x);
            //Debug.Log(Math.Round(pos.x,2)+" "+Math.Round(_percentage,2));
            gameObject.GetComponent<Animator>().enabled=false;
            pos.x = _percentage;
            bar.transform.position = pos;
        }
        
        if (GameObject.Find("Item_preview").GetComponent<Visibility_script>().isOpened)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else { gameObject.GetComponent<BoxCollider2D>().enabled = true; }
    }

    public void updateBar(string left_text, string center_text, string right_text)
    {
        left.GetComponent<Text_animation>().startAnim(left_text,1f);
        center.GetComponent<Text_animation>().startAnim(center_text,1f);   
        right.GetComponent<Text_animation>().startAnim(right_text,0.01f);
    }

    public void updateHealth()
    {
        //gameObject.GetComponent<Animator>().Play("Bar_init_reverse");
        updateBar(GameObject.Find("Game manager").GetComponent<Character_stats>().Local_max_health+"/"+GameObject.Find("Game manager").GetComponent<Character_stats>().Local_health.ToString()
        ,"",
        (((float)GameObject.Find("Game manager").GetComponent<Character_stats>().Local_health/(float)GameObject.Find("Game manager").GetComponent<Character_stats>().Local_max_health)*100f).ToString("0")+" %");
    }
    
    public void updateResource()
    {
        //gameObject.GetComponent<Animator>().Play("Bar_init_reverse");
        //right.GetComponent<TextMeshPro>().color=game_manager.GetComponent<Item_script>().blue;
        updateBar(GameObject.Find("Game manager").GetComponent<Character_stats>().Local_max_resource+"/"+GameObject.Find("Game manager").GetComponent<Character_stats>().Local_resource.ToString()
        ,"",
        (((float)GameObject.Find("Game manager").GetComponent<Character_stats>().Local_resource/(float)GameObject.Find("Game manager").GetComponent<Character_stats>().Local_max_resource)*100f).ToString("0")+" %");
    }

    public void updateXP()
    {
        //gameObject.GetComponent<Animator>().Play("Bar_init");
        updateBar("Level " + GameObject.Find("Game manager").GetComponent<Character_stats>().Local_level.ToString()
        ,GameObject.Find("Game manager").GetComponent<Character_stats>().Local_xp+"/"+GameObject.Find("Game manager").GetComponent<Character_stats>().Local_needed_xp.ToString(),
        GameObject.Find("Game manager").GetComponent<Character_stats>().getPercentOfXP().ToString() + " %");
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
