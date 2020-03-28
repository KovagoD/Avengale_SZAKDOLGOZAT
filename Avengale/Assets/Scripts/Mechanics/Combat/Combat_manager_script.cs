using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum battleRound { Player, Opponent_1, Opponent_2 }

public class Combat_manager_script : MonoBehaviour
{
    [Header("Battle details")]
    public bool isPaused;
    public battleRound[] round;
    public battleRound current_round;

    public int round_counter;

    public bool isOngoing;

    private int timer;
    public int round_time;
    private int battle_id;
    public int[] opponent_ids;
    public List<Battle> battles = new List<Battle>();

    [Header("References")]
    public GameObject[] opponents;
    public GameObject remaining_time;
    public GameObject round_text;
    public GameObject battle_background;

    private Ingame_notification_script _notification;
    private Character_stats _characterStats;
    private Character_manager _characterManager;
    private Enemy_manager_script _enemyManagerScript;
    private Item_script _itemScript;
    private Spell_script _spellScript;

    private Bar_script _healthBar;
    private Bar_script _resourceBar;
    private Animator _spellbarAnimator;

    public int currentOpponentID;

    private bool _isOpponentAttacked;

    void Start()
    {

    }

    private void Update()
    {

    }

    public void surrenderBattle()
    {
        pauseBattle();
        gameObject.GetComponent<Game_manager>().Change_screen(gameObject.GetComponent<Game_manager>().Character_screen_UI, false);
    }

    public void pauseBattle()
    {
        isPaused = true;
        StopAllCoroutines();
    }

    public void resumeBattle()
    {
        isPaused = false;
        StartCoroutine("Secs");
    }
    public void initializeBattle(int id)
    {
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();
        _characterManager = GameObject.Find("Character").GetComponent<Character_manager>();
        _notification = GameObject.Find("Notification").GetComponent<Ingame_notification_script>();
        _enemyManagerScript = GameObject.Find("Game manager").GetComponent<Enemy_manager_script>();
        _itemScript = GameObject.Find("Game manager").GetComponent<Item_script>();
        _spellScript = GameObject.Find("Game manager").GetComponent<Spell_script>();
        isPaused = false;

        _spellScript.initializeSpells();
        _spellScript.setupAttributes();

        _healthBar = GameObject.Find("Health_bar").GetComponent<Bar_script>();
        _resourceBar = GameObject.Find("Resource_bar").GetComponent<Bar_script>();
        _spellbarAnimator = GameObject.Find("Spellbar").GetComponent<Animator>();

        GameObject.Find("Item_preview").GetComponent<Animator>().Play("Item_preview_slide_out_anim");
        GameObject.Find("Inventory slots").GetComponent<Animator>().Play("Inventory_slide_out_anim");
        GameObject.Find("Conversation").GetComponent<Conversation_script>().closeConversation();

        _spellScript.target = null;


        StopAllCoroutines();
        isOngoing = true;

        if (gameObject.GetComponent<Game_manager>().current_screen.name == "Combat_screen_UI")
        {
            if (_characterStats.Local_health > _characterStats.Local_max_health || _characterStats.Local_health < 0)
            {
                _characterStats.Local_health = _characterStats.Local_max_health;
                _healthBar.updateHealth();
            }

            if (_characterStats.Local_resource > _characterStats.Local_max_resource || _characterStats.Local_resource < 0)
            {
                _characterStats.Local_resource = _characterStats.Local_max_resource;
                _resourceBar.updateResource();
            }
        }

        battle_id = id;


        _characterStats.Local_health = _characterStats.Local_max_health;
        _characterStats.Local_resource = _characterStats.Local_max_resource;

        _healthBar.updateHealth();
        _resourceBar.updateResource();
        GameObject.Find("Spell_preview").GetComponent<Visibility_script>().setInvisible();

        remaining_time.GetComponent<Text_animation>().startAnim((round_time).ToString(), 0.05f);

        opponent_ids = battles[id].opponent_ids;

        opponents[0].GetComponent<Enemy_script>().enemyInitialize(battles[id].opponent_ids[0]);
        opponents[1].GetComponent<Enemy_script>().enemyInitialize(battles[id].opponent_ids[1]);

        _notification.message("¤" + battles[id].battle_name, 3);

        battle_background.GetComponent<SpriteRenderer>().sprite = battles[id].background;

        generateSequence();

        round_counter = 0;
        timer = 0;
        //StartCoroutine("Secs");
        changeRound();


        //Debug.Log("teljes:" + round_time + " osztott:" + (round_time / 2));

    }

