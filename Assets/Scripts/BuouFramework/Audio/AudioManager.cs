using BuouFramework.SingleInstance;
using BuouFramework.Tweening;
using UnityEngine;
using UnityEngine.Pool;

namespace BuouFramework.Audio
{
    /// <summary>
    /// 提供播放音乐/音效的方法，使用对象池管理音频源对象
    /// </summary>
    public class AudioManager : MonoSingleton<AudioManager>
    {
        public const float LongDuration = 8f;
        public const float MiddleDuration = 5f;
        public const float ShortDuration = 2f;

        private const string FreeObjectName = "Free Pool Object";
        private float _musicVolume = 0.8f;
        private float _soundEffectVolume = 0.8f;

        private readonly IObjectPool<AudioSource> _audioPool;
        private AudioSource _currentMusicSource;

        private AudioManager()
        {
            _audioPool = new ObjectPool<AudioSource>(
                () =>
                {
                    var obj = new GameObject();
                    obj.transform.parent = transform;
                    return obj.AddComponent<AudioSource>();
                },
                source =>
                {
                    source.gameObject.SetActive(true);
                    source.volume = SoundEffectVolume;
                },
                source =>
                {
                    source.gameObject.SetActive(false);
                    source.gameObject.name = FreeObjectName;
                });
        }

        /// <summary>
        /// 音乐音量
        /// </summary>
        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                if (_currentMusicSource != null)
                {
                    _currentMusicSource.volume = MusicVolume;
                }
            }
        }

        /// <summary>
        /// 音效音量
        /// </summary>
        public float SoundEffectVolume
        {
            get => _soundEffectVolume;
            set => _soundEffectVolume = value;
        }

        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="clip">音频切片</param>
        /// <param name="overlap">若正在播放相同的音频，是否覆盖（重新开始）播放，默认为否</param>
        /// <param name="loop">是否循环。无论是否覆盖播放，该设置均对当前音频源有效</param>
        public void PlayMusic(AudioClip clip, bool overlap = false, bool loop = true)
        {
            PlayMusicInternal(clip, false, overlap, loop);
        }

        /// <summary>
        /// 播放音乐（渐入）
        /// </summary>
        /// <param name="clip">音频切片</param>
        /// <param name="duration">渐入时长</param>
        /// <param name="easeType">补间函数类型</param>
        /// <param name="overlap">若正在播放相同的音频，是否覆盖（重新开始）播放，默认为是</param>
        /// <param name="loop">是否循环。无论是否覆盖播放，该设置均对当前音频源有效</param>
        /// <returns></returns>
        public Awaitable FadeInMusic(AudioClip clip, float duration, EaseType easeType = EaseType.QuarticIn,
            bool overlap = true, bool loop = true)
        {
            PlayMusicInternal(clip, true, overlap, loop);

            var completeSource = new AwaitableCompletionSource();
            _currentMusicSource.TweenVolume(0f, MusicVolume, duration)
                .SetEase(easeType)
                .Completed += () => { completeSource.SetResult(); };

            return completeSource.Awaitable;
        }

        /// <summary>
        /// 停止当前音乐
        /// </summary>
        public void StopCurrentMusic()
        {
            if (_currentMusicSource != null && _currentMusicSource.isPlaying)
            {
                _currentMusicSource.Stop();
            }
        }

        /// <summary>
        /// 停止当前音乐（渐出）
        /// </summary>
        /// <param name="duration">渐出时长</param>
        /// <param name="easeType">补间函数类型</param>
        /// <returns></returns>
        public Awaitable FadeOutCurrentMusic(float duration, EaseType easeType = EaseType.QuarticOut)
        {
            var completeSource = new AwaitableCompletionSource();
            if (_currentMusicSource != null && _currentMusicSource.isPlaying)
            {
                var tween = _currentMusicSource
                    .TweenVolume(_currentMusicSource.volume, 0f, duration)
                    .SetEase(easeType);
                tween.Completed += () => { completeSource.SetResult(); };
            }
            else
            {
                completeSource.SetResult();
            }

            return completeSource.Awaitable;
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="clip">音频切片</param>
        public async void PlayEffect(AudioClip clip)
        {
            var source = _audioPool.Get();
            source.clip = clip;
            source.gameObject.name = $"[Sound Effect] {clip.name}";
            source.volume = SoundEffectVolume;
            source.Play();

            await Awaitable.WaitForSecondsAsync(clip.length + 0.1f);
            _audioPool.Release(source);
        }

        /// <summary>
        /// 播放音乐的内部方法
        /// </summary>
        /// <param name="clip">音频切片</param>
        /// <param name="fadeIn">是否渐入</param>
        /// <param name="overlap">若正在播放相同的音频，是否覆盖（重新开始）播放，默认为是</param>
        /// <param name="loop">是否循环。无论是否覆盖播放，该设置均对当前音频源有效</param>
        private void PlayMusicInternal(AudioClip clip, bool fadeIn, bool overlap = false, bool loop = true)
        {
            _currentMusicSource ??= _audioPool.Get();

            if (_currentMusicSource.clip == clip)
            {
                _currentMusicSource.loop = loop;
                if (!overlap)
                {
                    return;
                }
            }

            _currentMusicSource.Stop();
            _currentMusicSource.clip = clip;
            _currentMusicSource.loop = loop;
            _currentMusicSource.volume = fadeIn ? 0f : MusicVolume;
            _currentMusicSource.Play();

            _currentMusicSource.gameObject.name = $"[Music] {clip.name}";
        }
    }
}