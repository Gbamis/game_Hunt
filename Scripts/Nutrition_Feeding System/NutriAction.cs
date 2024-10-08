using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public interface INutriAction
    {
        void Buy();
    }

    public class NutriAction : ScriptableObject
    {
        public Sprite icon;
        public float cost;
        protected int spawnCount;

        public virtual void Init() { }

        public virtual void Buy(int count) { }

        public virtual void Reset(){}
    }
}