    private void generateSequence()
    {
        round = new battleRound[3];

        int rnd = UnityEngine.Random.Range(0, 3);

        if (rnd == 0)
        {
            round[0] = battleRound.Player;
            round[1] = battleRound.Opponent_1;
            round[2] = battleRound.Opponent_2;
        }
        else if (rnd == 1)
        {
            round[0] = battleRound.Opponent_1;
            round[1] = battleRound.Player;
            round[2] = battleRound.Opponent_2;
        }
        else if (rnd == 2)
        {
            round[0] = battleRound.Opponent_1;
            round[1] = battleRound.Opponent_2;
            round[2] = battleRound.Player;
        }
    }

    public void stopBattle()
    {
        timer = 0;
        StopAllCoroutines();
    }

    private void setTurnSign(int input)
    {
        opponents[0].GetComponent<Enemy_script>().turn_sign.GetComponent<SpriteRenderer>().enabled = false;
        opponents[1].GetComponent<Enemy_script>().turn_sign.GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Character").GetComponent<Character_manager>().turn_sign.GetComponent<SpriteRenderer>().enabled = false;

        switch (input)
        {
            case 0:
                opponents[0].GetComponent<Enemy_script>().turn_sign.GetComponent<SpriteRenderer>().enabled = true;
                break;

            case 1:
                opponents[1].GetComponent<Enemy_script>().turn_sign.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 2:
                GameObject.Find("Character").GetComponent<Character_manager>().turn_sign.GetComponent<SpriteRenderer>().enabled = true;
                break;

            default:
                break;
        }
    }

    public void changeRound()
    {
        StopAllCoroutines();
        StartCoroutine("Secs");
        timer = -1;

        if (round_counter != 2)
        {
            round_counter++;
        }
        else
        {
            round_counter = 0;
        }

        //Debug.Log(round[round_counter]);

        if (round[round_counter] == battleRound.Opponent_1 && opponents[0].GetComponent<Enemy_script>().isAlive())
        {
            current_round = battleRound.Opponent_1;
            //currentOpponentID = battles[battle_id].opponent_ids[0];
            currentOpponentID = 0;
            setTurnSign(0);
            round_text.GetComponent<Text_animation>().startAnim(_enemyManagerScript.enemies[battles[battle_id].opponent_ids[0]].enemy_name, 0.05f);
            _notification.message(_enemyManagerScript.enemies[battles[battle_id].opponent_ids[0]].enemy_name + "'s round!", 3);
            _isOpponentAttacked = false;
            StartCoroutine("enemyopponentAttack", 0);


        }
        else if (round[round_counter] == battleRound.Opponent_1 && !opponents[0].GetComponent<Enemy_script>().isAlive())
        {
            changeRound();
        }
        else if (round[round_counter] == battleRound.Opponent_2 && opponents[1].GetComponent<Enemy_script>().isAlive())
        {
            current_round = battleRound.Opponent_2;
            currentOpponentID = 1;
            setTurnSign(1);

            round_text.GetComponent<Text_animation>().startAnim(_enemyManagerScript.enemies[battles[battle_id].opponent_ids[1]].enemy_name, 0.05f);
            _notification.message(_enemyManagerScript.enemies[battles[battle_id].opponent_ids[1]].enemy_name + "'s round!", 3);
            _isOpponentAttacked = false;
            StartCoroutine("enemyopponentAttack", 1);
        }
        else if (round[round_counter] == battleRound.Opponent_2 && !opponents[1].GetComponent<Enemy_script>().isAlive())
        {
            changeRound();
        }
        else if (round[round_counter] == battleRound.Player)
        {
            current_round = battleRound.Player;

            setTurnSign(2);
            round_text.GetComponent<Text_animation>().startAnim(_characterStats.Local_name, 0.05f);
            _notification.message(_characterStats.Local_name + "'s round!", 3);
        }

        if (!opponents[0].GetComponent<Enemy_script>().isAlive() && !opponents[1].GetComponent<Enemy_script>().isAlive())
        {
            isOngoing = false;
            showResults("Victory");
        }

        spellbarController();

    }

