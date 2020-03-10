using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Visibility_script : MonoBehaviour
{
    public bool isOpened = false;

    public GameObject[] children;
    public GameObject[] children_text;
    public GameObject[] children_text_gui;
    void Start()
    {

        if (isOpened == false)
        {
            setInvisible();
        }
        else { setVisible(); }
    }

    public void setVisible()
    {
        
        isOpened = true;
        if (children.Length > 0)
        {
            foreach (var item in children)
            {
                item.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        if (children_text.Length > 0)
        {
            foreach (var item in children_text)
            {
                item.GetComponent<TextMeshPro>().enabled = true;
                item.GetComponent<Text_animation>().restartAnim();
            }
        }
        if (children_text_gui.Length > 0)
        {
            foreach (var item in children_text_gui)
            {
                item.GetComponent<TextMeshProUGUI>().enabled = true;
                item.GetComponent<Text_animation>().restartAnim();
            }
        }

        if (gameObject.GetComponent<BoxCollider2D>() != null)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled=true;
        }

         if (gameObject.GetComponent<Animation>() != null)
        {
            gameObject.GetComponent<Animation>().Play();
        }
    }

    public void setInvisible()
    {
        isOpened = false;
        if (children.Length > 0)
        {
            foreach (GameObject item in children)
            {
                item.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        if (children_text.Length > 0)
        {
            foreach (var item in children_text)
            {
                item.GetComponent<TextMeshPro>().enabled = false;
            }
        }
        if (children_text_gui.Length > 0)
        {
            foreach (var item in children_text_gui)
            {
                item.GetComponent<TextMeshProUGUI>().enabled = false;
            }
        }

        if (gameObject.GetComponent<BoxCollider2D>() != null)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (gameObject.GetComponent<Animation>() != null)
        {
            gameObject.GetComponent<Animation>().Stop();
        }
    }

}
