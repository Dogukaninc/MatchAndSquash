using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Template.GameDataEditor.Editor.DefaultPathEditor
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EditorDataHolder", order = 1)]
    public class EditorDataHolder : ScriptableObject
    {
        public List<string> DefaultDataPathes;
    }
}