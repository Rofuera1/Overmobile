using System;
using UnityEngine;

namespace MainCore
{
    public class GameCycleController : MonoBehaviour
    {
        public GameCycleType CurrentType => _currentType;
        
        private GameCycleType _currentType;

        private void Awake()
        {
            _currentType = GameCycleType.Player;
        }
    }
}