using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ingame_notification_script : MonoBehaviour
{

    public GameObject notificationText;

    private bool CR_running = false;
    public void message(string input_text, int duration)
    {
        Colors colors = new Colors();
        gameObject.GetComponent<Animator>().Play("Notification_fade_in");

        notificationText.GetComponent<TextMeshPro>().color = colors.white;

        if (CR_running == false)
        {
            gameObject.GetComponent<Visibility_script>().setVisible();

            notificationText.GetComponent<Text_animation>().startAnim(input_text, 0.05f);
            StartCoroutine("Wait", duration);
        }
        else
        {
            StopCoroutine("Wait");
            gameObject.GetComponent<Visibility_script>().setVisible();

            notificationText.GetComponent<Text_animation>().startAnim(input_text, 0.05f);
            StartCoroutine("Wait", duration);

        }

    }


    public void message(string input_text, int duration, string color)
    {
        StopAllCoroutines();
        CR_running = false;
        gameObject.GetComponent<Animator>().Play("Notification_fade_in");

        var textColor = notificationText.GetComponent<TextMeshPro>().color;

        if (CR_running == false)
        {

            gameObject.GetComponent<Visibility_script>().setVisible();

            Colors colors = new Colors();

            switch (color)
            {
                case "gray":
                case "poor":
                    notificationText.GetComponent<TextMeshPro>().color = colors.gray;
                    break;
                case "white":
                case "common":
                    notificationText.GetComponent<TextMeshPro>().color = colors.white;
                    break;
                case "green":
                case "uncommon":
                    notificationText.GetComponent<TextMeshPro>().color = colors.green;
                    break;
                case "blue":
                case "rare":
                    notificationText.GetComponent<TextMeshPro>().color = colors.blue;
                    break;
                case "purple":
                case "epic":
                    notificationText.GetComponent<TextMeshPro>().color = colors.purple;
                    break;
                case "yellow":
                case "legendary":
                    notificationText.GetComponent<TextMeshPro>().color = colors.yellow;
                    break;
                case "red":
                    notificationText.GetComponent<TextMeshPro>().color = colors.red;
                    break;
            }

            GameObject.Find("Notification text").GetComponent<Text_animation>().startAnim(input_text, 0.05f);
            //Debug.Log(notificationText.GetComponent<TextMeshPro>().color);
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
