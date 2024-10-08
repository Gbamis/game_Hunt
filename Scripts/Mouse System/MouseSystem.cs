using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace HT
{
    public class MouseSystem : MonoBehaviour
    {
        public GameEvent gameEvent;
        public Transform menuCam;
        public Transform gameCam;

        public string activate_y_axis;
        public string activate_x_axis;
        public string disable_control;

        public CinemachineFreeLook cinemachineFreeLook;

        private void Start()
        {
            gameEvent.OnGetMouseSystem += () => this;
            DeActivate();
            Cursor.SetCursor(gameEvent.defCursorIcon, Vector2.zero, CursorMode.Auto);
        }


        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Activate();
            }
            else
            {
                DeActivate();
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                RightClick();
            }

            if (Input.GetMouseButtonDown(1))
            {
                RightClick();
            }
        }

        private void Activate()
        {
            cinemachineFreeLook.m_XAxis.m_InputAxisName = activate_x_axis;
            cinemachineFreeLook.m_YAxis.m_InputAxisName = activate_y_axis;
        }

        private void DeActivate()
        {
            cinemachineFreeLook.m_XAxis.m_InputAxisName = disable_control;
            cinemachineFreeLook.m_YAxis.m_InputAxisName = disable_control;
        }

        private void RightClick() => gameEvent.OnGet_CommandSystem().RemoveAllSelected();

        private void OpenContextMenu() => gameEvent.OnShowContextMenu(Input.mousePosition);

        public void SetCam(bool menu){
            menuCam.gameObject.SetActive(menu);
            gameCam.gameObject.SetActive(!menu);
        }
    }

}