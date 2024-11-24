using BuouFramework;
using BuouFramework.EventSystem;
using BuouFramework.Tweening;
using BuouFramework.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MainView : MainViewBase
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TMP_InputField inputField;

        protected override void Start()
        {
            base.Start();
            startButton.onClick.AddListener(
                EventCenter.Trigger<ButtonClickEvent>);
            exitButton.onClick.AddListener(() => { UIManager.Instance.Close<MainView>(); });
        }
    }
}