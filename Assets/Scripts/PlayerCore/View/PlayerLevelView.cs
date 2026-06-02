using System;
using TMPro;
using UnityEngine;

namespace PlayerCore
{
    public class PlayerLevelView : MonoBehaviour
    {
        [SerializeField] private Transform _canvas;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Transform _player;
        [Space]
        [SerializeField] private PlayerModel _playerModel;

        private Vector3 _offset;
        private Vector3 _refPos;

        private void Awake()
        {
            _offset = _canvas.position - _player.position;
            _playerModel.LeveledUp += SetLevel;
        }

        private void LateUpdate()
        {
            _canvas.position = Vector3.SmoothDamp(_canvas.position, _player.position + _offset, ref _refPos, 0.1f);
        }

        private void SetLevel() => _text.text = _playerModel.Level.ToString();
    }
}