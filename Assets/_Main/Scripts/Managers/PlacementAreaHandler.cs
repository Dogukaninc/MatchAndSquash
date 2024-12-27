using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using dincdev._Main.Scripts.Operations;
using UnityEngine.Rendering;

namespace dincdev
{
    public class PlacementAreaHandler : SingletonMonoBehaviour<PlacementAreaHandler>
    {
        [field: SerializeField] public List<PlacementArea> PlacementAreas { get; private set; }

        public void MoveCubeToArea(Cube cube)
        {
            var area = FindAvailableArea();

            if (area == null)
            {
                Debug.LogWarning("No Available Area");
                return;
            }

            area.IsAreaOccupied = true;
            area.CubeOfArea = cube;
            Sequence seq = DOTween.Sequence();
            seq.Append(cube.transform.DOJump(area.PlacementPosition.position, 3, 1, 0.5f));
            seq.Join(cube.transform.DORotate(Vector3.up * 180, 0.5f));
            seq.OnComplete(() =>
            {
                cube.transform.position = area.PlacementPosition.position;
                CheckPlacedCubesToMerge();
            });
        }

        private PlacementArea FindAvailableArea()
        {
            var availableArea = PlacementAreas.FirstOrDefault(area => !area.IsAreaOccupied);
            return availableArea;
        }

        public void RePositionCubes()
        {
            for (int i = 0; i < PlacementAreas.Count; i++)
            {
                var cube = PlacementAreas[i].CubeOfArea;
                if (cube != null)
                {
                    var availableArea = FindAvailableArea();
                    if (availableArea != null) ResetArea(cube);
                    cube.transform.DOMove(availableArea.PlacementPosition.position, 0.5f);
                    availableArea.IsAreaOccupied = true;
                    availableArea.CubeOfArea = cube;
                }
            }
        }

        private void ResetArea(Cube cube) //TODO küpler merge olduktan sonra areaları temizlemek içinde bunu kullan
        {
            var area = PlacementAreas.Find(a => a.CubeOfArea == cube);
            area.IsAreaOccupied = false;
            area.CubeOfArea = null;
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
                        MatchAnimation(previousCube, cube, nextCube);
                        SetPlacementAreas(PlacementAreas[i - 1], PlacementAreas[i], PlacementAreas[i + 1]);
                    }
                }
            }
        }

        private void MatchAnimation(Cube prevCube, Cube cube, Cube nextCube)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(cube.transform.DOJump(prevCube.transform.position, 3, 1, 0.5f));
            seq.Join(nextCube.transform.DOJump(prevCube.transform.position, 3, 1, 0.5f));
            seq.Join(cube.transform.DOScale(Vector3.one / 3, 0.5f));
            seq.Join(nextCube.transform.DOScale(Vector3.one / 3, 0.5f));
            seq.OnComplete(() =>
            {
                VFXPlayer.Instance.PlayVFX("confetti", prevCube.transform.position);
                GameController.Instance.cubesOfLevel.Remove(prevCube); //TODO - Burayı düzelt
                GameController.Instance.cubesOfLevel.Remove(cube);
                GameController.Instance.cubesOfLevel.Remove(nextCube);
                DestroyCubes(prevCube, cube, nextCube);
                RePositionCubes();
            });
        }

        private void DestroyCubes(params Cube[] cubes)
        {
            foreach (var cube in cubes)
            {
                Destroy(cube.gameObject);
            }
        }

        private void SetPlacementAreas(params PlacementArea[] areas)
        {
            foreach (var area in areas)
            {
                area.IsAreaOccupied = false;
                area.CubeOfArea = null;
            }
        }
    }
}