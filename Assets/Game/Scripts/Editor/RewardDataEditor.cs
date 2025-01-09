using UnityEditor;
using UnityEngine; 

namespace WheelOfFortune.Reward {

    [CustomEditor(typeof(RewardData))]
    public class RewardDataEditor: AbstractRewardSystemEditor {
        [SerializeField] private RewardPool _rewardPool;
        private RewardData _rewardData;
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
                        var editor = CreateEditor(asset) as RewardDataEditor;
                        editor?.OnUnityStartupCustomMethod();
                    }
                }
            }
        }
        private void OnUnityStartupCustomMethod()
        {
            _rewardData = (RewardData)target;
            _rewardData.InitializeId();
            _rewardData.SetRewardName();
            Sprite spritePreview = SetSprite(_rewardData, false);

        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            _rewardData = (RewardData)target;
            _rewardData.InitializeId();
            _rewardData.SetRewardName();

            Sprite spritePreview = SetSprite(_rewardData,true);
            VisualizeRewardSprite(spritePreview);

            
        }
       

    }
}
