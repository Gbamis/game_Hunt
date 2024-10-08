using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HT
{
    [CreateAssetMenu(fileName = "Bank Account", menuName = "Games/Hunt/Bank/Account")]
    public class BankAccount : ScriptableObject
    {
        public float loan_balance;
        public float account_balance;

        public Action OnAccountChanged;
    }
}
