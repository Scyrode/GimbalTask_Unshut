using System.Linq;
using TMPro;
using UnityEngine;
using System;
using AhmadAllahham.Shared;

/*
 * Logger class, adapted from dilmerv's Logger class
 * Original code: https://github.com/dilmerv/UnityMultiplayerPlayground/blob/master/Assets/Scripts/Core/Logger.cs
 * Date of Retreival: 26/01/2024
 */
namespace AhmadAllahham.Core
{
    public class Logger : Singleton<Logger>
    {
        [SerializeField] private int maxLines = 12;
        [SerializeField] private bool _enableDebug = false;

        private TextMeshProUGUI _debugAreaText;

        void Awake()
        {
            Debug.Log("Starting logger");
            _debugAreaText = GetComponent<TextMeshProUGUI>();
            _debugAreaText.text = string.Empty;
        }

        void OnEnable()
        {
            _debugAreaText.enabled = _enableDebug;
            enabled = _enableDebug;

            if (enabled)
            {
                _debugAreaText.text += $"<color=\"white\">{DateTime.Now.ToString("HH:mm:ss.fff")} {this.GetType().Name} enabled</color>\n";
            }
        }

        public void LogInfo(string message)
        {
            ClearLines();

            _debugAreaText.text += $"<color=\"green\">{DateTime.Now.ToString("HH:mm:ss.fff")} {message}</color>\n";
        }

        public void LogError(string message)
        {
            ClearLines();
            _debugAreaText.text += $"<color=\"red\">{DateTime.Now.ToString("HH:mm:ss.fff")} {message}</color>\n";
        }

        public void LogWarning(string message)
        {
            ClearLines();
            _debugAreaText.text += $"<color=\"yellow\">{DateTime.Now.ToString("HH:mm:ss.fff")} {message}</color>\n";
        }

        private void ClearLines()
        {
            if (_debugAreaText.text.Split('\n').Count() >= maxLines)
            {
                _debugAreaText.text = string.Empty;
            }
        }
    }
}