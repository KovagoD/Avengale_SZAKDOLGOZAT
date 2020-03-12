using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_script : MonoBehaviour
{
    public Sprite sprite_normal;
    public Sprite sprite_activated;

    void OnMouseOver()
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
    void OnMouseExit()
    {
        if (sprite_normal != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite_normal;
        }
    }

}
