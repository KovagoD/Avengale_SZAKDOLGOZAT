using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_menu_script : MonoBehaviour
{
    public bool isOpened;
    public GameObject title;
    public GameObject save_button;
    public GameObject back_button, back_button_text;



    public void showPauseMenu()
    {
        var _gameManager = GameObject.Find("Game manager").GetComponent<Game_manager>();
        isOpened = true;
        GameObject.Find("Overlay").GetComponent<Overlay_script>().showOverlay();
        gameObject.GetComponent<Animator>().Play("Pause_slide_in_anim");

        title.GetComponent<Text_animation>().startAnim("Pause", 0.01f);

        if (_gameManager.current_screen == _gameManager.Combat_screen)
        {
            back_button_text.GetComponent<Text_animation>().startAnim("Surrender battle", 0.01f);
        }
        else
        {
            back_button_text.GetComponent<Text_animation>().startAnim("Main menu", 0.01f);
        }


    }

    public void closePauseMenu()
    {
        isOpened = false;
        GameObject.Find("Overlay").GetComponent<Overlay_script>().closeOverlay();
        gameObject.GetComponent<Animator>().Play("Pause_slide_out_anim");


    }

}
