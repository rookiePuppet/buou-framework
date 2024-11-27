using System;
using UnityEngine;
using UnityEngine.UI;
using BuouFramework.UI;

namespace Game.UI
{
    public class GameView : GameViewBase
    {
        private void Start()
        {
            exitButton.onClick.AddListener(() =>
            {
                UIManager.Instance.Close<GameView>();
            });
        }
    }
}