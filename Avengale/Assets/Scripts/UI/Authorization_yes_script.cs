using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Authorization_yes_script : MonoBehaviour
{
    void OnMouseUp()
    {
        if (gameObject.GetComponent<Visibility_script>().isOpened)
        {
            GameObject.Find("Authorization").GetComponent<Authorization_script>().AuthorizationYes();
        }
    }
}
