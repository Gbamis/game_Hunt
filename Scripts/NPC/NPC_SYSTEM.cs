using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public class NPC_SYSTEM : MonoBehaviour
    {
        public GameEvent gameEvent;

        public NPC_Factory merchant_Factory;
        public NPC_Factory pen_owner_Factory;

        private void Start()
        {
            merchant_Factory.CreaateNpc();
            pen_owner_Factory.CreaateNpc();
        }
    }
}
