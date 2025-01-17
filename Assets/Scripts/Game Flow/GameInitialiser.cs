using System;
using Komorebi.Debug;
using UnityEngine;
using Object = System.Object;

namespace Komorebi
{
    public class GameInitialiser : MonoBehaviour
    {
        [SerializeField] private bool _enableDebugModeOnStart = false;
        [SerializeField] private KeyCode _debugToggleKey = KeyCode.Tab;

        private void Awake()
        {
            if (_enableDebugModeOnStart)
            {
                DebugSettings.EnableDebugMode();
            }
        }

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (Input.GetKeyDown(_debugToggleKey))
            {
                DebugSettings.ToggleDebugMode();
            }
        }
    }
}