using System; 
using WheelOfFortune.CurrencySystem;

namespace WheelOfFortune.SaveManagement {
    [Serializable]
    public class CurrencySaveData: AbstractSaveData {

        public CurrencySaveData(string currencyId,CurrencyUnit currency) : base(currencyId,Utilities.RewardType.Currency)
        {
            _currency = currency;
            _currentAmount = currency.StartAmount;

        }

        public CurrencySaveData(string currencyId, int amount, CurrencyUnit currency) : base(currencyId, amount, Utilities.RewardType.Currency)
        {
            _currency = currency; 
        }

        private CurrencyUnit _currency;
        public CurrencyUnit Currency => _currency;

    }
} 
