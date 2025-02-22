using System.Collections.Generic;
using UnityEditor; 
using UnityEngine;
using WheelOfFortune.General;
using WheelOfFortune.Reward;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Stage {
    [CustomEditor(typeof(StagePool))]
    public class StagePoolEditor: Editor {
        [SerializeField] private DefaultAsset _stageTargetFolder;
        [SerializeField] private bool _simpleMode;
        [SerializeField] private int _stageCount;
        [SerializeField] private GameSettings _gameSettings;

        private StagePool _stagePool;
        private bool[] _stageFoldouts;
        private List<SerializedObject> _serializedObjects;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _simpleMode = EditorGUILayout.Toggle("Simple Mode", _simpleMode);
            if(_simpleMode)
            {
                return;
            }

            _stagePool = (StagePool)target;

            InitializeStageFoldouts();


            EditorGUILayout.LabelField("Manual Stage System Editor", EditorStyles.boldLabel);

            EditorGUILayout.Space();

            VisualizeStageList();
            AddStageButton();
            AddAllStages();


            if(GUI.changed)
            {
                EditorUtility.SetDirty(_stagePool);
            }
        }

        private void InitializeStageFoldouts()
        {
            if(_stagePool.StageDatas != null)
            {
                if(_stageFoldouts == null || _stageFoldouts.Length != _stagePool.StageDatas.Count)
                {
                    _stageFoldouts = new bool[_stagePool.StageDatas.Count];
                }
            }
        }

        private void AddStageButton()
        {
            if(GUILayout.Button("Add Stage"))
            {
                AddStage();
            }
        }

        private void AddStage()
        {
            // create and initialize a new stage data scriptable object.
            StageData newStage = CreateInstance<StageData>();
            string folderPath = AssetDatabase.GetAssetPath(_stageTargetFolder);
            string fullPath = $"{folderPath}/Stage_{_stagePool.StageDatas.Count + 1}.asset";

            // all the required datas here
            _stagePool.StageDatas.Add(newStage);
            int stageNo = _stagePool.StageDatas.Count;
            StageZone stageZone = Helpers.GetStageZone(stageNo, _gameSettings);
            List<RewardData> rewardDatas = _gameSettings.RewardPool.GetRandomRewards(_gameSettings.StageRewardUnitAmount, Helpers.GetStageZone(stageNo, _gameSettings));
            newStage.InitializeStageData(stageNo, stageZone, rewardDatas);

            AssetDatabase.CreateAsset(newStage, fullPath);
            AssetDatabase.SaveAssets();
            ArrayUtility.Add(ref _stageFoldouts, true);
            EditorUtility.SetDirty(_stagePool);
        }
        /// <summary>
        /// simply iterates all the reward datas and visualizes them
        /// </summary>
        private void VisualizeStageList()
        {
    
            List<StageData> stageDatas = _stagePool.StageDatas;
            if(stageDatas == null)
            {
                return; 
            }
            for(int i = 0; i < stageDatas.Count; i++)
            {
                StageData stage = stageDatas[i];

                EditorGUILayout.BeginVertical("box");

           
                _stageFoldouts[i] = EditorGUILayout.Foldout(_stageFoldouts[i], $"Stage {i + 1}", true);

                if(_stageFoldouts[i])
                { 
                    stageDatas[i] = (StageData)EditorGUILayout.ObjectField(
                        "Stage Reference",
                        stageDatas[i],
                        typeof(StageData),
                        false
                    );

                    int containIndex = -1;
                    if(stage != null)
                    { 
                        SerializedObject stageSerialized = new SerializedObject(stage);
                        if(_serializedObjects == null)
                        {
                            _serializedObjects = new List<SerializedObject>();
                        }
                        for(int a = 0; a < _serializedObjects.Count; a++)
                        {
                            if(_serializedObjects[a].targetObject == stageSerialized.targetObject)
                            {
                                containIndex = a;
                                break;
                            }
                        }
                        if(containIndex >= 0)
                        {
                            stageSerialized = _serializedObjects[containIndex];
                        }
                        else
                        {
                            _serializedObjects.Add(stageSerialized);
                            containIndex = _serializedObjects.Count - 1;
                        }
                         
                        SerializedProperty prop = stageSerialized.GetIterator();
                        prop.Next(true);
                        prop.NextVisible(true);
                        while(prop.NextVisible(false))
                        {
                            EditorGUILayout.PropertyField(prop, true);
                        }
                        if(stageSerialized.ApplyModifiedProperties())
                        {
                            EditorUtility.SetDirty(stage);
                        }
                    }
                    else
                    { 
                        RemoveStage(i, containIndex);
                    }

                    EditorGUILayout.Space(5);
                     
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if(GUILayout.Button("Move Upper", GUILayout.Width(120), GUILayout.Height(20)))
                    {
                        if(i > 0)
                        {
                            StageData temp = stageDatas[i - 1];
                            stageDatas[i - 1] = stageDatas[i];
                            stageDatas[i] = temp;
                        }
                        break;
                    }
                    GUILayout.FlexibleSpace();

                    if(GUILayout.Button("Remove Stage From The List", GUILayout.Width(240), GUILayout.Height(20)))
                    {
                        RemoveStage(i, containIndex);
                        break;
                    }
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();

                    if(GUILayout.Button("Move Lower", GUILayout.Width(120), GUILayout.Height(20)))
                    {
                        if(i >= 0 && i < stageDatas.Count - 1)
                        {
                            StageData temp = stageDatas[i + 1];
                            stageDatas[i + 1] = stageDatas[i];
                            stageDatas[i] = temp;
                        }
                        break;
                    }
                    GUILayout.FlexibleSpace();

                    if(GUILayout.Button("Completely Delete Stage Data", GUILayout.Width(240), GUILayout.Height(20)))
                    {
                        CompletelyRemoveStage(i, containIndex);
                        break;
                    }
                    GUILayout.FlexibleSpace();

                    GUILayout.EndHorizontal();
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }
        }

        private void CompletelyRemoveStage(int i, int containIndex = -1)
        {  
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(_stagePool.StageDatas[i]));
            RemoveStage(i, containIndex);
        }

        private void RemoveStage(int i, int containIndex = -1)
        { 
            if(containIndex >= 0)
            {
                _serializedObjects.RemoveAt(containIndex);
            }
            _stagePool.StageDatas.RemoveAt(i);
            ArrayUtility.RemoveAt(ref _stageFoldouts, i);

            EditorUtility.SetDirty(_stagePool);
        }

        private void AddAllStages()
        { 
            GUILayout.BeginHorizontal();
            GUILayout.Label("Amount", GUILayout.Width(60), GUILayout.Height(20));

            _stageCount = EditorGUILayout.IntField(_stageCount, GUILayout.Width(50), GUILayout.Height(20));
            if(GUILayout.Button("Initialize Stage Templates", GUILayout.Width(240), GUILayout.Height(20)))
            {
                for(int i = 0; i < _stageCount; i++)
                {
                    AddStage();
                }
            }
            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();
        }
    }
}
