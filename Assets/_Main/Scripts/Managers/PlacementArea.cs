using UnityEngine;

namespace dincdev
{
    [System.Serializable]
    public class PlacementArea
    {
        [field: SerializeField] public Transform PlacementPosition { get; private set; }
        [field: SerializeField] public bool IsAreaOccupied { get; set; }
        [field: SerializeField] public Cube CubeOfArea { get; set; }
    }
}