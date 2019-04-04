using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewEcsTest.Common
{
    public class CommonObject : MonoBehaviour
    {
        public float topBound;
        public float bottomBound;
        [SerializeField] private Vector3 speed = Vector3.zero;

        public void Update()
        {
            var value = transform.position;

            value += Time.deltaTime * speed;

            if (value.z < bottomBound)
            {
                value.z = topBound;
            }

            transform.position = value;
        }
    }
}