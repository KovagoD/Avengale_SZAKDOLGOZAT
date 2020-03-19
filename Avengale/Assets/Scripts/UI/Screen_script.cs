using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_script : MonoBehaviour
{
    public int order;
    void Update()
    {
        if ((gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Screen_to_inactive_right_anim") || gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Screen_to_inactive_left_anim")) && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            gameObject.SetActive(false);
        }
        
    }
}
