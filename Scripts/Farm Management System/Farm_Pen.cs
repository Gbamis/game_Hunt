using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public class Farm_Pen : MonoBehaviour, IFarmStruct
    {
        public GameObject structure;

        private void OnBecameVisible() => structure.SetActive(true);
        private void OnBecameInvisible() => structure.SetActive(false);
    }

}