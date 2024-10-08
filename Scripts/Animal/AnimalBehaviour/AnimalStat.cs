using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    [System.Serializable]
    public class AnimalStat
    {
        [Range(0f, 100)][SerializeField] private float stamina;
        [Range(0.1f, 100)][SerializeField] private float heatLevel;

        public Transform animaTransform;
        public Transform place_of_action;

        public AnimalStat(Transform root)
        {
            stamina = 50;
            animaTransform = root;
            heatLevel = 100;
        }

        public float GetStamina() => stamina / 100;
        public float GetHeatLevel() => heatLevel / 100;
        public void ResetHeatLevel() => heatLevel = 0;
        public void AddStamina(float amount) => stamina = Mathf.Clamp(stamina += amount, 0.1f, 100);
        public void ReduceStamina(float value) => stamina = Mathf.Clamp(stamina -= value, 0f, 100);
        public void ResetStamina() => stamina = 100;
        public void GetNextProductionCycle(float rate)
        {
            Routine.Instance.Run(RaiseHeat());
            IEnumerator RaiseHeat()
            {
                while (heatLevel < 100)
                {
                    heatLevel += rate;
                    yield return null;
                }
                heatLevel = 100;
            }
        }

    }
}
