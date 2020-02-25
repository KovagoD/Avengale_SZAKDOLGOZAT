using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Authorization_no_script : MonoBehaviour
{
    void OnMouseUp()
    {
        if (gameObject.GetComponent<Visibility_script>().isOpened)
        {
            //GameObject.Find("Game manager").GetComponent<Character_stats>().deleteItem(mode, slot_id);
            GameObject.Find("Authorization").GetComponent<Authorization_script>().AuthorizationNo();
        }
    }
}
