using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HT
{
    [RequireComponent(typeof(Button))]
    public class MenuItem : MonoBehaviour
    {
        public Image bg;
        public GameObject content;


        private void Start()
        {
            content.SetActive(false);
        }

        public void Activate(bool value)
        {
            Color color = bg.color;
            color.a = value ? 1 : 0;
            bg.color = color;
            content.SetActive(value);
        }
    }
}
