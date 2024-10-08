using UnityEngine;

namespace HT
{
    public class NPC_PenOwner : MonoBehaviour, IamNPC
    {
        public GameEvent gameEvent;
        public Dialog dialog;
        public float amount;
        public DialogAction dialogAction;

        public void OnSpanwed(Vector3 pos, Quaternion rot)
        {
            dialogAction.InjectFunc(NPC_Action);
            transform.SetPositionAndRotation(pos,rot);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                dialog.Read(gameEvent);
                gameEvent.OnBlockPlayerInput(true);
            }
        }

        private void NPC_Action()
        {
            Debug.Log("NPC do work");
        }

        private void GetPaid()
        {
            gameEvent.OnGet_BankSystem().Credit(amount);
        }
    }

}