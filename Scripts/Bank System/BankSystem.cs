using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public class BankSystem : MonoBehaviour
    {
        public GameEvent gameEvent;
        public BankAccount bankAccount;

        private void Start()
        {
            gameEvent.OnGet_BankSystem += () => this;
        }

        public void Credit(float amount)
        {
            bankAccount.account_balance += amount;
            bankAccount.OnAccountChanged?.Invoke();
        }
        public bool Debit(float amount)
        {
            if (bankAccount.account_balance >= amount)
            {
                bankAccount.account_balance -= amount;
                bankAccount.OnAccountChanged?.Invoke();
                return true;
            }
            return false;
        }

        public void GetLoan(float amount)
        {
            //check load requirements.
            bankAccount.OnAccountChanged?.Invoke();
        }

        public void PayLoan(float amount)
        {
            bankAccount.OnAccountChanged?.Invoke();
        }
    }
}
