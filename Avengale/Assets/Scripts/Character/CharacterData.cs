using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public int Player_max_health, Player_max_resource, Player_damage, Player_money;
    public double Player_plus_money_rate, Player_penalty_rate;
    public List<int> completed_conversations;
    public int[] accepted_quests;
    public List<int> completed_quests;
    public List<int> defeated_enemies;
    public string Player_name, Player_title;
    public int Player_class, Player_talent;
    public bool sex, hideHelmet;
    public int hair_id, eyes_id, nose_id, mouth_id, body_id;
    public byte[] hair_color;

    public int Player_xp, Player_needed_xp, Player_level;
    public int[] Inventory, Equipments;
    public int[] Spells, Talents;
    public int Player_spell_points;
    public CharacterData(Character_stats player)
    {
        //customization
        Player_name = player.Player_name;
        Player_class = player.Player_class;
        Player_talent = player.Player_talent;

        sex = player.sex;
        hideHelmet = player.hideHelmet;

        hair_id = player.hair_id;
        eyes_id = player.eyes_id;
        nose_id = player.nose_id;
        mouth_id = player.mouth_id;
        body_id = player.body_id;

        hair_color = player.hair_color;


        //stats
        Player_xp = player.Player_xp;
        Player_needed_xp = player.Player_needed_xp;
        Player_level = player.Player_level;
        Player_money = player.Player_money;
        Player_plus_money_rate = player.Player_plus_money_rate;
        Player_penalty_rate = player.Player_penalty_rate;
        Inventory = player.Inventory;
        Equipments = player.Equipments;

        Spells = player.Spells;
        Talents = player.Talents;
        Player_spell_points = player.Player_spell_points;


        Player_max_health = player.Player_max_health;
        Player_max_resource = player.Player_max_resource;
        Player_damage = player.Player_damage;
        defeated_enemies = player.defeated_enemies;
        completed_conversations = player.completed_conversations;
        accepted_quests = player.accepted_quests;
        completed_quests = player.completed_quests;
    }
}
