using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Reward {

    [CustomEditor(typeof(RewardPool))]
    public class RewardPoolEditor: AbstractRewardSystemEditor {
        [SerializeField] private DefaultAsset _normalRewardTargetFolder;
        [SerializeField] private DefaultAsset _specialRewardTargetFolder;
        [SerializeField] private DefaultAsset _currenciesTargetFolder;
        [SerializeField] private DefaultAsset _bombTargetFolder;
        [SerializeField] private bool _simpleMode;
        //[SerializeField] private DefaultAsset _targetFolder;
        private RewardPool _rewardPool;
        private bool[] _normalRewardFoldouts;
        private bool[] _currencyRewardFoldouts;
        private bool[] _specialRewardFoldouts;
        private bool _allNormalRewardsFoldout;
        private bool _allSpecialRewardsFoldout;
        private bool _allCurrenciesFoldout;
        private bool _bombFoldout;

        public override void OnInspectorGUI()
        {
            _simpleMode = EditorGUILayout.Toggle("Simple Mode", _simpleMode);
            if(_simpleMode)
            {
                base.OnInspectorGUI();
                return;
            }
            _rewardPool = (RewardPool)target;

            InitializeRewardFoldouts();

            EditorGUILayout.LabelField("Reward Pool Editor", EditorStyles.boldLabel);

            EditorGUILayout.Space();


            _allNormalRewardsFoldout = EditorGUILayout.Foldout(_allNormalRewardsFoldout, "All Normal Rewards", true);
            if(_allNormalRewardsFoldout)
            {
                EditorGUILayout.LabelField("Normal Rewards", EditorStyles.boldLabel);

                VisualizeRewardList(_rewardPool.NormalRewardDatas, _normalRewardFoldouts);
                AddNormalRewardButton();
                AddAllRewards(RewardType.Normal);

            }
            _allSpecialRewardsFoldout = EditorGUILayout.Foldout(_allSpecialRewardsFoldout, "All Special Rewards", true);
            if(_allSpecialRewardsFoldout)
            {
                EditorGUILayout.LabelField("Special Rewards", EditorStyles.boldLabel);
                VisualizeRewardList(_rewardPool.SpecialRewardDatas, _specialRewardFoldouts);
                AddSpecialRewardButton();
                AddAllRewards(RewardType.Special);
            }

            _allCurrenciesFoldout = EditorGUILayout.Foldout(_allCurrenciesFoldout, "All Currencies", true);
            if(_allCurrenciesFoldout)
            {
                EditorGUILayout.LabelField("Currencies", EditorStyles.boldLabel);
                VisualizeRewardList(_rewardPool.CurrencyDatas, _currencyRewardFoldouts);
                AddCurrencyButton();
                AddAllRewards(RewardType.Currency);
            }
            _bombFoldout = EditorGUILayout.Foldout(_bombFoldout, "Bomb", true);
            if(_bombFoldout)
            {
                VisualizeBomb();

                AddBomb();
            }
            EditorGUILayout.Space();
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
                    for(int i = 0; i < _normalRewardFoldouts.Length; i++)
                    {
                        _normalRewardFoldouts[i] = true;
                    }
                }
            }
            if(_rewardPool.SpecialRewardDatas != null)
            {
                if(_specialRewardFoldouts == null || _specialRewardFoldouts.Length != _rewardPool.SpecialRewardDatas.Count)
                {
                    _specialRewardFoldouts = new bool[_rewardPool.SpecialRewardDatas.Count];
                    for(int i = 0; i < _specialRewardFoldouts.Length; i++)
                    {
                        _specialRewardFoldouts[i] = true;
                    }
                }
            }
            if(_rewardPool.CurrencyDatas != null)
            {
                if(_currencyRewardFoldouts == null || _currencyRewardFoldouts.Length != _rewardPool.CurrencyDatas.Count)
                {
                    _currencyRewardFoldouts = new bool[_rewardPool.CurrencyDatas.Count];
                    for(int i = 0; i < _currencyRewardFoldouts.Length; i++)
                    {
                        _currencyRewardFoldouts[i] = true;
                    }
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

                if(rewardFoldouts[i])
                {
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
                        reward.InitializeId();

                        SetSprite(reward,true);
                        SpriteAtlas spriteAtlas = _normalRewardSpriteAtlas;
                        bool isBomb = false;
                        switch(reward.RewardType)
                        {
                            case RewardType.Normal:
                                spriteAtlas = _normalRewardSpriteAtlas;
                                break;
                            case RewardType.Currency:
                                spriteAtlas = _currencySpriteAtlas;
                                break;
                            case RewardType.Special:
                                spriteAtlas = _specialRewardSpriteAtlas;
                                break;
                            case RewardType.Bomb:
                                isBomb = true;
                                break;
                            default:
                                break;
                        }
                        Sprite spritePreview = null; 
                        if(isBomb)
                        {
                            spritePreview = _bombSprite;

                        } else
                        {
                            spritePreview= spriteAtlas.GetSprite(reward.SpriteName);
                        }
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

                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }
        }
        private void VisualizeBomb()
        {
            RewardData reward = _rewardPool.BombData;

            EditorGUILayout.BeginVertical("box");



                 reward = (RewardData)EditorGUILayout.ObjectField(
                    "Reward Reference",
                    reward,
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
                reward.InitializeId();

                SetSprite(reward,true);
                Sprite spritePreview = _normalRewardSpriteAtlas.GetSprite(reward.SpriteName);
                VisualizeRewardSprite(spritePreview);

                if(rewardSerialized.ApplyModifiedProperties())
                {
                    EditorUtility.SetDirty(reward);
                }

            }  
            EditorGUILayout.Space(5); 


            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
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
                newReward.InitializeRewardType(RewardType.Normal);
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
                newReward.InitializeRewardType(RewardType.Special);
                string folderPath = AssetDatabase.GetAssetPath(_specialRewardTargetFolder);
                string fullPath = $"{folderPath}/SpecialReward_{_rewardPool.SpecialRewardDatas.Count + 1}.asset";
                AssetDatabase.CreateAsset(newReward, fullPath);
                AssetDatabase.SaveAssets();

                _rewardPool.SpecialRewardDatas.Add(newReward);
                ArrayUtility.Add(ref _specialRewardFoldouts, true);

                EditorUtility.SetDirty(_rewardPool);
            }
        }
        private void AddCurrencyButton()
        {
            if(GUILayout.Button("Add Currency"))
            {
                RewardData newReward = CreateInstance<RewardData>();
                newReward.InitializeRewardType(RewardType.Currency);
                string folderPath = AssetDatabase.GetAssetPath(_currenciesTargetFolder);
                string fullPath = $"{folderPath}/CurrencyReward_{_rewardPool.CurrencyDatas.Count + 1}.asset";
                AssetDatabase.CreateAsset(newReward, fullPath);
                AssetDatabase.SaveAssets();

                _rewardPool.CurrencyDatas.Add(newReward);
                ArrayUtility.Add(ref _currencyRewardFoldouts, true);

                EditorUtility.SetDirty(_rewardPool);
            }
        }
        private void AddAllRewards(RewardType rewardType)
        {
            string buttonText = "Add All ";
            List<RewardData> rewardDatas = null;
            bool[] rewardFoldouts = null;
            SpriteAtlas spriteAtlas = null;
            DefaultAsset targetFolder = null;
            string pathText = "";
            switch(rewardType)
            {
                case RewardType.Normal:
                    buttonText += "Normal Rewards";
                    rewardDatas = _rewardPool.NormalRewardDatas;
                    rewardFoldouts = _normalRewardFoldouts;
                    spriteAtlas = _normalRewardSpriteAtlas;
                    targetFolder = _normalRewardTargetFolder;
                    pathText = "NormalReward";
                    break;
                case RewardType.Currency:
                    buttonText += "Currencies";
                    rewardDatas = _rewardPool.CurrencyDatas;
                    rewardFoldouts = _currencyRewardFoldouts;
                    spriteAtlas = _currencySpriteAtlas;
                    targetFolder = _currenciesTargetFolder;
                    pathText = "CurrencyReward";
                    break;
                case RewardType.Special:
                    buttonText += "Special Rewards";
                    rewardDatas = _rewardPool.SpecialRewardDatas;
                    rewardFoldouts = _specialRewardFoldouts;
                    spriteAtlas = _specialRewardSpriteAtlas;
                    targetFolder = _specialRewardTargetFolder;
                    pathText = "SpecialReward";
                    break;
                default:
                    break;
            }
            if(GUILayout.Button(buttonText))
            {
                for(int i = 0; i < rewardDatas.Count; i++)
                {
                    CompletelyRemoveReward(rewardDatas, rewardFoldouts, i);
                }
                for(int i = 0; i < spriteAtlas.spriteCount; i++)
                {

                    RewardData newReward = CreateInstance<RewardData>();
                    newReward.InitializeRewardType(rewardType);
                    string folderPath = AssetDatabase.GetAssetPath(targetFolder);
                    string fullPath = $"{folderPath}/{pathText}_{rewardDatas.Count + 1}.asset";
                    if(i < spriteAtlas.spriteCount)
                    {
                        newReward.selectedSpriteIndex = i;
                    }
                    AssetDatabase.CreateAsset(newReward, fullPath);
                    AssetDatabase.SaveAssets();

                    rewardDatas.Add(newReward);
                    ArrayUtility.Add(ref rewardFoldouts, true);

                    EditorUtility.SetDirty(_rewardPool);
                }
            }
        }
        private void AddBomb()
        {

            if(GUILayout.Button("Add Bomb"))
            {
                if(_rewardPool.BombData == null)
                {
                    RewardData newReward = CreateInstance<RewardData>();
                    newReward.selectedSpriteIndex = 0;
                    string folderPath = AssetDatabase.GetAssetPath(_bombTargetFolder);
                    string fullPath = $"{folderPath}/Bomb.asset";

                    AssetDatabase.CreateAsset(newReward, fullPath);
                    AssetDatabase.SaveAssets();

                    _rewardPool.InitializeBomb(newReward);
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
