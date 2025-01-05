using System.Collections.Generic; 
using UnityEditor;
using UnityEngine; 

namespace WheelOfFortune.Reward {

    [CustomEditor(typeof(RewardPool))]
    public class RewardPoolEditor: AbstractRewardSystemEditor {
        [SerializeField] private DefaultAsset _normalRewardTargetFolder;
        [SerializeField] private DefaultAsset _specialRewardTargetFolder;
        [SerializeField] private bool _simpleMode;
        //[SerializeField] private DefaultAsset _targetFolder;
        private RewardPool _rewardPool;
        private bool[] _normalRewardFoldouts;
        private bool[] _specialRewardFoldouts;
        public override void OnInspectorGUI()
        {
           _simpleMode = EditorGUILayout.Toggle("Simple Mode",_simpleMode);
            if(_simpleMode)
            {
                base.OnInspectorGUI();
                return;
            }
            _rewardPool = (RewardPool)target;

            InitializeRewardFoldouts();

            EditorGUILayout.LabelField("Reward Pool Editor", EditorStyles.boldLabel);

            EditorGUILayout.Space();


            EditorGUILayout.LabelField("Normal Rewards", EditorStyles.boldLabel);
            VisualizeRewardList(_rewardPool.NormalRewardDatas, _normalRewardFoldouts);
            AddNormalRewardButton();


            EditorGUILayout.LabelField("Special Rewards", EditorStyles.boldLabel);
            VisualizeRewardList(_rewardPool.SpecialRewardDatas, _specialRewardFoldouts);
            AddSpecialRewardButton();

            EditorGUILayout.Space();
            AddAllPossibleRewardsAsNormal();
            if(GUI.changed)
            {
                EditorUtility.SetDirty(_rewardPool);
            }
        }

        private void InitializeRewardFoldouts()
        {
            if(_rewardPool.NormalRewardDatas != null)
            {
                if(_normalRewardFoldouts == null || _normalRewardFoldouts.Length != _rewardPool.NormalRewardDatas.Count)
                {
                    _normalRewardFoldouts = new bool[_rewardPool.NormalRewardDatas.Count];
                    //for(int i = 0; i < _normalRewardFoldouts.Length; i++)
                    //{
                    //    _normalRewardFoldouts[i] = true;
                    //}
                }
            }
            if(_rewardPool.SpecialRewardDatas != null)
            {
                if(_specialRewardFoldouts == null || _specialRewardFoldouts.Length != _rewardPool.SpecialRewardDatas.Count)
                {
                    _specialRewardFoldouts = new bool[_rewardPool.SpecialRewardDatas.Count];
                    //for(int i = 0; i < _specialRewardFoldouts.Length; i++)
                    //{
                    //    _specialRewardFoldouts[i] = true;
                    //}
                }
            }
        }

        private void VisualizeRewardList(List<RewardData> rewardDatas, bool[] rewardFoldouts)
        {
            if(rewardDatas == null)
            {
                return;
            }
            for(int i = 0; i < rewardDatas.Count; i++)
            {
                RewardData reward = rewardDatas[i];

                EditorGUILayout.BeginVertical("box");

                rewardFoldouts[i] = EditorGUILayout.Foldout(rewardFoldouts[i], reward.RewardName, true);

                //if(rewardFoldouts[i])
                //{
                    rewardDatas[i] = (RewardData)EditorGUILayout.ObjectField(
                        "Reward Reference",
                        rewardDatas[i],
                        typeof(RewardData),
                        false
                    );

                    if(reward != null)
                    {
                        SerializedObject rewardSerialized = new SerializedObject(reward);

                        SerializedProperty prop = rewardSerialized.GetIterator();

                        prop.Next(true);
                        prop.NextVisible(true);
                        while(prop.NextVisible(false))
                        {
                            EditorGUILayout.PropertyField(prop, true);
                        }
                        reward.SetRewardName();

                        SetSprite(reward);
                        Sprite spritePreview = _spriteAtlas.GetSprite(reward.SpriteName);
                        VisualizeRewardSprite(spritePreview);

                        if(rewardSerialized.ApplyModifiedProperties())
                        {
                            EditorUtility.SetDirty(reward);
                        }

                    } else
                    {
                        RemoveReward(rewardDatas, rewardFoldouts, i);
                    }
                    EditorGUILayout.Space(5);

                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if(GUILayout.Button("Move Upper", GUILayout.Width(120), GUILayout.Height(20)))
                    {
                        if(i > 0)
                        {
                            RewardData temp = rewardDatas[i - 1];
                            rewardDatas[i - 1] = rewardDatas[i];
                            rewardDatas[i] = temp;
                        }
                        break;
                    }
                    GUILayout.FlexibleSpace();

                    if(GUILayout.Button("Remove Reward From The List", GUILayout.Width(240), GUILayout.Height(20)))
                    {
                        RemoveReward(rewardDatas, rewardFoldouts, i);
                        break;
                    }
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();


                    if(GUILayout.Button("Move Lower", GUILayout.Width(120), GUILayout.Height(20)))
                    {
                        if(i >= 0 && i < rewardDatas.Count - 1)
                        {
                            RewardData temp = rewardDatas[i + 1];
                            rewardDatas[i + 1] = rewardDatas[i];
                            rewardDatas[i] = temp;
                        }
                        break;
                    }
                    GUILayout.FlexibleSpace();

                    if(GUILayout.Button("Completely Delete Reward Data", GUILayout.Width(240), GUILayout.Height(20)))
                    {
                        CompletelyRemoveReward(rewardDatas, rewardFoldouts, i);
                        break;
                    }
                    GUILayout.FlexibleSpace();

                    GUILayout.EndHorizontal();

                //}
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }
        }

