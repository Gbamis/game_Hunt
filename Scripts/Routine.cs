using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Services.Authentication;
using Unity.Services.CloudCode;
using Unity.Services.CloudCode.GeneratedBindings;
using Unity.Services.Core;

using LiveCode;

namespace HT
{
    [Serializable]
    public class DebugCheck
    {
        [HideInInspector] public Vector3 center;
        public Color debugColor;
        public float radius;

    }

    public class Routine : MonoBehaviour
    {
        public List<DebugCheck> gizmos = new List<DebugCheck>();
        public MouseClicker moveIcon;

        public static Routine Instance { set; get; }

        private void Awake() => Instance = this;

        private void Start()
        {
            Physics.gravity = new Vector3(0, -19, 0);
            Class1 code = new();
            Debug.Log(code.InitializeModule());

            // Initialize the Unity Services Core SDK
            /* await UnityServices.InitializeAsync();

             // Authenticate by logging into an anonymous account
             await AuthenticationService.Instance.SignInAnonymouslyAsync();

             try
             {
                 // Call the function within the module and provide the parameters we defined in there
                 var module = new HelloWorldBindings(CloudCodeService.Instance);
                 var result = await module.SayHello("World");

                 Debug.Log("this is cloud ode" + result);
             }
             catch (CloudCodeException exception)
             {
                 Debug.LogException(exception);
             }*/
        }

        public void Run(IEnumerator coroutine) => StartCoroutine(coroutine);

        public void AddInfo(DebugCheck info)
        {
            if (gizmos.Contains(info)) { return; }
            gizmos.Add(info);
            StartCoroutine(Remove());
            IEnumerator Remove()
            {
                yield return new WaitForSeconds(2);
                if (gizmos.Contains(info)) { gizmos.Remove(info); }
            }
        }

        private void OnDrawGizmos()
        {
            if (gizmos.Count == 0) { return; }
            foreach (DebugCheck info in gizmos)
            {
                Gizmos.color = info.debugColor;
                Gizmos.DrawSphere(info.center, info.radius);
            }
        }
    }
}
