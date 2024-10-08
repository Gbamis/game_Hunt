using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    [System.Serializable]
    public struct GoatInfo
    {
        public string gendder;
        public float stamina;
        public float oestrus;
    }
    public class UI_Goat_Info : MonoBehaviour
    {
        [SerializeField] private Text gender;
        [SerializeField] private Image staminaLevel;
        [SerializeField] private Image oesrusLevel;

        public void SetData(GoatInfo goatInfo)
        {
            gender.text = goatInfo.gendder;
            staminaLevel.fillAmount = goatInfo.stamina;
            oesrusLevel.fillAmount = goatInfo.oestrus;
        }
    }
}
