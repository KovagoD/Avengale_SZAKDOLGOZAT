using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_pause_button_script : MonoBehaviour
{
    private Combat_manager_script _combatManager;
    public Sprite pauseSprite;
    public Sprite resumeSprite;
    private void Start()
    {
        _combatManager = GameObject.Find("Game manager").GetComponent<Combat_manager_script>();
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0) && gameObject.name == "Battle_pause_button")
        {
            if (!_combatManager.isPaused)
            {
                _combatManager.pauseBattle();
                gameObject.GetComponent<SpriteRenderer>().sprite = resumeSprite;
                Debug.Log("Battle paused");
            }
            else
            {
                _combatManager.resumeBattle();
                gameObject.GetComponent<SpriteRenderer>().sprite = pauseSprite;
            }
        }
        if (Input.GetMouseButtonUp(0) && gameObject.name == "Battle_skip_opponent_turn_button")
        {
            if (!_combatManager.isPaused)
            {
                _combatManager.skipEnemyRound();
                gameObject.GetComponent<SpriteRenderer>().sprite = resumeSprite;
                Debug.Log("Battle paused");
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = pauseSprite;
            }
        }
    }
}