    private bool isSpellbarActive;
    public void spellbarController()
    {

        if (round[round_counter] == battleRound.Player)
        {
            _spellbarAnimator.Play("Spellbar_slide_in_anim");
            isSpellbarActive = true;
        }
        else if (round[round_counter] != battleRound.Player && isSpellbarActive)
        {
            _spellbarAnimator.Play("Spellbar_slide_out_anim");
        }
    }

    public void showResults(string result)
    {
        var enemy = GameObject.Find("Game manager").GetComponent<Enemy_manager_script>();
        var result_window = GameObject.Find("Battle_results");

        GameObject.Find("Overlay").GetComponent<Overlay_script>().showOverlay();


        GameObject.Find("Battle_name").GetComponent<Text_animation>().startAnim(battles[battle_id].battle_name, 0.05f);
        GameObject.Find("Battle_description").GetComponent<Text_animation>().startAnim(battles[battle_id].description, 0.05f);
        GameObject.Find("Battle_outcome").GetComponent<Text_animation>().startAnim(" -" + result + "! -", 0.05f);

        _spellbarAnimator.Play("Spellbar_slide_out_anim");
        GameObject.Find("Spell_preview").GetComponent<Visibility_script>().setInvisible();

        result_window.GetComponent<Open_button_script>().Open();
        result_window.GetComponent<Animator>().Play("Battle_results_slide_in_anim");

        var _rewards = battles[battle_id].rewards;
        string _battleRewards = "";

        int _xpRewards = 0;
        int _moneyRewards = 0;
        //int _spellPointRewards = 0;

        if (_rewards[0] != 0)
        {
            _battleRewards += "[" + _itemScript.items[_rewards[0]].name + "]\n";
        }
        if (_rewards[1] != 0)
        {
            _battleRewards += "[" + _itemScript.items[_rewards[1]].name + "]\n";
        }
        if (_rewards[2] != 0)
        {
            _xpRewards += _rewards[2];
        }
        if (_rewards[3] != 0)
        {
            _moneyRewards += _rewards[3];
        }
        /*
        if (_rewards[4] != 0)
        {
            _spellPointRewards += _rewards[4];
        }
        */

        //Opponent_1
        var _firstOpponentRewards = _enemyManagerScript.enemies[battles[battle_id].opponent_ids[0]].rewards;

        if (_firstOpponentRewards[0] != 0)
        {
            _battleRewards += "[" + _itemScript.items[_firstOpponentRewards[0]].name + "]\n";
        }

        if (_firstOpponentRewards[1] != 0)
        {
            _battleRewards += "[" + _itemScript.items[_firstOpponentRewards[1]].name + "]\n";
        }

        if (_firstOpponentRewards[2] != 0)
        {
            _xpRewards += _firstOpponentRewards[2];
        }

        if (_firstOpponentRewards[3] != 0)
        {
            _moneyRewards += _firstOpponentRewards[3];
        }

        //Opponent_2
        var _secondOpponentRewards = _enemyManagerScript.enemies[battles[battle_id].opponent_ids[1]].rewards;


        if (_secondOpponentRewards[0] != 0)
        {
            _battleRewards += "[" + _itemScript.items[_secondOpponentRewards[0]].name + "]\n";
        }

        if (_secondOpponentRewards[1] != 0)
        {
            _battleRewards += "[" + _itemScript.items[_secondOpponentRewards[1]].name + "]\n";
        }

        if (_secondOpponentRewards[2] != 0)
        {
            _xpRewards += _secondOpponentRewards[2];
        }

        if (_secondOpponentRewards[3] != 0)
        {
            _moneyRewards += _secondOpponentRewards[3];
        }

        _battleRewards += "+ " + _xpRewards + " XP\n+" + _moneyRewards + " credits";

        var result_battle_rewards = GameObject.Find("Battle_rewards");
        result_battle_rewards.GetComponent<Text_animation>().startAnim("<b>Rewards:</b>\n" + _battleRewards, 0.05f);
    }

