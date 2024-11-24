using System;

namespace BuouFramework.Settings
{
    [Serializable]
    public class SettingsData
    {
        /*
         * 音频
         */
        public float MusicVolume = 1f;
        public float SoundEffectVolume = 1f;
        /*
         * 显示
         */
        public bool IsFullScreen = true;
        public int ResolutionWidth = 1920;
        public int ResolutionHeight = 1080;
    }
}