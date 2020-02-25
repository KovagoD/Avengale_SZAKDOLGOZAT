using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conv_test_button : MonoBehaviour
{
    void OnMouseOver()
    {

        if (Input.GetMouseButtonUp(0) && !GameObject.Find("Item_preview").GetComponent<Visibility_script>().isOpened)
        {

            int rnd = Random.Range(1, 5);
            GameObject.Find("Conversation").GetComponent<Conversation_script>().showConversation(rnd);
        }
    }
}
