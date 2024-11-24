using System;
using Game.Framework;
using Game.Framework.EventSystem;
using Game.Framework.UI;
using Game.UI;
using UnityEngine;

namespace Game
{
    public class Main : MonoBehaviour
    {
        public EventChanel<int> testEventChanel;
        public EventChanel gameStartEventChanel;
        public AudioClip[] testMusics;
        public AudioClip[] testSounds;
        public float duration = 2f;

        private bool _isGameScene;

        private void OnEnable()
        {
            EventCenter.AddListener<StartGameEvent, StartGameEvent.Args>(args => { Log.Info($"{args.Time}"); });
            EventCenter.AddListener<ButtonClickEvent>(() => { Log.Info("Clicked"); });
        }

        private void Start()
        {
            UIManager.Instance.Open<MainView>();
            // gameStartEventChanel.Broadcast(new Empty());
            // testEventChanel.Broadcast(DateTime.Now.Day);

            EventCenter.Get<StartGameEvent>().Trigger(new StartGameEvent.Args { Time = DateTime.Now.Ticks });
        }

        public void OnGameStart()
        {
            Log.Info("Game started");
        }

        public void OnTest()
        {
            Log.Info("Test");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UIManager.Instance.Open<MainView>();
            }
        }
    }

    public class ButtonClickEvent : Framework.EventSystem.Event
    {
    }

    public class StartGameEvent : Event<StartGameEvent.Args>
    {
        public struct Args
        {
            public float Time;
        }
    }
}