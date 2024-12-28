using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace dincdev
{
    public class LevelEditor : MonoBehaviour
    {
        [SerializeField] private GameObject placementAreaPrefab;
        [SerializeField] private List<GameObject> cubePrefabs;
        [SerializeField] private int placementAreaCount;
        [SerializeField] private float placementOffset;
        [SerializeField] private float cubePlacementOffset;

        [Header("Cube Counts")]
        [SerializeField] private int redCubeCount;

        [SerializeField] private int blueCubeCount;
        [SerializeField] private int orangeCubeCount;

        private PlacementAreaHandler _placementAreaHandler;

        [Header("Placement Settings")]
        [SerializeField] private float xAxis;

        [SerializeField] private float zAxis;

        private void Start()
        {
            _placementAreaHandler = PlacementAreaHandler.Instance;
            SetPlacementAreas();
            SetCubes();
        }

        private void SetCubes()
        {
            // int totalCubes = redCubeCount + blueCubeCount + orangeCubeCount;
            int currentCubeIndex = 0;

            SpawnCube(redCubeCount, 0, ref currentCubeIndex);
            SpawnCube(blueCubeCount, 1, ref currentCubeIndex);
            SpawnCube(orangeCubeCount, 2, ref currentCubeIndex);
        }

        private void SpawnCube(int desiredCount, int prefabIndex, ref int currentCubeIndex)
        {
            for (int i = 0; i < desiredCount; i++)
            {
                var cube = Instantiate(cubePrefabs[prefabIndex], transform.position, Quaternion.identity);
                GameController.Instance.cubesOfLevel.Add(cube.GetComponent<Cube>());

                float posX = xAxis + cubePlacementOffset * (currentCubeIndex % 5);
                float posY = 1.8f;
                float posZ = zAxis + (cubePlacementOffset * (currentCubeIndex / 5));

                cube.transform.position = new Vector3(posX, posY, posZ);
                currentCubeIndex++;
            }
        }

        private void SetPlacementAreas()
        {
            for (int i = 0; i < placementAreaCount; i++)
            {
                var placementHolder = Instantiate(placementAreaPrefab, _placementAreaHandler.transform);
                placementHolder.transform.position = new Vector3(_placementAreaHandler.transform.position.x + placementOffset * i, _placementAreaHandler.transform.position.y, _placementAreaHandler.transform.position.z);
                PlacementArea placementArea = new PlacementArea();
                placementArea.CubeOfArea = null;
                placementArea.IsAreaOccupied = false;
                placementArea.PlacementPosition = placementHolder.transform;

                _placementAreaHandler.PlacementAreas.Add(placementArea);
            }
        }
    }
}