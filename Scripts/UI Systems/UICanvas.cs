using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public class UICanvas : MonoBehaviour
    {
        protected GameEvent gameEvent;
        protected void OnEnable() => OnDisplay();
        protected void OnDisable() => OnHide();

        
        public virtual void OnInit(GameEvent gameEvent) { }
        public virtual void OnDisplay() { }
        public virtual void OnHide() { }
    }
}
