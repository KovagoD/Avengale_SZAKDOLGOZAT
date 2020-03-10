﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Save_script
{
    public static void savePlayer(Character_stats player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.padoru";
        FileStream stream = new FileStream(path, FileMode.Create);

        CharacterData data = new CharacterData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void saveItems(Item_script itemScript)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/items.padoru";
        FileStream stream = new FileStream(path, FileMode.Create);

        ItemData data = new ItemData(itemScript);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CharacterData loadPlayer()
    {
        string path = Application.persistentDataPath + "/save.padoru";
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
        string path = Application.persistentDataPath + "/items.padoru";
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
}