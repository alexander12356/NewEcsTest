using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Burst;

namespace NewEcsTest
{
    public class MovementSystem : JobComponentSystem
    {
        [BurstCompile]
        struct MovementJob : IJobProcessComponentData<Position, Rotation, MoveSpeed>
        {
            public float topBound;
            public float bottomBound;
            public float deltaTime;
            public float3 mousePosition;
            public float radius;
            public float leftBound;
            public float rightBound;

            public void Execute(ref Position position, [ReadOnly] ref Rotation rotation, [ReadOnly] ref MoveSpeed speed)
            {
                var value = position.Value;

                //value += deltaTime * speed.Value * math.forward(rotation.Value);

                if (value.z < bottomBound)
                {
                    //value.x = UnityEngine.Random.Range(leftBound, rightBound);
                    value.z = topBound;
                }

                //if ((value.x < (mousePosition.x + radius)) && (value.x > (mousePosition.x - radius)))
                //{
                //    if (mousePosition.x < value.x)
                //    {
                //        rotation.Value.value.w = 1;
                        
                //    }
                //    else
                //    {
                //        rotation.Value.value.w = -1;
                //    }
                //}
                //else
                //{
                //    rotation.Value.value.w = 0;
                //}

                value += deltaTime * speed.Value * math.forward(rotation.Value);

                position.Value = value;
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

            Debug.Log(mouseWorldPos);

            MovementJob movementJob = new MovementJob
            {
                topBound = GameManager.Instance.TopBound,
                bottomBound = GameManager.Instance.BottomBound,
                deltaTime = Time.deltaTime,
                mousePosition = new float3(mouseWorldPos.x, mouseWorldPos.y, mouseWorldPos.z),
                radius = GameManager.Instance.Radius,
                leftBound = GameManager.Instance.LeftBound,
                rightBound = GameManager.Instance.RightBound
            };

            var movementHandle = movementJob.Schedule(this, inputDeps);

            return movementHandle;
        }
    }
}