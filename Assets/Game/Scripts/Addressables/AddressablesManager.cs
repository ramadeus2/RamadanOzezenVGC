using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

namespace WheelOfFortune.Utilities {

    public class AddressablesManager: MonoSingleton<AddressablesManager> {
        [Header("Asset References")]
        [SerializeField] private AssetReferenceAtlasedSprite _rewardAtlasReference;
        private SpriteAtlas _loadedRewardAtlas;
        public void GetRewardAtlas(  System.Action<SpriteAtlas> onAtlasLoaded)
        {
            if(!_loadedRewardAtlas)
            {

                _rewardAtlasReference.LoadAssetAsync<SpriteAtlas>().Completed += (handle) =>
                {
                    if(handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        _loadedRewardAtlas = handle.Result; 
                        onAtlasLoaded?.Invoke(_loadedRewardAtlas);
                    }
                };
            }  
           
        }
        public void ReleaseRewardedAtlas()
        {
            _rewardAtlasReference.ReleaseAsset();
            _loadedRewardAtlas = null;
        }
    }
}
