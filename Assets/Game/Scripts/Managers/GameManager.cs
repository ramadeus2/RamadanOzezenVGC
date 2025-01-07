using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune.General {

public class GameManager : MonoSingleton<GameManager>
{
        [SerializeField] private GameSettings _gameSettings;
        public GameSettings GameSettings => _gameSettings;
}
}
