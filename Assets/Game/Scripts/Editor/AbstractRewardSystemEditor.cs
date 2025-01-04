using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

namespace WheelOfFortune {

    public abstract class AbstractRewardSystemEditor: Editor {
        [SerializeField] protected SpriteAtlas _spriteAtlas;
     
        protected string[] _allSpriteNames;

        protected void VisualizeRewardSprite(Sprite spritePreview)
        {
            if(spritePreview != null)
            {
                EditorGUILayout.Space(15);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Sprite Preview:");
                float previewSize = 50f;
                Rect previewRect = GUILayoutUtility.GetRect(previewSize, previewSize);

                EditorGUILayout.Space();

                EditorGUI.DrawTextureTransparent(previewRect, spritePreview.texture, ScaleMode.ScaleToFit);

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(15);
            }
        }
        protected Sprite SetSprite(RewardData rewardData)
        {
            GUIContent iconLabel = new GUIContent("Reward Sprite");

            _allSpriteNames = new string[_spriteAtlas.spriteCount];

            Sprite[] sprites = new Sprite[_spriteAtlas.spriteCount];
            _spriteAtlas.GetSprites(sprites);

            for(int i = 0; i < _allSpriteNames.Length; i++)
            {
                _allSpriteNames[i] = sprites[i].name.Replace("(Clone)", string.Empty);
            }
            rewardData.selectedSpriteIndex = EditorGUILayout.Popup(iconLabel, rewardData.selectedSpriteIndex, _allSpriteNames);
            rewardData.InitializeSpriteName(_allSpriteNames[rewardData.selectedSpriteIndex]);
            Sprite spritePreview = sprites[rewardData.selectedSpriteIndex];
            return spritePreview;
        }
    }
}
