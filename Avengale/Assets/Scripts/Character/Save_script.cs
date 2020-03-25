using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



public static class Save_script
{
    public static void savePlayer(Character_stats player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        CharacterData data = new CharacterData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void saveItems(Item_script itemScript)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/items.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        ItemData data = new ItemData(itemScript);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void saveSpells(Spell_script spellScript)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/spells.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        SpellData data = new SpellData(spellScript);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CharacterData loadPlayer()
    {
        string path = Application.persistentDataPath + "/save.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            CharacterData data = formatter.Deserialize(stream) as CharacterData;


            stream.Close();

            return data;
        }
        else
        {

            Debug.LogError("No save file in: " + path);
            return null;
        }
    }

    public static ItemData loadItems()
    {
        string path = Application.persistentDataPath + "/items.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            ItemData data = formatter.Deserialize(stream) as ItemData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("No item save file in: " + path);
            return null;
        }

    }

    public static SpellData loadSpells()
    {
        string path = Application.persistentDataPath + "/spells.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SpellData data = formatter.Deserialize(stream) as SpellData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("No spell save file in: " + path);
            return null;
        }

    }
}