    public battleRound getRound()
    {
        return round[round_counter];
    }

    IEnumerator Secs()
    {
        yield return new WaitForSeconds(1);
        if (isOngoing)
        {
            timer++;
            //Debug.Log(round_time - timer);
            remaining_time.GetComponent<Text_animation>().startAnim((round_time - timer).ToString(), 0.05f);

            if ((round_time - timer) == 0)
            {
                changeRound();
            }

            StartCoroutine("Secs");
        }
    }

    IEnumerator enemyopponentAttack(int enemy_id)
    {
        yield return new WaitForSeconds(round_time / 2);
        opponents[enemy_id].GetComponent<Enemy_script>().opponentAttack();
        _isOpponentAttacked = true;
    }

    public void skipEnemyRound()
    {

        if (round[round_counter] != battleRound.Player && !_isOpponentAttacked && opponents[currentOpponentID].GetComponent<Enemy_script>().isAlive())
        {
            opponents[currentOpponentID].GetComponent<Enemy_script>().opponentAttack();
            StopAllCoroutines();
        }

        changeRound();
    }

    private void Awake()
    {
        /*
            ID, OPPONENTS, DESCRIPTON, REWARD[item, quest item, xp, money]
        */
        battles.AddRange(new List<Battle>()
        {
            {new Battle(0, "Test battle", new int[] { 2, 1 }, "This is the descriptioon of Test Battle", new int[] { 5, 0, 1000, 100, 1 }, Resources.Load<Sprite>("Item_icons/Icon2"))},
            {new Battle(1, "JOGIJOGIJOGI", new int[] { 2, 2 }, "Ayayo? Aya. AYAYA!", new int[] { 0, 0, 200, 0, 2 }, Resources.Load<Sprite>("Item_icons/Icon2"))},
            {new Battle(2, "10 4 dinosaur", new int[] { 1, 2 }, "oki doki boomer", new int[] { 10, 0, 1000, 5000, 3 }, Resources.Load<Sprite>("Item_icons/Icon2"))},
            {new Battle(3, "Ambush", new int[] { 4, 4 }, "Two rebel rectruits appeared", new int[] { 0, 0, 10, 100, 1 }, Resources.Load<Sprite>("Item_icons/Icon2"))}

        });
    }

    public void getBattleReward()
    {
        var enemy = GameObject.Find("Game manager").GetComponent<Enemy_manager_script>();
        var _rewards = battles[battle_id].rewards;

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
        if (_rewards[4] != 0)
        {
            _characterStats.getSpellPoint(_rewards[4]);
        }
    }
}

public class Battle
{
    public string battle_name;
    public int id;
    public int[] opponent_ids;
    public string description;
    public int[] rewards;
    public Sprite background;


    public Battle(int id, string battle_name, int[] opponent_ids, string description, int[] rewards, Sprite background)
    {
        this.id = id;
        this.battle_name = battle_name;
        this.opponent_ids = opponent_ids;
        this.description = description;
        this.rewards = rewards;
        this.background = background;
    }
}
