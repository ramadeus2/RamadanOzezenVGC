using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

namespace WheelOfFortune.Reward {

    public abstract class AbstractRewardSystemEditor: Editor {
        [SerializeField] protected SpriteAtlas _normalRewardSpriteAtlas;
        [SerializeField] protected SpriteAtlas _specialRewardSpriteAtlas;
        [SerializeField] protected SpriteAtlas _currencySpriteAtlas;
        [SerializeField] protected Sprite _bombSprite ;


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
        protected Sprite SetSprite(RewardData rewardData,bool onInspector)
        {
            SpriteAtlas spriteAtlas = null;
                    GUIContent iconLabel = new GUIContent("Sprite");
            switch(rewardData.RewardType)
            {
                case Utilities.RewardType.Normal:
                    spriteAtlas = _normalRewardSpriteAtlas;
                    break;
                case Utilities.RewardType.Currency:
                    spriteAtlas = _currencySpriteAtlas;
                    break;
                case Utilities.RewardType.Special:
                    spriteAtlas = _specialRewardSpriteAtlas;
                    break;
                case Utilities.RewardType.Bomb:

                     
                    return _bombSprite;
                default:
                    break;
            } 

            _allSpriteNames = new string[spriteAtlas.spriteCount];

            Sprite[] sprites = new Sprite[spriteAtlas.spriteCount];
            spriteAtlas.GetSprites(sprites);

            for(int i = 0; i < _allSpriteNames.Length; i++)
            {
                _allSpriteNames[i] = sprites[i].name.Replace("(Clone)", string.Empty);
            }
            if(onInspector)
            {
            rewardData.selectedSpriteIndex = EditorGUILayout.Popup(iconLabel, rewardData.selectedSpriteIndex, _allSpriteNames);
            }
            if(_allSpriteNames.Length > rewardData.selectedSpriteIndex)
            {
                rewardData.InitializeSpriteName(_allSpriteNames[rewardData.selectedSpriteIndex]);
            Sprite spritePreview = sprites[rewardData.selectedSpriteIndex]; 
            return spritePreview;
            }
            return null;
        }
    }
}
