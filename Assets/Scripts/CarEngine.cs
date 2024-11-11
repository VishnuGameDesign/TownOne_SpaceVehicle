using System;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class CarEngine : MonoBehaviour, IInteractable
    {
        [SerializeField] private Player _player;
        [field: SerializeField] private GameObject _carEngineCanvas;
        
        [SerializeField] private GameObject _draggableUI;
        private bool _displayCanvas;

        private void OnValidate()
        {
            _carEngineCanvas.SetActive(false);
        }

        public void CheckForToolMatch(InputValue value)
        {
            _displayCanvas = !_displayCanvas;
            if(_displayCanvas) _carEngineCanvas.SetActive(true);
            else _carEngineCanvas.SetActive(false);
        }
    }
}