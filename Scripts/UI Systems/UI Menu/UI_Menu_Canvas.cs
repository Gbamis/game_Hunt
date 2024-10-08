using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public class UI_Menu_Canvas : UICanvas
    {
        private Dictionary<Button, MenuItem> keyValues;
        public List<MenuItem> menuItems;


        public override void OnInit(GameEvent ge)
        {
            base.OnInit(ge);
            gameEvent = ge;
            keyValues = new Dictionary<Button, MenuItem>();

            foreach (MenuItem mi in menuItems)
            {
                mi.Activate(false);
                Button btn = mi.GetComponent<Button>();
                keyValues.Add(btn, mi);
                btn.onClick.AddListener(() => { MenuItemClicked(btn); });
            }
        }

        public override void OnDisplay() => base.OnDisplay();

        public override void OnHide()
        {
            base.OnHide();
            gameObject.SetActive(false);
        }

        public void CloseMenuItems()
        {
            foreach (MenuItem mi in menuItems)
            {
                mi.Activate(false);
            }
        }

        private void MenuItemClicked(Button bt)
        {
            Debug.Log("clicked");
            foreach (KeyValuePair<Button, MenuItem> bm in keyValues) { bm.Value.Activate(false); }
            keyValues[bt].Activate(true);
        }


    }
}
