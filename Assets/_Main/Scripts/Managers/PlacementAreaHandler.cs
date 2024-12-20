using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using dincdev._Main.Scripts.Operations;

namespace dincdev
{
    public class PlacementAreaHandler : SingletonMonoBehaviour<PlacementAreaHandler>
    {
        [field: SerializeField] public List<PlacementArea> PlacementAreas { get; private set; }

        public override void Awake()
        {
            base.Awake();
        }

        public void MoveCubeToArea(Cube cube)
        {
            var area = FindAvailableArea();
            if (area == null)
            {
                Debug.LogWarning("No Available Area");
                return;
            }

            cube.transform.position = area.PlacementPosition.position;
            area.IsAreaOccupied = true;
            area.CubeOfArea = cube;

            CheckPlacedCubesToMerge();
        }

        private PlacementArea FindAvailableArea()
        {
            var availableArea = PlacementAreas.FirstOrDefault(area => !area.IsAreaOccupied);
            return availableArea;
        }

        private void CheckPlacedCubesToMerge()
        {
            for (int i = 1; i < PlacementAreas.Count - 1; i++)
            {
                var previousCube = PlacementAreas[i - 1].CubeOfArea;
                var cube = PlacementAreas[i].CubeOfArea;
                var nextCube = PlacementAreas[i + 1].CubeOfArea;

                if (previousCube != null && cube != null && nextCube != null)
                {
                    if (previousCube.CubeTag == cube.CubeTag && nextCube.CubeTag == cube.CubeTag)
                    {
                        Destroy(previousCube.gameObject);
                        Destroy(cube.gameObject);
                        Destroy(nextCube.gameObject);

                        PlacementAreas[i - 1].IsAreaOccupied = false;
                        PlacementAreas[i].IsAreaOccupied = false;
                        PlacementAreas[i + 1].IsAreaOccupied = false;

                        PlacementAreas[i - 1].CubeOfArea = null;
                        PlacementAreas[i].CubeOfArea = null;
                        PlacementAreas[i + 1].CubeOfArea = null;
                    }
                }
            }
        }
    }
}