        private void CompletelyRemoveReward(List<RewardData> rewardDatas, bool[] rewardFoldouts, int i)
        {
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(rewardDatas[i]));
            RemoveReward(rewardDatas, rewardFoldouts, i);
        }

        private void AddNormalRewardButton()
        {
            if(GUILayout.Button("Add Normal Reward"))
            {
                RewardData newReward = CreateInstance<RewardData>();
                string folderPath = AssetDatabase.GetAssetPath(_normalRewardTargetFolder);
                string fullPath = $"{folderPath}/NormalReward_{_rewardPool.NormalRewardDatas.Count + 1}.asset";
                AssetDatabase.CreateAsset(newReward, fullPath);
                AssetDatabase.SaveAssets();

                _rewardPool.NormalRewardDatas.Add(newReward);
                ArrayUtility.Add(ref _normalRewardFoldouts, true);

                EditorUtility.SetDirty(_rewardPool);
            }
        }
        private void AddSpecialRewardButton()
        {
            if(GUILayout.Button("Add Special Reward"))
            {
                RewardData newReward = CreateInstance<RewardData>();
                string folderPath = AssetDatabase.GetAssetPath(_specialRewardTargetFolder);
                string fullPath = $"{folderPath}/SpecialReward_{_rewardPool.SpecialRewardDatas.Count + 1}.asset";
                AssetDatabase.CreateAsset(newReward, fullPath);
                AssetDatabase.SaveAssets();

                _rewardPool.SpecialRewardDatas.Add(newReward);
                ArrayUtility.Add(ref _specialRewardFoldouts, true);

                EditorUtility.SetDirty(_rewardPool);
            }
        }
        private void AddAllPossibleRewardsAsNormal()
        {
            if(GUILayout.Button("Add All As Normal Rewards"))
            {
                for(int i = 0; i < _rewardPool.NormalRewardDatas.Count; i++)
                {
                    CompletelyRemoveReward(_rewardPool.NormalRewardDatas, _normalRewardFoldouts, i);
                }
                for(int i = 0; i < _spriteAtlas.spriteCount; i++)
                {

                    RewardData newReward = CreateInstance<RewardData>();
                    string folderPath = AssetDatabase.GetAssetPath(_normalRewardTargetFolder);
                    string fullPath = $"{folderPath}/NormalReward_{_rewardPool.NormalRewardDatas.Count + 1}.asset";
                    if(i < _spriteAtlas.spriteCount)
                    {
                    newReward.selectedSpriteIndex= i;
                    }
                    AssetDatabase.CreateAsset(newReward, fullPath);
                    AssetDatabase.SaveAssets();

                    _rewardPool.NormalRewardDatas.Add(newReward);
                    ArrayUtility.Add(ref _normalRewardFoldouts, true);

                    EditorUtility.SetDirty(_rewardPool);
                }
            }
        }

        private void RemoveReward(List<RewardData> rewardDatas, bool[] rewardFoldouts, int i)
        {
            rewardDatas.RemoveAt(i);
            ArrayUtility.RemoveAt(ref rewardFoldouts, i);

            EditorUtility.SetDirty(_rewardPool);
        }
    }
}
