using System;

namespace Assets.Scripts.World
{
    public class Wallet : IMoneyCollection
    {
        public int AvailableMoney { get; private set; }
        private int _totalMoney;
        private int _totalReserved;

        public Action OnMoneyChanged;

        public void AcceptMoney(int amount)
        {
            var newAmount = _totalMoney + amount;
            _totalMoney = newAmount >= 0 ? newAmount : 0;
            RecalcAvailableMoney();
        }

        public void AddReservation(int amount)
        {
            _totalReserved += amount;
            RecalcAvailableMoney();
        }

        private void RecalcAvailableMoney()
        {
            var last = AvailableMoney;
            AvailableMoney = _totalMoney - _totalReserved;
            if(last != AvailableMoney)
                OnMoneyChanged?.Invoke();
        }
    }
}