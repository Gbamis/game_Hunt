using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public class Environmental_DayNightCycle : MonoBehaviour
    {
        private int _hour;
        private int _minute;
        private int day = 1;

        public GameEvent gameEvent;

        [Header("Time Params")]
        [SerializeField, Range(0, 24)] private float _timeOfDay;
        [SerializeField] private float _orbitSpeed;
        [SerializeField] private float _orbitSpeedMax;
        [SerializeField] private float _axisOffset;
        [SerializeField] private Gradient _nightLight;
        [SerializeField] private AnimationCurve _sunCurve;

        [Header("Sun")]
        [SerializeField] private Light _sun;
        [SerializeField] private Material SkyboxMaterial;
        [SerializeField] private AnimationCurve skyCurve;
        private bool swapped;


        private UI_Gameplay_Canvas gameplay_Canvas;

        private void Start()
        {
            gameplay_Canvas = gameEvent.OnGet_UISystem().gameplay_canvas;
            gameEvent.OnGetDayNightCycle += () => this;
        }

        public float GetHourNormalized() => _timeOfDay / 24;

        private void OnValidate()
        {
            ProgressTime();
        }

        private void Update()
        {
            _orbitSpeed = (_timeOfDay > 7 && _timeOfDay < 17) ? 0.03f : _orbitSpeedMax;
            _timeOfDay += Time.deltaTime * _orbitSpeed;
            ProgressTime();
        }



        private void ProgressTime()
        {
            float currentTime = _timeOfDay / 24;
            float sunRotation = Mathf.Lerp(-90, 270, currentTime);

            _sun.transform.rotation = Quaternion.Euler(sunRotation, _axisOffset, 0);

            _hour = Mathf.FloorToInt(_timeOfDay);
            _minute = Mathf.FloorToInt(_timeOfDay / (24f / 1440f) % 60);

            RenderSettings.ambientLight = _nightLight.Evaluate(currentTime);
            _sun.intensity = _sunCurve.Evaluate(currentTime) + .6f;

            _timeOfDay %= 24;
            if (_hour > 23.9) { day++; }

            if (gameplay_Canvas != null)
            {
                int h = (_hour > 12) ? _hour - 12 : _hour;
                string med = _timeOfDay > 12 ? "PM" : "AM";
                gameplay_Canvas.SetTimeText(day, h, _minute, med);
            }

            if (SkyboxMaterial != null)
            {
                float code = skyCurve.Evaluate(currentTime) * 181;
                code /= 181;
                Color color = new(code, code, code);
                RenderSettings.skybox.SetColor("_Tint", color);
            }

        }
    }
}