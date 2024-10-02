using System;
using Cinemachine;
using Runtime.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Extensions
{
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")]
    public class LockCinemachineAxis : CinemachineExtension
    {
        [Tooltip("Lock the Cinemachine Virtual Camera's X Axis position with this specific value")]
        public float XClampVale = 0f;

        [FormerlySerializedAs("_lockAxis")] [SerializeField] private CinemachineAxisTypes lockAxis;
        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            switch (lockAxis)
            {
                case CinemachineAxisTypes.X:
                    if (stage == CinemachineCore.Stage.Body)
                    {
                        var pos = state.RawPosition;
                        pos.x = XClampVale;
                        state.RawPosition = pos;
                    }

                    if (stage == CinemachineCore.Stage.Aim)
                    {
                        var aim = state.RawOrientation;
                        aim = new Quaternion(XClampVale,XClampVale,XClampVale,aim.w);
                        state.RawOrientation = aim;
                    }
                    break;
                case CinemachineAxisTypes.Y:
                    if (stage == CinemachineCore.Stage.Body)
                    {
                        var pos = state.RawPosition;
                        pos.y = XClampVale;
                        state.RawPosition = pos;
                    }

                    if (stage == CinemachineCore.Stage.Aim)
                    {
                        var aim = state.RawOrientation;
                        aim = new Quaternion(XClampVale,XClampVale,XClampVale,aim.w);
                        state.RawOrientation = aim;
                    }
                    break;
                case CinemachineAxisTypes.Z:
                    if (stage == CinemachineCore.Stage.Body)
                    {
                        var pos = state.RawPosition;
                        pos.z = XClampVale;
                        state.RawPosition = pos;
                    }

                    if (stage == CinemachineCore.Stage.Aim)
                    {
                        var aim = state.RawOrientation;
                        aim = new Quaternion(XClampVale,XClampVale,XClampVale,aim.w);
                        state.RawOrientation = aim;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }
}