using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_script : MonoBehaviour
{
    public Sprite sprite_normal;
    public Sprite sprite_activated;

    void OnMouseOver()
    {
        if (gameObject.GetComponent<SpriteRenderer>())
        {
            if (sprite_normal != null)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite_activated;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (sprite_normal != null)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprite_normal;
                }
            }
        }
        else if (gameObject.GetComponent<Image>())
        {
            if (sprite_normal != null)
            {
                gameObject.GetComponent<Image>().sprite = sprite_activated;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (sprite_normal != null)
                {
                    gameObject.GetComponent<Image>().sprite = sprite_normal;
                }
            }
        }

    }

    void OnMouseUp()
    {
        if (gameObject.GetComponent<SpriteRenderer>())
        {
            if (sprite_normal != null)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite_normal;
            }
        }
        else if (gameObject.GetComponent<Image>())
        {
            if (sprite_normal != null)
            {
                gameObject.GetComponent<Image>().sprite = sprite_normal;
            }
        }
    }
    void OnMouseExit()
    {
        if (gameObject.GetComponent<SpriteRenderer>())
        {
            if (sprite_normal != null)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite_normal;
            }
        }
        else if (gameObject.GetComponent<Image>())
        {
            if (sprite_normal != null)
            {
                gameObject.GetComponent<Image>().sprite = sprite_normal;
            }
        }
    }

}
