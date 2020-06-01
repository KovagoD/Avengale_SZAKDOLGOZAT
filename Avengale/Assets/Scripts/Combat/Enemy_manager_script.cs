using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_manager_script : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    void Awake()
    {
        enemies.AddRange(new List<Enemy>()
        {
            {new Enemy(0, "", true, new int[] { 0, 0, 0, 0 },false,  new int[] { 0, 0, 0, 0, 0 }, new byte[]{0,0,0}, new int[] { 0, 0, 0, 0, 0, 0, 0, 0 }, "")},
            {new Enemy(1, "Cultist raider", true, new int[] { 0, 0, 100, 100 }, true, new int[] { 0, 2, 3, 0, 5, 6, 7, 15 }, "attack_3")},
            {new Enemy(2, "Corrupted console", false,  new int[] { 0, 11, 50, 50 }, "Enemy_appearances/corrupted_console", "attack_2")},
            {new Enemy(3, "Cultist leader", true, new int[] { 0, 0, 100, 100 }, true, new int[] { 1, 2, 3, 4, 13, 6, 7, 14 }, "attack_1")},
            {new Enemy(4, "Recruit", true, new int[] { 0, 0, 20, 0 }, true, new int[] { 0, 9, 10, 4, 0, 0, 12, 8  }, "attack_2")},
            {new Enemy(5, "Thief", true, new int[] { 0, 0, 20, 0 }, false, new int[] { 1, 2, 1, 3, 0 }, new byte[]{64,95,113}, new int[] { 0, 9, 10, 0, 13, 0, 12, 0  }, "attack_1")},
            {new Enemy(6, "Supply looter", true, new int[] { 0, 0, 20, 0 }, true, new int[] { 0, 9, 10, 0, 13, 0, 12, 15}, "attack_3")},
        });
    }
}
[System.Serializable]
public class Enemy
{
    public int id;
    public string enemy_name;
    public bool isHuman;
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
        int hair_length = 8, eyes_length = 4, nose_length = 3, mouth_length = 3, body_length = 3;
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

    public Enemy(int id, string enemy_name, bool isHuman, int[] rewards, string non_human_appearance, string attackAnimation)
    {
        this.id = id;
        this.enemy_name = enemy_name;
        this.isHuman = isHuman;
        this.rewards = rewards;
        this.non_human_appearance = non_human_appearance;
        this.attackAnimation = attackAnimation;
    }

    public Enemy(int id, string enemy_name, bool isHuman, int[] rewards, bool sex, int[] appearance, byte[] hair_color, int[] equipment, string attackAnimation)
    {
        this.id = id;
        this.enemy_name = enemy_name;
        this.isHuman = isHuman;
        this.rewards = rewards;
        this.sex = sex;
        this.appearance = appearance;
        this.hair_color = hair_color;
        this.equipment = equipment;
        this.attackAnimation = attackAnimation;
    }

    public Enemy(int id, string enemy_name, bool isHuman, int[] rewards, bool isRandomAppearance, int[] equipment, string attackAnimation)
    {
        this.id = id;
        this.enemy_name = enemy_name;
        this.isHuman = isHuman;
        this.rewards = rewards;
        this.isRandomAppearance = isRandomAppearance;
        this.equipment = equipment;
        this.attackAnimation = attackAnimation;
    }

}