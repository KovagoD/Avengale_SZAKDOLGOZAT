using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_manager_script : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    void Awake()
    {
        /*
        COMMANDS:
            -none
            -item_add.item id
            -quest_item_add.item id
            -xp_add.amount

            ID, NAME, IS HUMAN, TYPE, HEALTH, DAMAGE, REWARD, APPEARANCE, ATTACK ANIMATION
        */
        enemies.Add(new Enemy(0, "Angry thug", true, "melee", 50, 5, new int[]{5,0,100,100}, new int[] { 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, "attack_1"));
        enemies.Add(new Enemy(1, "Senko-san", false, "long-range", 100, 10, new int[] {0,11,50,50}, Resources.Load<Sprite>("Enemy_appearances/senkosan_1"), "attack_2"));
        enemies.Add(new Enemy(2, "Unlawful citizen", false, "melee", 200, 50, new int[] {0,0,1000,1000}, Resources.Load<Sprite>("Enemy_appearances/senkosan_2"), "attack_2"));

    }
}

public class Enemy
{
    public int id;
    public string enemy_name;
    public bool isHuman;
    public string type;

    public int health;
    public int damage;

    public int[] rewards;

    public int[] appearance;
    public int[] equipment;
    public Sprite non_human_appearance;
    public string attackAnimation;




    public Enemy(int id, string enemy_name, bool isHuman, string type, int health, int damage, int[] rewards, Sprite non_human_appearance, string attackAnimation)
    {
        this.id = id;
        this.enemy_name = enemy_name;
        this.isHuman = isHuman;
        this.type = type;
        this.health = health;
        this.damage = damage;
        this.rewards = rewards;
        this.non_human_appearance = non_human_appearance;
        this.attackAnimation = attackAnimation;
    }

    public Enemy(int id, string enemy_name, bool isHuman, string type, int health, int damage, int[] rewards, int[] appearance, int[] equipment, string attackAnimation)
    {
        this.id = id;
        this.enemy_name = enemy_name;
        this.isHuman = isHuman;
        this.type = type;
        this.health = health;
        this.damage = damage;
        this.rewards = rewards;

        this.appearance = appearance;
        this.equipment = equipment;
        this.attackAnimation = attackAnimation;
    }


}