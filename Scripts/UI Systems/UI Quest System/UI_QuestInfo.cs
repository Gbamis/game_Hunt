using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public class UI_QuestInfo : MonoBehaviour
    {
        public Text description_text;

        public void Describe(string value) => description_text.text = value;
    }

}