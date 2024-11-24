using UnityEngine;

namespace Game.Framework.Settings
{
    public class SettingsManager : Singleton<SettingsManager>
    {
        private const string Key = "settings";

        private SettingsManager()
        {
            if (PlayerPrefs.HasKey(Key))
            {
                var json = PlayerPrefs.GetString(Key);
                Data = JsonUtility.FromJson<SettingsData>(json);
            }
            else
            {
                InitializeData();
            }
        }

        public SettingsData Data { get; private set; }

        public void InitializeData()
        {
            Data = new SettingsData();
            var currentResolution = Screen.currentResolution;
            Data.ResolutionWidth = currentResolution.width;
            Data.ResolutionHeight = currentResolution.height;
        }

        public void SaveData()
        {
            var json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(Key, json);
            PlayerPrefs.Save();
        }
    }
}