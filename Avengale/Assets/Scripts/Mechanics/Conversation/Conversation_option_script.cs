using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation_option_script : MonoBehaviour
{
    [Header("Option details")]
    public int option_id;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0) && gameObject.GetComponent<Visibility_script>().isOpened)
        {
            GameObject.Find("Conversation").GetComponent<Conversation_script>().selectOption(option_id);
        }
    }
}
