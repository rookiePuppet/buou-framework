using System;
using Game.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ButtonSound:MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(() =>
            {
                //AudioManager.Instance.PlayEffect(Resources.Load<AudioClip>("Sounds/ButtonSound"));
            });
        }
    }
}