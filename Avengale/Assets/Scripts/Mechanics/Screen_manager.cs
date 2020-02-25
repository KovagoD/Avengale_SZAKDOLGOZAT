using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_manager : MonoBehaviour
{
    public GameObject[] elements;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
