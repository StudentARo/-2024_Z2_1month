using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayerData(SavePlayerData playerData)
    {
        // Convert PlayerData to a string and save it
        string json = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString("playerData", json);
        PlayerPrefs.Save();
        Debug.Log("Player data saved.");
    }

    public static SavePlayerData LoadPlayerData()
    {
        if (PlayerPrefs.HasKey("playerData"))
        {
            string json = PlayerPrefs.GetString("playerData");
            SavePlayerData playerData = JsonUtility.FromJson<SavePlayerData>(json);
            Debug.Log("Player data loaded.");
            return playerData;
        }
        else
        {
            Debug.Log("No player data found.");
            return null; // No saved data
        }
    }
}