using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text_animation : MonoBehaviour
{
    public TextMeshProUGUI ugui = null;
    public TextMeshPro generic = null;
    public string Text;
    int counter = 0;
    public float speed = 0.01f;
    public bool start = false;

    private void Start()
    {
        if (start == true)
        {
            startAnim(Text, speed);
        }
    }

    public void restartAnim()
    {
        startAnim(Text, speed);
    }

    public void Instant_text()
    {
        counter = 0;
        StopAllCoroutines();
        if (ugui != null && ugui.enabled == true)
        {
            ugui.GetComponent<TextMeshProUGUI>().text = "";
            ugui.GetComponent<TextMeshProUGUI>().text = Text;

        }
        else if (generic != null && generic.enabled == true)
        {
            generic.GetComponent<TextMeshPro>().text = "";
            generic.GetComponent<TextMeshPro>().text = Text;
        }
    }

    public void startAnim(string input_text, float speed)
    {
        if (gameObject.activeInHierarchy)
        {
            StopCoroutine("Wait");
            counter = 0;
            Text = input_text;
            if (ugui != null && ugui.enabled == true)
            {
                ugui.GetComponent<TextMeshProUGUI>().text = "";

            }
            else if (generic != null && generic.enabled == true)
            {
                generic.GetComponent<TextMeshPro>().text = "";
            }

            StartCoroutine("Wait");
        }
    }

    IEnumerator Wait()
    {

        if (ugui != null && ugui.enabled == true)
        {
            var text_box = ugui.GetComponent<TextMeshProUGUI>();
            for (int i = 0; i < Text.Length; i++)
            {
                if (counter < Text.Length)
                {
                    text_box.text += Text[counter];
                    yield return new WaitForSeconds(speed);
                    counter++;
                }
            }
        }

        if (generic != null && generic.enabled == true)
        {
            var text_box = generic.GetComponent<TextMeshPro>();
            for (int i = 0; i < Text.Length; i++)
            {
                if (counter < Text.Length)
                {
                    text_box.text += Text[counter];
                    yield return new WaitForSeconds(speed);
                    counter++;
                }
            }
        }
    }
}
