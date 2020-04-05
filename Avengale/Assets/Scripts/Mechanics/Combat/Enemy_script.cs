using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_script : MonoBehaviour
{
    [Header("Enemy details")]
    public GameObject selected;
    public int id, enemy_health, enemy_damage;

    [Header("References")]
    public GameObject health_bar, health_text, appearance_human, appearance_non_human, turn_sign;
    public Animator spell_animation;
    public SpriteRenderer appearanceSprite;
    public string enemy_name;
    private List<Enemy> enemies;
    private Combat_manager_script _combatManager;
    private Character_stats _characterStats;
    private Character_manager _characterManager;
    private Ingame_notification_script _notification;

    private void Start()
    {
        _notification = GameObject.Find("Notification").GetComponent<Ingame_notification_script>();
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        _combatManager = GameObject.Find("Game manager").GetComponent<Combat_manager_script>();
        enemies = GameObject.Find("Game manager").GetComponent<Enemy_manager_script>().enemies;
    }
    private void Update()
    {
        if (GameObject.Find("Game manager").GetComponent<Spell_script>().target == gameObject)
        {
            selected.GetComponent<SpriteRenderer>().enabled = true;
        }
        else { selected.GetComponent<SpriteRenderer>().enabled = false; }
    }

    public void opponentUpdateHealthBar()
    {
        int health_in_percent = Convert.ToInt32(Math.Round(((double)enemy_health / (double)GameObject.Find("Game manager").GetComponent<Enemy_manager_script>().enemies[id].health) * 100));
        SpriteRenderer _healthBar = health_bar.GetComponent<SpriteRenderer>();
        if (health_in_percent >= 100)
        {
            _healthBar.sprite = Resources.Load<Sprite>("enemy hp bar/100");
        }
        else if (health_in_percent >= 75)
        {
            _healthBar.sprite = Resources.Load<Sprite>("enemy hp bar/75");
        }
        else if (health_in_percent >= 50)
        {
            _healthBar.sprite = Resources.Load<Sprite>("enemy hp bar/50");
        }
        else if (health_in_percent >= 25)
        {
            _healthBar.sprite = Resources.Load<Sprite>("enemy hp bar/25");
        }
        else if (health_in_percent >= 0)
        {
            _healthBar.sprite = Resources.Load<Sprite>("enemy hp bar/0");
        }

        health_text.GetComponent<Text_animation>().startAnim(enemy_health + "/" + GameObject.Find("Game manager").GetComponent<Enemy_manager_script>().enemies[id].health, 0.05f);
    }

    public void opponentTakeDamage(int amount)
    {
        enemy_health -= amount;
        //Debug.Log(enemies[id].enemy_name + ": " + enemies[id].health + "/" + enemy_health + " hp");
        opponentUpdateHealthBar();
        if (enemy_health <= 0)
        {
            opponentDie();
            //GetReward();
        }
    }

    public void opponentAttack()
    {
        var _playerManager = GameObject.Find("Character").GetComponent<Character_manager>();
        var _playerHealthPercent = _characterStats.getPercentOfHealth(enemies[id].damage);

        if (GameObject.Find("Game manager").GetComponent<Game_manager>().vibrationEnabled)
        {
            Handheld.Vibrate();
        }

        _playerManager.spell_animation.Play(enemies[id].attackAnimation);


        string _hitAnimation = "";


        int random_crit = UnityEngine.Random.Range(0, 100);

        if (random_crit > 90)
        {
            _characterStats.looseHealth(_playerHealthPercent * 2);
            int random_hit = UnityEngine.Random.Range(1, 3);
            switch (random_hit)
            {
                case 1:
                    _hitAnimation = "crit_hit_1";
                    break;
                case 2:
                    _hitAnimation = "crit_hit_2";
                    break;
            }
            _playerManager.damage_text.GetComponent<Text_animation>().startAnim("-" + _playerHealthPercent * 2 + " CRITICAL!", 0.05f);
        }
        else
        {
            _characterStats.looseHealth(_playerHealthPercent);
            int random_hit = UnityEngine.Random.Range(1, 7);
            switch (random_hit)
            {
                case 1:
                    _hitAnimation = "hit_1";
                    break;
                case 2:
                    _hitAnimation = "hit_2";
                    break;
                case 3:
                    _hitAnimation = "hit_3";
                    break;
                case 4:
                    _hitAnimation = "hit_4";
                    break;
                case 5:
                    _hitAnimation = "hit_5";
                    break;
                case 6:
                    _hitAnimation = "hit_6";
                    break;
            }
            _playerManager.damage_text.GetComponent<Text_animation>().startAnim("-" + _playerHealthPercent, 0.05f);

            //Debug.Log(enemies[id].enemy_name + "'s attack animaton: " + enemies[id].attackAnimation);
        }
        _playerManager.damage_text.GetComponent<Animator>().Play(_hitAnimation);
        GameObject.Find("Battle_scene").GetComponent<Animator>().Play("Screen_shake_1");

        if (_characterStats.Local_health <= 0)
        {
            _combatManager.showResults("Defeat");
        }
    }

    public void opponentDie()
    {
        GameObject.Find("Game manager").GetComponent<Spell_script>().setTarget(null);
        /*
        if (GameObject.Find("Game manager").GetComponent<Enemy_manager_script>().enemies[id].isHuman)
        {

        }
        */

        gameObject.GetComponent<Visibility_script>().setInvisible();

        _notification.message(enemy_name + " is defeated!", 3);

        if (!_characterStats.defeated_enemies.Contains(enemies[id]))
        {
            _characterStats.defeated_enemies.Add(enemies[id]);
        }
    }

    public void GetReward()
    {
        int[] _rewards = enemies[id].rewards;

        if (_rewards[0] != 0)
        {
            _characterStats.itemPickup(_rewards[0], true);
        }
        if (_rewards[1] != 0)
        {
            _characterStats.itemPickup(_rewards[1], true, true);
        }
        if (_rewards[2] != 0)
        {
            _characterStats.getXP(_rewards[2]);
        }
        if (_rewards[3] != 0)
        {
            _characterStats.getMoney(_rewards[3]);
        }
    }

    public bool isAlive()
    {
        if (enemy_health <= 0)
        {
            return false;
        }
        else { return true; }
    }

    public void enemyInitialize(int input_id)
    {
        var enemy = GameObject.Find("Game manager").GetComponent<Enemy_manager_script>().enemies[input_id];
        _characterManager = gameObject.GetComponent<Character_manager>();
        enemy_name = enemy.enemy_name;
        enemy_damage = enemy.damage;
        enemy_health = enemy.health;
        id = input_id;

        opponentUpdateHealthBar();

        if (enemy.isHuman)
        {
            appearance_non_human.SetActive(false);
            appearance_human.SetActive(true);

            if (enemy.isRandomAppearance)
            {
                int hair_length = 8, eyes_length = 4, nose_length = 2, mouth_length = 3, body_length = 3;

                int random_sex = UnityEngine.Random.Range(0, 2);
                if (random_sex == 0)
                {
                    _characterManager.sex = false;
                }
                else { _characterManager.sex = true; }
                _characterManager.hair_id = UnityEngine.Random.Range(0, hair_length + 1);
                _characterManager.eyes_id = UnityEngine.Random.Range(0, eyes_length + 1);
                _characterManager.nose_id = UnityEngine.Random.Range(0, nose_length + 1);
                _characterManager.mouth_id = UnityEngine.Random.Range(0, mouth_length + 1);
                _characterManager.body_id = UnityEngine.Random.Range(0, body_length + 1);

                _characterManager.hair_color_r = (byte)UnityEngine.Random.Range(0, 256);
                _characterManager.hair_color_g = (byte)UnityEngine.Random.Range(0, 256);
                _characterManager.hair_color_b = (byte)UnityEngine.Random.Range(0, 256);

            }
            else
            {
                _characterManager.sex = enemy.sex;
                _characterManager.hair_id = enemy.appearance[0];
                _characterManager.eyes_id = enemy.appearance[1];
                _characterManager.nose_id = enemy.appearance[2];
                _characterManager.mouth_id = enemy.appearance[3];
                _characterManager.body_id = enemy.appearance[4];

                _characterManager.hair_color_r = enemy.hair_color[0];
                _characterManager.hair_color_g = enemy.hair_color[1];
                _characterManager.hair_color_b = enemy.hair_color[2];
            }



            _characterManager.equipment_head_id = enemy.equipment[0];
            _characterManager.equipment_body_id = enemy.equipment[1];
            _characterManager.equipment_legs_id = enemy.equipment[2];
            _characterManager.equipment_left_id = enemy.equipment[3];
            _characterManager.equipment_shoulder_id = enemy.equipment[4];
            _characterManager.equipment_gadget_id = enemy.equipment[5];
            _characterManager.equipment_feet_id = enemy.equipment[6];
            _characterManager.equipment_right_id = enemy.equipment[7];
        }
        else
        {
            appearance_human.SetActive(false);
            appearance_non_human.SetActive(true);
            appearanceSprite.sprite = enemy.non_human_appearance;

        }


        gameObject.GetComponent<Visibility_script>().setVisible();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            GameObject.Find("Game manager").GetComponent<Spell_script>().setTarget(gameObject);
        }

        if (Input.GetMouseButtonUp(1))
        {
            opponentAttack();
        }

    }
}

