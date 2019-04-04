using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Entities;
//using System;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using TMPro;

namespace NewEcsTest
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private EntityManager m_Manager = null;
        private int m_ObjectTotalCount = 0;

        [SerializeField] private int m_AddingObjectCounts;
        [SerializeField] private GameObject _myObjectPrefab = null;
        [SerializeField] private float m_Speed;
        [SerializeField] private float m_LeftBound;
        [SerializeField] private float m_RightBound;
        [SerializeField] private float m_TopBound;
        [SerializeField] private float m_BottomBound;
        [SerializeField] private TextMeshProUGUI m_ObjectCountText = null;

        public float TopBound => m_TopBound;
        public float BottomBound => m_BottomBound;
        public float LeftBound => m_LeftBound;
        public float RightBound => m_RightBound;

        public float Radius = 0f;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            m_Manager = World.Active.GetOrCreateManager<EntityManager>();
            CreateObjects(m_AddingObjectCounts);
        }

        private void CreateObjects(int amount)
        {
            NativeArray<Entity> entities = new NativeArray<Entity>(amount, Allocator.Temp);
            m_Manager.Instantiate(_myObjectPrefab, entities);

            for (int i = 0; i < amount; i++)
            {
                float posX = UnityEngine.Random.Range(m_LeftBound, m_RightBound);
                float posZ = UnityEngine.Random.Range(0, 10f);
                m_Manager.SetComponentData(entities[i], new Position { Value = new float3(posX, 0f, m_TopBound + posZ) });
                m_Manager.SetComponentData(entities[i], new Rotation { Value = new quaternion(0, 1, 0, 0) });
                m_Manager.SetComponentData(entities[i], new MoveSpeed { Value = m_Speed });
            }
            entities.Dispose();

            m_ObjectTotalCount += amount;
            m_ObjectCountText.text = $"Count: {m_ObjectTotalCount}";
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space) || (Input.GetMouseButtonUp(0)))
            {
                CreateObjects(m_AddingObjectCounts);
            }
        }
    }
}