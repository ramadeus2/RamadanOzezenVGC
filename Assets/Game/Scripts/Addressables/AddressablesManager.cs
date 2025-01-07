
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;
using WheelOfFortune.CurrencySystem;

namespace WheelOfFortune.Utilities {

    public class AddressablesManager: MonoSingleton<AddressablesManager> {
 
        private Dictionary<string, SpriteAtlas> _loadedRewardAtlases;
        private CurrencySettings _loadedCurrencySettings;
        private void OnEnable()
        {
            _loadedRewardAtlases = new Dictionary<string, SpriteAtlas>();

        }
        

       

        public void GetRewardAtlas(System.Action<SpriteAtlas> onAtlasLoaded, string atlasName)
        {
            if(!_loadedRewardAtlases.ContainsKey(atlasName))
            {
                Addressables.LoadAssetAsync<SpriteAtlas>($"Assets/Game/Sprites/DemoSprites/SpriteAtlases/{atlasName}.spriteatlas").Completed += (handle) =>
                {
                    if(handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        _loadedRewardAtlases.Add(atlasName, handle.Result);
                        onAtlasLoaded?.Invoke(handle.Result);
                    }
                };
            } else
            {
                onAtlasLoaded?.Invoke(_loadedRewardAtlases[atlasName]);
            }


        } 

        public void GetCurrencySettings(System.Action<CurrencySettings> onCurrencySettingsLoaded)
        {
            if(!_loadedCurrencySettings)
            {
                Addressables.LoadAssetAsync<CurrencySettings>("Assets/Game/ScriptableObjects/Currency/CurrencySettings.asset").Completed += (handle) =>
                {
                    if(handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        _loadedCurrencySettings = handle.Result;
                        onCurrencySettingsLoaded?.Invoke(_loadedCurrencySettings);
                    }
                };
            }

        }
        public void ReleaseSpriteAtlases()
        {
            foreach(KeyValuePair<string, SpriteAtlas> item in _loadedRewardAtlases)
            {
                Addressables.Release(item);
            }
            _loadedRewardAtlases = null;
        }

        public void ReleaseCurrencySettings()
        {
            Addressables.Release(_loadedCurrencySettings);
            _loadedCurrencySettings = null;
        }

    }

}
