using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using dincdev._Main.Scripts.Operations;
using TMPro;
using UnityEngine;

namespace dincdev
{
    public class GameController : SingletonMonoBehaviour<GameController>
    {
        [field: SerializeField] public int MoveCountForPlayer { get; private set; }
        [field: SerializeField] public TextMeshProUGUI MoveCountText { get; private set; }

        public List<Cube> cubesOfLevel = new();

        private void Start()
        {
            MoveCountText.text = MoveCountForPlayer.ToString();
        }

        public void UpdateMoveCount()
        {
            if (MoveCountForPlayer > 0) MoveCountForPlayer--;
            MoveCountText.text = MoveCountForPlayer.ToString();
            StartCoroutine(WaitForGameEndCondition());
        }

        public void CheckWinCondition()
        {
            if (cubesOfLevel.Count == 0) //Eğer tüm küpler yok olduysa win eğer alan kalmadıysa ya da hareket hakkı kalmadıysa lose
            {
                GameStateHandler.Instance.GameWin();
            }
            else if ((MoveCountForPlayer <= 0 && cubesOfLevel.Count > 0) || (cubesOfLevel.Count > 0 && PlacementAreaHandler.Instance.PlacementAreas.All(area => area.IsAreaOccupied)))
            {
                GameStateHandler.Instance.GameOver();
            }
        }

        IEnumerator WaitForGameEndCondition()
        {
            yield return new WaitForSeconds(0.5f);
            CheckWinCondition();
        }
    }
}