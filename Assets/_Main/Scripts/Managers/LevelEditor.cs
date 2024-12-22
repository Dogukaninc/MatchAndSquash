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

        private void Start()
        {
            _placementAreaHandler = PlacementAreaHandler.Instance;
            SetPlacementAreas();
            SetCubes();
        }

        private void SetCubes() //TODO - indexteki prefab'in tipini bilmediği için elle atamak zorunda kaldım.
        {
            for (int i = 0; i < cubePrefabs.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        SpawnCube(redCubeCount, i);
                        break;
                    case 1:
                        SpawnCube(blueCubeCount, i);
                        break;
                    case 2:
                        SpawnCube(orangeCubeCount, i);
                        break;
                }
            }
        }

        private void SpawnCube(int desiredCount, int prefabIndex)
        {
            for (int i = 0; i < desiredCount; i++)
            {
                var cube = Instantiate(cubePrefabs[prefabIndex], transform.position, Quaternion.identity);
                GameController.Instance.cubesOfLevel.Add(cube.GetComponent<Cube>());
                float posX = Random.Range(3.38f, -3.79f);
                float posY = 1.8f;
                float posZ = Random.Range(-2.58f, 5.23f);
                cube.transform.position = new Vector3(posX, posY, posZ);
            }
        }

        private void SetPlacementAreas()
        {
            for (int i = 0; i < placementAreaCount; i++)
            {
                var placementHolder = Instantiate(placementAreaPrefab, _placementAreaHandler.transform);
                placementHolder.transform.position = new Vector3(_placementAreaHandler.transform.position.x + i + placementOffset, _placementAreaHandler.transform.position.y, _placementAreaHandler.transform.position.z);

                PlacementArea placementArea = new PlacementArea();
                placementArea.CubeOfArea = null;
                placementArea.IsAreaOccupied = false;
                placementArea.PlacementPosition = placementHolder.transform;

                _placementAreaHandler.PlacementAreas.Add(placementArea);
            }
        }
    }
}