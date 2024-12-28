using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Template.GameDataEditor.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        public int currentLevelIndex;
        public int currentStageIndex;

        #region Settings

        public bool isSoundEnabled;
        public bool isMusicEnabled;
        public bool isVibrationEnabled;

        #endregion

        [Button]
        public void Save() => SaveManager.SaveData(this);

        [Button]
        public void Load() => SaveManager.LoadData(this);
    }
}