using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_manager : MonoBehaviour
{
    public GameObject[] elements;
    public void SetScreenActive()
    {
        foreach (var element in elements)
        {
            element.GetComponent<Visibility_script>().setVisible();
        }
    }

    public void SetScreenInactive()
    {
        foreach (var element in elements)
        {
            element.GetComponent<Visibility_script>().setInvisible();
        }
    }
}
