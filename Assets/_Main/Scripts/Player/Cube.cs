using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dincdev
{
    public class Cube : MonoBehaviour
    {
        [field: SerializeField] public CubeType CubeType { get; private set; }
        public string CubeTag { get; set; }
        public bool IsPlaced { get; set; }
        private void Awake()
        {
            TypeSetter();
            Debug.Log(CubeTag);
        }

        void Start()
        {
        }

        void Update()
        {
        }

        private void TypeSetter()
        {
            switch (CubeType)
            {
                case CubeType.Blue:
                    CubeTag = "BlueCube";
                    break;

                case CubeType.Red:
                    CubeTag = "RedCube";
                    break;

                case CubeType.Orange:
                    CubeTag = "OrangeCube";
                    break;
            }
        }
    }

    public enum CubeType
    {
        Red,
        Orange,
        Blue
    }
}