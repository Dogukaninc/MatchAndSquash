using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace dincdev
{
    public class CubeDetectionHandler : MonoBehaviour
    {
        private Ray _ray;
        private Camera _mainCamera;

        private void OnEnable()
        {
            _mainCamera = Camera.main;
        }

        void Start()
        {
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnPointerDown();
            }
        }

        public void OnPointerDown()
        {
            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out var hit))
            {
                hit.collider.TryGetComponent(out Cube cube);
                if (cube != null && !cube.IsPlaced)
                {
                    cube.IsPlaced = true;
                    if (cube.CubeTag == "BlueCube")
                    {
                        Debug.Log("B");
                    }
                    else if (cube.CubeTag == "RedCube")
                    {
                        Debug.Log("R");
                    }
                    else if (cube.CubeTag == "OrangeCube")
                    {
                        Debug.Log("O");
                    }

                    PlacementAreaHandler.Instance.MoveCubeToArea(cube);
                }
            }
        }
    }
}