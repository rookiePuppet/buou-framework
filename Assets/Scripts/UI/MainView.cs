using BuouFramework;
using BuouFramework.EventSystem;
using BuouFramework.Schedule;
using BuouFramework.Tweening;
using BuouFramework.UI;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MainView : MainViewBase
    {
        [SerializeField] private Text timeText;
        [SerializeField] private TMP_InputField durationInputField;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;

        private CountDownTimer _timer;

        protected override void Start()
        {
            base.Start();

            _timer = new CountDownTimer(this,3);
            _timer.OnStopped(() => { print("计时结束"); })
                .OnTicking(() => { timeText.text = $"{_timer.Time}"; });
            TimerManager.Instance.RegisterTimer(_timer);

            startButton.onClick.AddListener(() =>
            {
                _timer.Reset(int.Parse(durationInputField.text));
                _timer.Start();
            });

            pauseButton.onClick.AddListener(() => { _timer.Pause(); });

            resumeButton.onClick.AddListener(() => { _timer.Resume(); });

            exitButton.onClick.AddListener(() => { UIManager.Instance.Close<MainView>(); });
        }
    }
}