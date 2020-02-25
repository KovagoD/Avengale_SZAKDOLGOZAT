using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Authorization_yes_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUp()
    {
        if (gameObject.GetComponent<Visibility_script>().isOpened)
        {
            //GameObject.Find("Game manager").GetComponent<Character_stats>().deleteItem(mode, slot_id);
            GameObject.Find("Authorization").GetComponent<Authorization_script>().AuthorizationYes();
        }
    }
}
