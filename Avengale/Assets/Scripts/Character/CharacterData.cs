using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public int Local_max_health, Local_max_resource, Local_damage, Local_money;
    public List<int> completed_conversations;
    public int[] accepted_quests;
    public List<int> completed_quests;
    public List<int> defeated_enemies;
    public string Local_name, Local_title;
    public int Local_class, Local_talent;
    public bool sex, hideHelmet;
    public int hair_id, eyes_id, nose_id, mouth_id, body_id;
    public byte[] hair_color;

    public int Local_xp, Local_needed_xp, Local_level;
    public int[] Inventory, Equipments;
    public int[] Spells, Talents;
    public int Local_spell_points;
    public CharacterData(Character_stats player)
    {
        //customization
        Local_name = player.Local_name;
        Local_title = player.Local_title;
        Local_class = player.Local_class;
        Local_talent = player.Local_talent;

        sex = player.sex;
        hideHelmet = player.hideHelmet;
        
        hair_id = player.hair_id;
        eyes_id = player.eyes_id;
        nose_id = player.nose_id;
        mouth_id = player.mouth_id;
        body_id = player.body_id;

        hair_color = player.hair_color;


        //stats
        Local_xp = player.Local_xp;
        Local_needed_xp = player.Local_needed_xp;
        Local_level = player.Local_level;
        Local_money = player.Local_money;
        Inventory = player.Inventory;
        Equipments = player.Equipments;

        Spells = player.Spells;
        Talents = player.Talents;
        Local_spell_points = player.Local_spell_points;


        Local_max_health = player.Local_max_health;
        Local_max_resource = player.Local_max_resource;
        Local_damage = player.Local_damage;
        defeated_enemies = player.defeated_enemies;
        completed_conversations = player.completed_conversations;
        accepted_quests = player.accepted_quests;
        completed_quests = player.completed_quests;
    }
}
