using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace WheelOfFortune.General {

public class GameManager : MonoSingleton<GameManager>
{
        [SerializeField] private GameSettings _gameSettings;
       
        public GameSettings GameSettings => _gameSettings;
}
}
