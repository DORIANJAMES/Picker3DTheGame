using System;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.Data.ValueObjects
{
    [Serializable]
    public struct PlayerData
    {
        public PlayerMovementData MovementData;
        public PlayerMeshData MeshData;
        public PlayerForceData ForceData;

    }

    [Serializable]
    public class PlayerForceData
    {
        public float3 ForceParameters;
    }

    [Serializable]
    public class PlayerMeshData
    {
        public float ScaleCounter;
    }

    [Serializable]
    public struct PlayerMovementData
    {
        public float ForwardSpeed;
        public float SidewaySpeed;
    }
}