using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bar_script_gui : MonoBehaviour
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
        } else if (mode=="resource")
        {
            _percentage = ((float)scrpt.Local_resource / (float)scrpt.Local_max_resource) * size;
        }

        var pos = bar.transform.position;
        pos.x = _percentage;
        bar.transform.position = pos;

        /*
        if (GameObject.Find("Item_preview").GetComponent<Visibility_script>().isOpened)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled=false;
        } else {gameObject.GetComponent<BoxCollider2D>().enabled=true;}
        
        if (mode=="health")
        {
            if (scrpt.Local_health>scrpt.Local_max_health)
            {
                scrpt.Local_health=scrpt.Local_max_health;
                updateHealth();
            }
        }

        if (mode=="resource")
        {
            if (scrpt.Local_resource>scrpt.Local_max_resource)
            {
                scrpt.Local_resource=scrpt.Local_max_resource;
                updateResource();
            }
        }
        */



    }

    public void updateBar(string left_text, string center_text, string right_text)
    {
        left.GetComponent<Text_animation>().startAnim(left_text,1f);
        center.GetComponent<Text_animation>().startAnim(center_text,1f);   
        right.GetComponent<Text_animation>().startAnim(right_text,0.01f);
    }

    public void updateHealth()
    {
        updateBar(GameObject.Find("Game manager").GetComponent<Character_stats>().Local_max_health+"/"+GameObject.Find("Game manager").GetComponent<Character_stats>().Local_health.ToString()
        ,"",
        (((float)GameObject.Find("Game manager").GetComponent<Character_stats>().Local_health/(float)GameObject.Find("Game manager").GetComponent<Character_stats>().Local_max_health)*100f).ToString("0")+" %");
    }
    
    public void updateResource()
    {
        updateBar(GameObject.Find("Game manager").GetComponent<Character_stats>().Local_max_resource+"/"+GameObject.Find("Game manager").GetComponent<Character_stats>().Local_resource.ToString()
        ,"",
        (((float)GameObject.Find("Game manager").GetComponent<Character_stats>().Local_resource/(float)GameObject.Find("Game manager").GetComponent<Character_stats>().Local_max_resource)*100f).ToString("0")+" %");
    }

     void OnMouseUp()
    {
        if (GameObject.Find("Item_preview").GetComponent<Visibility_script>().isOpened==false)
        {        
            if (mode=="health")
            {
                local.looseHealth(10);
                updateHealth();
            }
            else if (mode=="resource")
            {
                local.looseResource(10);
                updateResource();
            }
        }
    }
}
