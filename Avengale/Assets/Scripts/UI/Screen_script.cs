using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_script : MonoBehaviour
{
    // Start is called before the first frame update
    public int order;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (GameObject.Find("Game manager").GetComponent<Game_manager>().current_screen.name==gameObject.name)
        {
            gameObject.SetActive(true);
        }
        else{gameObject.SetActive(false);}
        */

        
        if ((gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Screen_to_inactive_right_anim") || gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Screen_to_inactive_left_anim")) && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            gameObject.SetActive(false);
        }
        
    }
}
