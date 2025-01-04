
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

namespace WheelOfFortune {

    [CustomEditor(typeof(RewardData))]
    public class RewardDataEditor: AbstractRewardSystemEditor {
        [SerializeField] private RewardPool _rewardPool; 
        RewardData rewardData;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            rewardData = (RewardData)target;
            rewardData.SetRewardName();

            Sprite spritePreview = SetSprite(rewardData);
            VisualizeRewardSprite(spritePreview);

            if(GUILayout.Button("Add Or Update Me To Pool"))
            {
                rewardData.InitializeId();
                _rewardPool.AddOrUpdateReward(rewardData);
            }
        }
       

    }
}
