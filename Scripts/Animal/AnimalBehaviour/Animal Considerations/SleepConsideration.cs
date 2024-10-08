using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "sleep_Consideration", menuName = "Games/Hunt/AI/AnimalConsideration/sleep")]
    public class SleepConsideration : AnimalConsiderations
    {
        public GameEvent gameEvent;
        [SerializeField] private AnimationCurve responseCurve;
        private float score = 0;

        public override float CalculateScore(AnimalStat animalStat)
        {
            float hour = gameEvent.OnGetDayNightCycle().GetHourNormalized();
            score = responseCurve.Evaluate(Mathf.Clamp01(hour));
            return score;
        }
    }

}