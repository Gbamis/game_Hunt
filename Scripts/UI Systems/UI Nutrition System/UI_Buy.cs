using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public class UI_Buy : MonoBehaviour
    {
        public NutriAction nutriAction;
        public Text costTxt;
        public Text countTxt;
        public Button moreBtn;
        public Button lessBtn;
        public Button buyBtn;

        [Header("values")]
        private int count;
        public int max;
        public Image fill;


        private void Start()
        {
            countTxt.text = "0";
            costTxt.text = "$0";
            count = 0;
            fill.fillAmount = (float)count / max;

            nutriAction.Init();

            moreBtn.onClick.AddListener(() =>
            {
                count++;
                count = Mathf.Clamp(count, 0, max);
                float total = count * nutriAction.cost;
                countTxt.text = count.ToString();

                fill.fillAmount = (float)count / max;

                costTxt.text = "$" + total;
            });

            lessBtn.onClick.AddListener(() =>
            {
                count--;
                count = Mathf.Clamp(count, 0, max);
                fill.fillAmount = (float)count / max;
                countTxt.text = count.ToString();

                float total = count * nutriAction.cost;
                costTxt.text = "$" + total;
            });

            buyBtn.onClick.AddListener(() =>
            {
                nutriAction.Buy(count);
                countTxt.text = "0";
                costTxt.text = "$0";
                fill.fillAmount = 0;
                count = 0;
                //gameObject.SetActive(false);
                gameObject.SendMessageUpwards("CloseMenuItems");

            });
        }
    }
}
