using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WheelOfFortune.CurrencySystem;
using WheelOfFortune.Reward;

namespace WheelOfFortune.CurrencySystem {
    [CustomEditor(typeof(CurrencyUnit))] 
    public class CurrencyUnitEditor : Editor
{
        private CurrencyUnit _currencyUnit;
        private static bool _hasInitialized = false;
        [InitializeOnLoadMethod]
        private static void OnUnityStartup()
        {
            if(!_hasInitialized)
            {
                _hasInitialized = true;

                var guids = AssetDatabase.FindAssets("t:ScriptableObject");
                foreach(var guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

                    if(asset != null)
                    {
                        var editor = CreateEditor(asset) as CurrencyUnitEditor;
                        editor?.OnUnityStartupCustomMethod();
                    }
                }
            }
        }
        private void OnUnityStartupCustomMethod()
        {
            _currencyUnit = (CurrencyUnit)target;
            _currencyUnit.InitializeId();  

        }
    }
}
