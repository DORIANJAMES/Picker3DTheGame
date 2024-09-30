using System;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CameraSignals : MonoBehaviour
    {
        #region Singleton

        public static CameraSignals Instance;

        private void Awake()
        {
            throw new NotImplementedException();
        }

        #endregion

        public UnityAction onSetCameraTarget = delegate { };
    }
}