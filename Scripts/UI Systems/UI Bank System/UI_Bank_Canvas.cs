using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public class UI_Bank_Canvas : UICanvas
    {
        public BankAccount bankAccount;
        public Text account_text;

        private void Start()
        {
            account_text.text = "$" + bankAccount.account_balance;
            bankAccount.OnAccountChanged += () => account_text.text = "$" + bankAccount.account_balance;
        }
    }

}