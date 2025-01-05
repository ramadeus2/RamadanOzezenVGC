using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;
using WheelOfFortune.Reward;
using WheelOfFortune.Stage;

namespace WheelOfFortune.Utilities {

    public class AddressablesManager: MonoSingleton<AddressablesManager> {
        [Header("Asset References")]
        [SerializeField] private AssetReferenceAtlasedSprite _rewardAtlasReference;
        [SerializeField] private AssetReference _rewardPoolReference;
        [SerializeField] private AssetReference _stagePoolReference;
        private SpriteAtlas _loadedRewardAtlas;
        private RewardPool _loadedRewardPool;
        private StagePool _loadedStagePool;
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
        public void GetRewardPool(System.Action<RewardPool> onRewardPoolLoaded)
        {
            if(!_loadedRewardPool)
            {

                _rewardPoolReference.LoadAssetAsync<RewardPool>().Completed += (handle) =>
                {
                    if(handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        _loadedRewardPool = handle.Result;
                        onRewardPoolLoaded?.Invoke(_loadedRewardPool);
                    }
                };
            }

        }
        public void GetStagePool(System.Action<StagePool> onStagePoolLoaded)
        {
            if(!_loadedStagePool)
            {

                _stagePoolReference.LoadAssetAsync<StagePool>().Completed += (handle) =>
                {
                    if(handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        _loadedStagePool = handle.Result;
                        onStagePoolLoaded?.Invoke(_loadedStagePool);
                    }
                };
            }

        }
        public void ReleaseRewardedAtlas()
        {
            _rewardAtlasReference.ReleaseAsset();
            _loadedRewardAtlas = null;
        }
        public void ReleaseRewardPool()
        {
            _rewardPoolReference.ReleaseAsset();
            _loadedRewardPool = null;
        }
        public void ReleaseStagePool()
        {
            _stagePoolReference.ReleaseAsset();
            _loadedStagePool = null;
        }
    }

}
