using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public class UI_Gameplay_Canvas : UICanvas
    {
        private RectTransform goatInfoRect;
        [SerializeField] private UI_Goat_Info goatInfo;
        public Vector2 offset;
        public Text statText;
        public Text day_time_text;


        public override void OnInit(GameEvent ge)
        {
            base.OnInit(ge);
            gameEvent = ge;
            goatInfoRect = goatInfo.GetComponent<RectTransform>();

            gameEvent.OnShowGoatInfo += (ctx, pos) =>
            {
                if (goatInfo == null) { return; }

                Vector2 screen = Camera.main.WorldToScreenPoint(pos);
                screen.x += offset.x;
                screen.y += offset.y;
                goatInfoRect.position = screen;
                goatInfo.SetData(ctx);
                goatInfo.gameObject.SetActive(true);
            };

            gameEvent.OnHideGoatInfo += () =>
            {
                if (goatInfo != null) { goatInfo.gameObject.SetActive(false); }
            };

            gameEvent.OnBreedStatChanged += (ctx, ctx2) =>
            {
                if (statText.text != null) { statText.text = "M " + ctx + " F " + ctx2;}
                
            };
        }

        public override void OnDisplay()
        {
            base.OnDisplay();
            goatInfo.gameObject.SetActive(false);

        }

        public override void OnHide()
        {
            base.OnHide();
        }

        public void SetTimeText(int day, float hour, float minute, string meridian)
        {
            //string meridian = hour > 12 ? "PM" : "AM";
            day_time_text.text = "Day " + day.ToString() + " [ " +
            hour.ToString() + " : " + minute.ToString() + " " + meridian + " ]";
        }

    }
}
