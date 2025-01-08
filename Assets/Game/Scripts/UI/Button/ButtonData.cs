using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WheelOfFortune {
    [RequireComponent(typeof(Button))]
public class ButtonData : MonoBehaviour
{ 
        public Button Button ; 
        public TMP_Text ButtonText  ;
        protected virtual void OnValidate()
        {
            Button = GetComponent<Button>();
            ButtonText = GetComponentInChildren<TMP_Text>();
        }
        public void SetButtonText(string text)
        {
            if(ButtonText)
            {
                ButtonText.text = text;
            }
        }
}
}
