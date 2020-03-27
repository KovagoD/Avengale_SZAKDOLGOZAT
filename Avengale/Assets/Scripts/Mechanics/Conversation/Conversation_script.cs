using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Conversation_script : MonoBehaviour
{
    public List<Conversation> conversations = new List<Conversation>();

    [Header("References")]
    public GameObject name_text;
    public GameObject dialogue_text;
    public GameObject option_1_button;
    public GameObject option_2_button;
    public GameObject battle_screen;

    public GameObject conversation;


    [Header("Conversation details")]
    public int dialogue_length = 0;
    public int conversation_id;

    private Ingame_notification_script _notification;
    private Character_stats _characterStats;

    private string _charNameWithTitle;
    private string _charName;

    void Start()
    {
        initializeConversations();
    }

    public void initializeConversations()
    {

        conversations.Clear();

        _notification = GameObject.Find("Notification").GetComponent<Ingame_notification_script>();
        _characterStats = GameObject.Find("Game manager").GetComponent<Character_stats>();


        _charNameWithTitle = _characterStats.Local_name + " " + _characterStats.Local_title;
        _charName = _characterStats.Local_name;


        /*----------------------------------
        COMMANDS:
            - none
            - next
            - exit
            - skip:where
            - change_dialog:id
            - item_add:id
            - money_add:amount
            - money_remove:amount
            - xp_add:amount
            - quest_add:id
            - quest_remove:id
            - start_battle:id
        ----------------------------------*/

        conversations.Add((new Conversation(0, "", "", new List<string>() { "", "", "" },
         new List<string>() {
        "",
        "",
        ""},
        new List<string>() { "", "", "" },
        new List<string>() { "exit", "next", "next" },

        new List<string>() { "", "", "" },
        new List<string>() { "exit", "next", "next" }
         )));


        conversations.Add((new Conversation(1, "Walking home", "normal", new List<string>() { "Unknown man", _charNameWithTitle, "Unknown man", _charName },
         new List<string>() {
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. ¤ Nulla non malesuada felis, eget euismod velit. Phasellus id sapien est. Curabitur in maximus velit. Sed lobortis sem vel ex cursus,",
        "Mauris semper ipsum sed turpis faucibus auctor. Vestibulum mattis lacus hendrerit commodo venenatis. Integer faucibus semper dui ultricies lobortis.",
        "Nulla efficitur erat et mi aliquet pharetra quis sit amet urna. Integer et urna ac turpis tempor posuere.",
        ("Wanna talk to ?")},

        new List<string>() { " Nulla non malesuada felis. (next)", "Nulla efficitur. (item)", "Integer faucibus semper (next)", "Curabitur (change)" },
        new List<string>() { "next", "item_add: 2", "next", "change_dialog: 2" },

        new List<string>() { "Nevermind. (exit)", "Faucibus. (item)", "Integer (next)", "Goodbye! (exit & quest)" },
        new List<string>() { "exit", "next item_add: 2", "next", "exit quest_add: 1" }
         )));


        conversations.Add((new Conversation(2, "TikTok meme", "quest", new List<string>() { "Gamer girl", "Gamer girl", "Gamer girl", "Gamer girl" },
        new List<string>() {
        "Hey " + _charName +"!\n\nHit or miss",
        "I guess they never miss,...?",
        "You got a boyfriend, I bet he doesnt kiss ya <b>He gon find another girl</b> and <color=#00ff00>he wont miss ya</color> He gon skrrt and hit the dab like Wiz Khalifa",
        "Take this!\nIt's dangerous to go alone."},

        new List<string>() { "? (next)", "Huh? (next)", "Okay. (next)", "Thank you! (item)" },
        new List<string>() { "next", "next", "next", "exit item_add:8" },

        new List<string>() { "Then what happened? (next)", "Huh? (none)", "Goodbye! (exit)", "Yos and 100c (exit & getMoney)" },
        new List<string>() { "next", "none", "exit", "exit money_add:100" }
        )));

        conversations.Add((new Conversation(3, "Demonstration", "tutorial", new List<string>() { "David", "David", "David", "David" },
        new List<string>() {
        "Hi! Can I give you an item?",
        "If you need some credits you can sell it. Your choice. ",
        "Oh, I almost forgot...Take this! Not much, but still something.",
        "Now, go on your journey! \n\n<color=#808080>Padoru's hat\n100 credit\n1000 xp </color>"},

        new List<string>() { "Yes please!", "Okay.", "Thank you!", "Good bye!" },
        new List<string>() { "item_add:10 ", "next", "money_add:100", "exit xp_add:1000" },

        new List<string>() { "No.", "(none)", "Goodbye! (exit)", "(none)" },
        new List<string>() { "next", "none", "exit", "none" }
        )));

        conversations.Add((new Conversation(4, "Battle test", "battle", new List<string>() { "Szisz" },
        new List<string>() {
        "Fight me!"},

        new List<string>() { "Ok!" },
        new List<string>() { "start_battle:1 " },

        new List<string>() { "No." },
        new List<string>() { "exit" }
        )));
    }


    public void selectOption(int option_id)
    {
        var conversation = GameObject.Find("Conversation").GetComponent<Conversation_script>();
        string option = "";

        switch (option_id)
        {
            case 0:
                option = conversations[conversation.conversation_id].option_1_type[conversation.dialogue_length];
                break;
            case 1:
                option = conversations[conversation.conversation_id].option_2_type[conversation.dialogue_length];
                break;
            default:
                break;
        }



        if (option.Contains("next"))
        {
            conversation.continueConversation();
        }

        if (option.Contains("exit"))
        {
            conversation.closeConversation();
            _characterStats.completed_conversations.Add(conversations[conversation.conversation_id]);
        }

        if (option.Contains("item_add"))
        {
            if (!_characterStats.isInventoryFull())
            {
                string[] split = option.Split(':');
                _characterStats.itemPickup(int.Parse(split[1]), true);
                conversation.continueConversation();
            }
            else { conversation.closeConversation(); }
        }

        if (option.Contains("change_dialog"))
        {
            string[] split = option.Split(':');
            conversation.showConversation(int.Parse(split[1]));
        }

        if (option.Contains("skip"))
        {
            string[] split = option.Split(':');
            conversation.dialogue_length = int.Parse(split[1]) - 1;
            conversation.continueConversation();
        }
        if (option.Contains("money_add"))
        {
            string[] split = option.Split(':');
            _characterStats.getMoney(int.Parse(split[1]));
            conversation.continueConversation();
        }
        if (option.Contains("money_remove"))
        {
            string[] split = option.Split(':');
            _characterStats.giveMoney(int.Parse(split[1]));
            conversation.continueConversation();
        }
        if (option.Contains("xp_add"))
        {
            string[] split = option.Split(':');
            _characterStats.getXP(int.Parse(split[1]));
            conversation.continueConversation();
        }
        if (option.Contains("quest_add"))
        {
            string[] split = option.Split(':');
            GameObject.Find("Game manager").GetComponent<Quest_manager_script>().acceptQuest(int.Parse(split[1]));
            conversation.continueConversation();
        }

        if (option.Contains("start_battle"))
        {
            string[] split = option.Split(':');
            GameObject.Find("Game manager").GetComponent<Game_manager>().Change_screen(battle_screen, false);
            GameObject.Find("Game manager").GetComponent<Combat_manager_script>().initializeBattle(int.Parse(split[1]));
            conversation.continueConversation();
        }

    }

    public void showConversation(int id)
    {
        StopCoroutine("Wait");
        gameObject.GetComponent<Animator>().Play("Conversation_slide_in", -1, 0f);
        gameObject.GetComponent<Animator>().Play("Conversation_slide_in");

        GameObject.Find("Overlay").GetComponent<Overlay_script>().showOverlay();


        _charNameWithTitle = _characterStats.Local_name + " " + _characterStats.Local_title;
        _charName = _characterStats.Local_name;

        //if (!_characterStats.completed_conversations.Contains(conversations[id]))
        //{
        gameObject.GetComponent<Open_button_script>().Open();

        conversation_id = id;
        dialogue_length = 0;
        string prefix = "";
        switch (conversations[id].type)
        {
            case "normal":
                prefix = "";
                break;
            case "quest":
                prefix = "[Quest]";
                break;
            case "tutorial":
                prefix = "[Tutorial]";
                break;

            default:
                break;
        }
        name_text.GetComponent<Text_animation>().startAnim(conversations[id].speaker[0], 0.01f);
        dialogue_text.GetComponent<Text_animation>().startAnim(conversations[id].dialogue[0], 0.01f);



        option_1_button.GetComponent<Text_animation>().startAnim("¤ " + conversations[id].option_1[0], 0.01f);
        option_2_button.GetComponent<Text_animation>().startAnim("¤ " + conversations[id].option_2[0], 0.01f);

        checkIfOptionsIsNone();

        _notification.message(prefix + " " + conversations[id].name, 2);
        //}


    }

    private void checkIfOptionsIsNone()
    {
        if (conversations[conversation_id].option_1_type[dialogue_length].Contains("none"))
        {
            option_1_button.GetComponent<Visibility_script>().setInvisible();
        }
        else { option_1_button.GetComponent<Visibility_script>().setVisible(); }
        if (conversations[conversation_id].option_2_type[dialogue_length].Contains("none"))
        {
            option_2_button.GetComponent<Visibility_script>().setInvisible();
        }
        else { option_2_button.GetComponent<Visibility_script>().setVisible(); }
    }

    public void closeConversation()
    {
        gameObject.GetComponent<Animator>().Play("Conversation_slide_out");
        GameObject.Find("Overlay").GetComponent<Overlay_script>().closeOverlay();

        StopCoroutine("Wait");
        StartCoroutine("Wait", 3);

    }

    IEnumerator Wait(int duration)
    {
        yield return new WaitForSeconds(duration);

        gameObject.GetComponent<Visibility_script>().setInvisible();
        name_text.GetComponent<Visibility_script>().setInvisible();
        dialogue_text.GetComponent<Visibility_script>().setInvisible();

        option_1_button.GetComponent<Visibility_script>().setInvisible();
        option_2_button.GetComponent<Visibility_script>().setInvisible();

    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.GetComponent<Visibility_script>().isOpened)
        {
            dialogue_text.GetComponent<Text_animation>().Instant_text();
            name_text.GetComponent<Text_animation>().Instant_text();
            option_1_button.GetComponent<Text_animation>().Instant_text();
            option_2_button.GetComponent<Text_animation>().Instant_text();
        }
    }

    public void continueConversation()
    {
        if (conversations[conversation_id].dialogue.Count > dialogue_length + 1)
        {
            dialogue_length += 1;

            name_text.GetComponent<Text_animation>().startAnim(conversations[conversation_id].speaker[dialogue_length], 0.01f);
            dialogue_text.GetComponent<Text_animation>().startAnim(conversations[conversation_id].dialogue[dialogue_length], 0.01f);

            option_1_button.GetComponent<Text_animation>().startAnim("¤ " + conversations[conversation_id].option_1[dialogue_length], 0.01f);
            option_2_button.GetComponent<Text_animation>().startAnim("¤ " + conversations[conversation_id].option_2[dialogue_length], 0.01f);

            checkIfOptionsIsNone();
        }
        else { closeConversation(); }
    }
}
[System.Serializable]
public class Conversation
{
    public int id;
    public string name;
    public string type;
    public List<string> speaker;
    public List<string> dialogue;

    public List<string> option_1;
    public List<string> option_1_type;

    public List<string> option_2;
    public List<string> option_2_type;

    public Conversation(int id, string name, string type, List<string> speaker, List<string> dialogue, List<string> option_1, List<string> option_1_type, List<string> option_2, List<string> option_2_type)
    {
        this.id = id;
        this.name = name;
        this.type = type;
        this.speaker = speaker;
        this.dialogue = dialogue;

        this.option_1 = option_1;
        this.option_1_type = option_1_type;

        this.option_2 = option_2;
        this.option_2_type = option_2_type;
    }
}
