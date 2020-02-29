using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    [Header("Combat")]
    public int Local_max_health;
    public int Local_max_resource;
    public int Local_damage;


    [Header("Completed things")]

    //public List<Enemy> defeated_enemies;
    public List<Conversation> completed_conversations;
    public int[] accepted_quests;
    public List<Quest> completed_quests;

    [Header("Customization")]
    public string Local_name;
    public string Local_title;
    public int Local_class;
    public int Local_talent;

    public int hair_id;
    public int eyes_id;
    public int nose_id;
    public int mouth_id;
    public int body_id;

    [Header("Stats")]
    public int Local_xp;
    public int Local_needed_xp;
    public int Local_level;
    public int Local_money;

    [Header("Inventory")]
    public int[] Inventory;
    public int[] Equipments;

    [Header("Spells")]
    public int[] Spells;

    public CharacterData(Character_stats player)
    {
        //customization
        Local_name = player.Local_name;
        Local_title = player.Local_title;
        Local_class = player.Local_class;
        Local_talent = player.Local_talent;
        hair_id = player.hair_id;
        eyes_id = player.eyes_id;
        nose_id = player.nose_id;
        mouth_id = player.mouth_id;
        body_id = player.body_id;

        //stats
        Local_xp = player.Local_xp;
        Local_needed_xp = player.Local_needed_xp;
        Local_level = player.Local_level;
        Local_money = player.Local_money;
        Inventory = player.Inventory;
        Equipments = player.Equipments;
        Spells = player.Spells;
        Local_max_health = player.Local_max_health;
        Local_max_resource = player.Local_max_resource;
        Local_damage = player.Local_damage;
        //defeated_enemies = player.defeated_enemies;
        completed_conversations = player.completed_conversations;
        accepted_quests = player.accepted_quests;
        completed_quests = player.completed_quests;
    }
}
