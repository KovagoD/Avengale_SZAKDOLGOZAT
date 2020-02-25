using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ingame_notification_script : MonoBehaviour
{
    private bool CR_running = false;
    public void message(string input_text, int duration)
    {
        var colors = GameObject.Find("Game manager").GetComponent<Game_manager>();
        gameObject.GetComponent<Animator>().Play("Notification_fade_in");

        GameObject.Find("Notification text").GetComponent<TextMeshPro>().color = colors.white;

        if (CR_running == false)
        {
            gameObject.GetComponent<Visibility_script>().setVisible();

            GameObject.Find("Notification text").GetComponent<Text_animation>().startAnim(input_text, 0.05f);
            StartCoroutine("Wait", duration);
        }
        else
        {
            StopCoroutine("Wait");
            gameObject.GetComponent<Visibility_script>().setVisible();

            GameObject.Find("Notification text").GetComponent<Text_animation>().startAnim(input_text, 0.05f);
            StartCoroutine("Wait", duration);        

        }

    }


    public void message(string input_text, int duration, string color)
    {
        StopAllCoroutines();
        CR_running = false;
        gameObject.GetComponent<Animator>().Play("Notification_fade_in");


        if (CR_running == false)
        {

            gameObject.GetComponent<Visibility_script>().setVisible();


            var colors = GameObject.Find("Game manager").GetComponent<Game_manager>();

            if (color == "poor")
            {
                GameObject.Find("Notification text").GetComponent<TextMeshPro>().color = colors.gray;

            }
            if (color == "common")
            {
                GameObject.Find("Notification text").GetComponent<TextMeshPro>().color = colors.white;
            }
            if (color == "uncommon")
            {
                GameObject.Find("Notification text").GetComponent<TextMeshPro>().color = colors.green;
            }
            if (color == "rare")
            {
                GameObject.Find("Notification text").GetComponent<TextMeshPro>().color = colors.blue;
            }
            if (color == "epic")
            {
                GameObject.Find("Notification text").GetComponent<TextMeshPro>().color = colors.purple;
            }
            if (color == "legendary")
            {
                GameObject.Find("Notification text").GetComponent<TextMeshPro>().color = colors.yellow;
            }

            if (color == "red")
            {
                GameObject.Find("Notification text").GetComponent<TextMeshPro>().color = colors.red;
            }
            if (color == "blue")
            {
                GameObject.Find("Notification text").GetComponent<TextMeshPro>().color = colors.blue;
            }

            GameObject.Find("Notification text").GetComponent<Text_animation>().startAnim(input_text, 0.05f);
            StartCoroutine("Wait", duration);
        }
        else
        {
            StopCoroutine("Wait");
            gameObject.GetComponent<Visibility_script>().setVisible();
            GameObject.Find("Notification text").GetComponent<Text_animation>().startAnim(input_text, 0.05f);
            gameObject.GetComponent<Animator>().Play("Notification_fade_out");
            StartCoroutine("Wait", duration);

        }
    }


    IEnumerator Wait(int duration)
    {
        CR_running = true;
        yield return new WaitForSeconds(duration);
        CR_running = false;
        //gameObject.GetComponent<Animator>().Play("Notification_anim");
        gameObject.GetComponent<Animator>().Play("Notification_fade_out");
        //StartCoroutine("WaitForAnimation",3);
    }

    IEnumerator WaitForAnimation(int duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.GetComponent<Visibility_script>().setInvisible();
    }

}
