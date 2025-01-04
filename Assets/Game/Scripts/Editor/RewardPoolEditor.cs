using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

namespace WheelOfFortune {

    [CustomEditor(typeof(RewardPool))]
    public class RewardPoolEditor: AbstractRewardSystemEditor {

        private bool[] _rewardFoldouts;
        [SerializeField] private DefaultAsset _targetFolder;
        public override void OnInspectorGUI()
        {
            RewardPool rewardPool = (RewardPool)target;
            if(rewardPool.RewardDatas == null)
            {
                return;
            }

            InitializeRewardFoldouts(rewardPool);

            EditorGUILayout.LabelField("Reward Pool Editor", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            VisualizeRewardList(rewardPool);
            AddNewRewardButton(rewardPool);
            if(GUI.changed)
            {
                EditorUtility.SetDirty(rewardPool);
            }
        }

        private void InitializeRewardFoldouts(RewardPool rewardPool)
        {
            if(_rewardFoldouts == null || _rewardFoldouts.Length != rewardPool.RewardDatas.Count)
            {
                _rewardFoldouts = new bool[rewardPool.RewardDatas.Count];
                for(int i = 0; i < _rewardFoldouts.Length; i++)
                {
                    _rewardFoldouts[i] = true;
                }
            }
        }

        private void VisualizeRewardList(RewardPool rewardPool)
        {
            for(int i = 0; i < rewardPool.RewardDatas.Count; i++)
            {
                RewardData reward = rewardPool.RewardDatas[i];

                EditorGUILayout.BeginVertical("box");

                _rewardFoldouts[i] = EditorGUILayout.Foldout(_rewardFoldouts[i], reward.RewardName, true);

                if(_rewardFoldouts[i])
                {
                    rewardPool.RewardDatas[i] = (RewardData)EditorGUILayout.ObjectField(
                        "Reward Reference",
                        rewardPool.RewardDatas[i],
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
                        SetSprite(reward);
                        Sprite spritePreview = _spriteAtlas.GetSprite(reward.SpriteName);
                        VisualizeRewardSprite(spritePreview);

                        if(rewardSerialized.ApplyModifiedProperties())
                        {
                            EditorUtility.SetDirty(reward);
                        }

                    } else
                    {
                        RemoveReward(rewardPool, i);
                    }
                EditorGUILayout.Space(5);

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if(GUILayout.Button("Move Upper", GUILayout.Width(120), GUILayout.Height(20)))
                {
                    if(i > 0)
                    {
                        RewardData temp = rewardPool.RewardDatas[i - 1];
                        rewardPool.RewardDatas[i - 1] = rewardPool.RewardDatas[i];
                        rewardPool.RewardDatas[i] = temp;
                    }
                    break;
                }
                GUILayout.FlexibleSpace();

                if(GUILayout.Button("Remove Reward From The List", GUILayout.Width(240), GUILayout.Height(20)))
                {
                    RemoveReward(rewardPool, i);
                    break;
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();


                if(GUILayout.Button("Move Lower", GUILayout.Width(120), GUILayout.Height(20)))
                {
                    if(i >= 0 && i < rewardPool.RewardDatas.Count - 1)
                    {
                        RewardData temp = rewardPool.RewardDatas[i + 1];
                        rewardPool.RewardDatas[i + 1] = rewardPool.RewardDatas[i];
                        rewardPool.RewardDatas[i] = temp;
                    }
                    break;
                }
                GUILayout.FlexibleSpace();

                if(GUILayout.Button("Completely Delete Reward Data", GUILayout.Width(240), GUILayout.Height(20)))
                {
                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(rewardPool.RewardDatas[i]));
                    RemoveReward(rewardPool, i);
                    break;
                }
                GUILayout.FlexibleSpace();

                GUILayout.EndHorizontal();

                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }
        }

        private void AddNewRewardButton(RewardPool rewardPool)
        {
            if(GUILayout.Button("Add New Reward"))
            {
                RewardData newReward = CreateInstance<RewardData>(); 
                string folderPath = AssetDatabase.GetAssetPath(_targetFolder);
                string fullPath = $"{folderPath}/NewReward_{rewardPool.RewardDatas.Count + 1}.asset";
                AssetDatabase.CreateAsset(newReward, fullPath);
                AssetDatabase.SaveAssets();

                rewardPool.RewardDatas.Add(newReward);
                ArrayUtility.Add(ref _rewardFoldouts, true);

                EditorUtility.SetDirty(rewardPool);
            }
        }

        private void RemoveReward(RewardPool rewardPool, int i)
        {
            rewardPool.RewardDatas.RemoveAt(i);
            ArrayUtility.RemoveAt(ref _rewardFoldouts, i);

            EditorUtility.SetDirty(rewardPool);
        }
    }
}
