using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NewEcsTest.Common
{
    public class CommonGameManager : MonoBehaviour
    {
        [SerializeField] private int m_AddingObjectCount = 0;
        [SerializeField] private CommonObject m_CommonObjectPrefab = null;
        [SerializeField] private float m_LeftBound = 0f;
        [SerializeField] private float m_RightBound = 0f;
        [SerializeField] private float m_TopBound;
        [SerializeField] private TextMeshProUGUI m_ObjectCountText = null;
        private int m_ObjectTotalCount = 0;

        private void Start()
        {
            CreateObjects(m_AddingObjectCount);
        }

        private void CreateObjects(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                float posX = UnityEngine.Random.Range(m_LeftBound, m_RightBound);
                float posZ = UnityEngine.Random.Range(0, 10f);
                var newObject = Instantiate(m_CommonObjectPrefab);
                newObject.transform.position = new Vector3(posX, 0f, m_TopBound + posZ);
            }

            m_ObjectTotalCount += amount;
            m_ObjectCountText.text = $"Count: {m_ObjectTotalCount}";
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space) || (Input.GetMouseButtonUp(0)))
            {
                CreateObjects(m_AddingObjectCount);
            }
        }
    }
}