using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Vibration_button_script : MonoBehaviour
{
    public Sprite onSprite, offSprite;
    public GameObject text;

    void Start()
    {
        var _vibrationSetting = GameObject.Find("Game manager").GetComponent<Game_manager>().vibrationEnabled;

        if (_vibrationSetting == true)
        {
            GameObject.Find("Game manager").GetComponent<Game_manager>().vibrationEnabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = offSprite;
            text.GetComponent<Text_animation>().startAnim("Vibration OFF", 0.01f);
        }
        else if (_vibrationSetting == false)
        {
            GameObject.Find("Game manager").GetComponent<Game_manager>().vibrationEnabled = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = onSprite;
            text.GetComponent<Text_animation>().startAnim("Vibration ON", 0.01f);
        }
        
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var _vibrationSetting = GameObject.Find("Game manager").GetComponent<Game_manager>().vibrationEnabled;

            if (_vibrationSetting == true)
            {
                GameObject.Find("Game manager").GetComponent<Game_manager>().vibrationEnabled = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = offSprite;
                text.GetComponent<Text_animation>().startAnim("Vibration OFF", 0.01f);
            }
            else if (_vibrationSetting == false)
            {
                GameObject.Find("Game manager").GetComponent<Game_manager>().vibrationEnabled = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = onSprite;
                Handheld.Vibrate();
                text.GetComponent<Text_animation>().startAnim("Vibration ON", 0.01f);

            }
        }

    }
}
