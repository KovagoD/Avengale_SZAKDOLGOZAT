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
        enemies.AddRange(new List<Enemy>()
        {
            {new Enemy(0, "", true, "", 0, 0, new int[] { 0, 0, 0, 0 },false,  new int[] { 0, 0, 0, 0, 0 }, new byte[]{0,0,0}, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 }, "")},
            {new Enemy(1, "Cultist raider", true, "melee", 50, 5, new int[] { 5, 0, 100, 100 }, true, new int[] { 0, 2, 3, 4, 5, 6, 7, 8 }, "attack_1")},
            {new Enemy(2, "Senko-san", false, "long-range", 100, 10, new int[] { 0, 11, 50, 50 }, "Enemy_appearances/senkosan_1", "attack_2")},
            {new Enemy(3, "Unlawful citizen", false, "melee", 200, 50, new int[] { 0, 0, 1000, 1000 }, "Enemy_appearances/senkosan_2", "attack_2")},
            {new Enemy(4, "Recruit", true, "melee", 10, 10, new int[] { 0, 0, 0, 0 }, true, new int[] { 0, 9, 10, 4, 5, 6, 7, 11 }, "attack_1")},
        });
    }
}
[System.Serializable]
public class Enemy
{
    public int id;
    public string enemy_name;
    public bool isHuman;
    public string type;

    public int health;
    public int damage;

    public int[] rewards;

    public bool sex;
    public int[] appearance = new int[5] { 0, 0, 0, 0, 0 };
    public byte[] hair_color = new byte[3] { 0, 0, 0 };

    public bool isRandomAppearance = false;

    public int[] equipment;
    public string non_human_appearance;
    public string attackAnimation;


    public void randomizeAppearance()
    {
        int hair_length = 3, eyes_length = 4, nose_length = 2, mouth_length = 3, body_length = 3;
        if (isRandomAppearance)
        {
            appearance[0] = Random.Range(0, hair_length + 1);
            appearance[1] = Random.Range(0, eyes_length + 1);
            appearance[2] = Random.Range(0, nose_length + 1);
            appearance[3] = Random.Range(0, mouth_length + 1);
            appearance[4] = Random.Range(0, body_length + 1);

            hair_color[0] = (byte)Random.Range(0, 256);
            hair_color[1] = (byte)Random.Range(0, 256);
            hair_color[2] = (byte)Random.Range(0, 256);
        }
    }

    public Enemy(int id, string enemy_name, bool isHuman, string type, int health, int damage, int[] rewards, string non_human_appearance, string attackAnimation)
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

    public Enemy(int id, string enemy_name, bool isHuman, string type, int health, int damage, int[] rewards, bool sex, int[] appearance, byte[] hair_color, int[] equipment, string attackAnimation)
    {
        this.id = id;
        this.enemy_name = enemy_name;
        this.isHuman = isHuman;
        this.type = type;
        this.health = health;
        this.damage = damage;
        this.rewards = rewards;

        this.sex = sex;
        this.appearance = appearance;
        this.hair_color = hair_color;
        this.equipment = equipment;
        this.attackAnimation = attackAnimation;
    }

    public Enemy(int id, string enemy_name, bool isHuman, string type, int health, int damage, int[] rewards, bool isRandomAppearance, int[] equipment, string attackAnimation)
    {
        this.id = id;
        this.enemy_name = enemy_name;
        this.isHuman = isHuman;
        this.type = type;
        this.health = health;
        this.damage = damage;
        this.rewards = rewards;

        this.isRandomAppearance = isRandomAppearance;
        this.equipment = equipment;
        this.attackAnimation = attackAnimation;
    }

}