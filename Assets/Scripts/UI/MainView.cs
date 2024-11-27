using BuouFramework;
using BuouFramework.EventSystem;
using BuouFramework.Logging;
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

        protected void Start()
        {
            // _timer = new CountDownTimer(this,3);
            // _timer.OnStopped(() => { print("计时结束"); })
            //     .OnTicking(() => { timeText.text = $"{_timer.Time}"; });
            // TimerManager.Instance.RegisterTimer(_timer);

            startButton.onClick.AddListener(() => { UIManager.Instance.Open<GameView>(); });

            // pauseButton.onClick.AddListener(() => { _timer.Pause(); });
            //
            // resumeButton.onClick.AddListener(() => { _timer.Resume(); });

            exitButton.onClick.AddListener(() => { UIManager.Instance.Destroy<MainView>(); });
        }

        public override void OnShow()
        {
            base.OnShow();
            Log.Info("OnShow", this);
        }

        public override void OnHide()
        {
            base.OnHide();
            Log.Info("OnHide", this);
        }

        public override void AfterShowEffect()
        {
            base.AfterShowEffect();
            Log.Info("AfterShowEffect", this);
        }

        public override void AfterHideEffect()
        {
            base.AfterHideEffect();
            Log.Info("AfterHideEffect", this);
        }
    }
}