using System.Collections.Generic;
using UnityEngine; 
using WheelOfFortune.SaveManagement;

namespace WheelOfFortune.UserInterface {

    public class UICurrency: MonoBehaviour {
        [SerializeField] private UICurrencyItem _uiCurrencyItemPrefab;
        private List<CurrencySaveData> _currencyDatas;
        private UICurrencyItem[] _uiCurrencyItems;
        public void InitializeCurrencyUI(List<CurrencySaveData> savedCurrencyDatas)
        {
            _currencyDatas = savedCurrencyDatas;

            if(_uiCurrencyItems == null)
            {
                _uiCurrencyItems = new UICurrencyItem[savedCurrencyDatas.Count];
                for(int i = 0; i < savedCurrencyDatas.Count; i++)
                {
                    UICurrencyItem uICurrencyItem = Instantiate(_uiCurrencyItemPrefab, transform);
                    _uiCurrencyItems[i] = uICurrencyItem;
                }
            }
            
            UpdateCurrencyUI();
        }
        public void UpdateCurrencyUI()
        {
            for(int i = 0; i < _uiCurrencyItems.Length; i++)
            {
                _uiCurrencyItems[i].UpdateItem(_currencyDatas[i]);

            }
        }
    }
}
