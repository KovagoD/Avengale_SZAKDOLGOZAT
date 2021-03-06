﻿using System;
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


        _charName = _characterStats.Player_name;

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

        conversations.Add((new Conversation(1, "Exploring the hub", "quest", new List<string>() { "Zachary", "Zachary", "Zachary" },
        new List<string>() {
            "Hello "+_charName+"! I've been waiting for you!",
            "My name is <b>Zachary</b>. I'm the instructor of the new recruits.",
            "Your first task is to find and speak with <b>David</b> in the hub. He might need some urgent help. \n\nAre you ready?"
        },

        new List<string>() { "Hello!", "Nice to meet you Zachary", "I'm ready!" },
        new List<string>() { "next", "next", "quest_add:1" },

        new List<string>() { "Nevermind.", "Nevermind.", "No." },
        new List<string>() { "exit", "exit", "finalexit" }
        )));

        conversations.Add((new Conversation(2, "Welcome to the HUB", "quest", new List<string>() { "David", "David", "David", "David" },
        new List<string>() {
            "Hi! How can I help you?",
            "So are you a recruit? I should've known.\n\nWhats your name?",
            "A pleasure to meet you "+_charName+"! I will instruct you on your first battle against two opponents! It won't be hard, I promise! ",
            "You need to defeat these two recruits as a training. If you fail, you can try any time you want."
        },

        new List<string>() { "Zachary told me to speak with you.", "I'm " + _charName, "...", "Let's fight!" },
        new List<string>() { "next", "next", "next", "start_battle:1" },

        new List<string>() { "Nevermind.", "Let's cut to the chase.", "Nevermind.", "No." },
        new List<string>() { "exit", "skip:3", "exit", "finalexit" }
        )));

        conversations.Add((new Conversation(3, "Kaalin's day", "normal", new List<string>() { "Kaalin" },
        new List<string>() {
            "I forgot something important...what was that?...\n\nOh...Hi! "
        },

        new List<string>() { "...Hi!" },
        new List<string>() { "finalexit" },

        new List<string>() { "Nevermind." },
        new List<string>() { "none" }
        )));

        conversations.Add((new Conversation(4, "Meeting with the others", "normal", new List<string>() { "Kaalin" },
        new List<string>() {
            "Hello! Are you new here too?\n\nMy name is <b>Kaalin</b>, I came from Europe! This is my first day. The view is so nice from here!",
        },

        new List<string>() { "I'm " + _charName + "." },
        new List<string>() { "next" },

        new List<string>() { "Nevermind." },
        new List<string>() { "exit" }
        )));

        conversations.Add((new Conversation(5, "Life in the station", "normal", new List<string>() { "David", "David" },
        new List<string>() {
            "Hello recruit!\n\nAre you completed the trials?",
            "Then wait for your next task! You will receive it soon."
        },

        new List<string>() { "Yes!", "Ok." },
        new List<string>() { "next", "finalexit" },

        new List<string>() { "No!", "Nevermind." },
        new List<string>() { "change_dialog:6", "exit" }
        )));

        conversations.Add((new Conversation(6, "The trials", "normal", new List<string>() { "David" },
        new List<string>() {
            "Then you should speak with <b>Zachary</b>. ",
        },

        new List<string>() { "Ok." },
        new List<string>() { "exit" },

        new List<string>() { "Nevermind." },
        new List<string>() { "exit" }
        )));

        conversations.Add((new Conversation(7, "The current situation", "normal", new List<string>() { "Zachary", "Zachary" },
        new List<string>() {
            "We are waiting three more supply shipments from Earth. The station requested it a month ago.\n\nThis is worrisome.",
            "Maybe it's just a mistake...\n\n I should investigate.",

        },

        new List<string>() { "There are problems with the supplies?", "Goodbye." },
        new List<string>() { "next", "exit" },

        new List<string>() { "Nevermind.", "Nevermind." },
        new List<string>() { "exit", "exit" }
        )));


        conversations.Add((new Conversation(8, "Repel the cultists", "quest", new List<string>() { "David", "David", "David", "David" },
        new List<string>() {
            "Wha...What was that? \n\nDid you heard that noise?",
            "Someone got inside the station! \n\nStop them! Quickly!\n\n<color=#828282><i>Use the Random battle console until you find cultists.</i></color>"
        },

        new List<string>() { "What noise?", "I'm on my way!" },
        new List<string>() { "next", "quest_add:2" },

        new List<string>() { "No.", "Nevermind." },
        new List<string>() { "exit", "exit" }
        )));

        conversations.Add((new Conversation(9, "Interrogation", "quest", new List<string>() { "Thief", "Thief", "Thief", "Thief" },
        new List<string>() {
            "...I will not say anything to you!\n\n\n...or anyone else...",
            "I'm innocent! I was dragged into this!",
            "If I tell you, you will spare me? Right?",
            "I was with a red haired woman called Kyra. Her friend was in a mask, and used an alias <b>'Delta'</b>."
        },

        new List<string>() { "Are you sure?", "Tell me your companions' name.", "I will.", "You are free to go. " },
        new List<string>() { "next", "next", "next", "finalexit" },

        new List<string>() { "Nevermind.", "Nevermind.", "I can't promise anything.", "You will face trial by combat." },
        new List<string>() { "exit", "exit", "next", "start_battle:4" }
        )));

        conversations.Add((new Conversation(10, "Stolen supplies", "normal", new List<string>() { "Thief" },
       new List<string>() {
            "I've done nothing! Please help me! ",
       },

       new List<string>() { "..." },
       new List<string>() { "none" },

       new List<string>() { "Nevermind." },
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
        }

        if (option.Contains("finalexit"))
        {
            conversation.closeConversation();
            _characterStats.completed_conversations.Add(conversation.conversation_id);
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
            _characterStats.looseMoney(int.Parse(split[1]));
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
            _characterStats.completed_conversations.Add(conversation.conversation_id);
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


        _charName = _characterStats.Player_name;

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


        var _option1 = conversations[id].option_1_type[0];
        string _icon1 = "";

        if (_option1.Contains("next"))
        {
            _icon1 = "¤";
        }
        else if (_option1.Contains("exit") || _option1.Contains("finalexit"))
        {
            _icon1 = "×";
        }
        else if (_option1.Contains("skip") || _option1.Contains("change_dialog"))
        {
            _icon1 = "¡";
        }
        else if (_option1.Contains("start_battle"))
        {
            _icon1 = "¿";
        }
        else if (_option1.Contains("quest_add"))
        {
            _icon1 = "£";
        }

        var _option2 = conversations[id].option_2_type[0];
        string _icon2 = "";

        if (_option2.Contains("next"))
        {
            _icon2 = "¤";
        }
        else if (_option2.Contains("exit") || _option2.Contains("finalexit"))
        {
            _icon2 = "×";
        }
        else if (_option2.Contains("skip") || _option2.Contains("change_dialog"))
        {
            _icon2 = "¡";
        }
        else if (_option2.Contains("start_battle"))
        {
            _icon2 = "¿";
        }
        else if (_option2.Contains("quest_add"))
        {
            _icon2 = "£";
        }

        option_1_button.GetComponent<Text_animation>().startAnim(_icon1 + " " + conversations[id].option_1[0], 0.01f);
        option_2_button.GetComponent<Text_animation>().startAnim(_icon2 + " " + conversations[id].option_2[0], 0.01f);

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

            var _option1 = conversations[conversation_id].option_1_type[dialogue_length];
            string _icon1 = "";

            if (_option1.Contains("next"))
            {
                _icon1 = "¤";
            }
            else if (_option1.Contains("exit") || _option1.Contains("finalexit"))
            {
                _icon1 = "×";
            }
            else if (_option1.Contains("skip") || _option1.Contains("change_dialog"))
            {
                _icon1 = "¡";
            }
            else if (_option1.Contains("start_battle"))
            {
                _icon1 = "¿";
            }
            else if (_option1.Contains("quest_add"))
            {
                _icon1 = "£";
            }

            var _option2 = conversations[conversation_id].option_2_type[dialogue_length];
            string _icon2 = "";

            if (_option2.Contains("next"))
            {
                _icon2 = "¤";
            }
            else if (_option2.Contains("exit") || _option2.Contains("finalexit"))
            {
                _icon2 = "×";
            }
            else if (_option2.Contains("skip") || _option2.Contains("change_dialog"))
            {
                _icon2 = "¡";
            }
            else if (_option2.Contains("start_battle"))
            {
                _icon2 = "¿";
            }
            else if (_option2.Contains("quest_add"))
            {
                _icon2 = "£";
            }

            option_1_button.GetComponent<Text_animation>().startAnim(_icon1 + " " + conversations[conversation_id].option_1[dialogue_length], 0.01f);
            option_2_button.GetComponent<Text_animation>().startAnim(_icon2 + " " + conversations[conversation_id].option_2[dialogue_length], 0.01f);

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
