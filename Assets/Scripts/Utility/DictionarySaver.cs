using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class DictionarySaver
{
    public static void SaveDictionary(string key, Dictionary<ItemType, int> dict)
    {
        string dictString = DictionaryToString(dict);
        PlayerPrefs.SetString(key, dictString);
        PlayerPrefs.Save();
    }
    
    public static Dictionary<ItemType, int> LoadDictionary(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string dictString = PlayerPrefs.GetString(key);
            return StringToDictionary(dictString);
        }
       
        return new Dictionary<ItemType, int>();
        
    }
    
    private static string DictionaryToString(Dictionary<ItemType, int> dict)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var pair in dict)
        {
            sb.Append(pair.Key);
            sb.Append(":");
            sb.Append(pair.Value);
            sb.Append(",");
        }
        return sb.ToString();
    }
    
    private static Dictionary<ItemType, int> StringToDictionary(string dictString)
    {
        Dictionary<ItemType, int> dict = new Dictionary<ItemType, int>();
        string[] pairs = dictString.Split(',');
        foreach (string pair in pairs)
        {
            string[] keyValue = pair.Split(':');
            if (keyValue.Length == 2)
            {
                ItemType key = (ItemType)System.Enum.Parse(typeof(ItemType), keyValue[0]);
                int value = int.Parse(keyValue[1]);
                dict[key] = value;
            }
        }
        return dict;
    }
}
