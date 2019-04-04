using System;
using Unity.Entities;

namespace NewEcsTest
{
    [Serializable]
    public struct MoveSpeed : IComponentData
    {
        public float Value;
    }

    public class MovementSpeedComponent : ComponentDataWrapper<MoveSpeed> { }
}