using UnityEditor;
using UnityEngine;

namespace Komorebi.PersistentData
{
    [CreateAssetMenu(fileName = "MapData", menuName = "Assets/PersistentData")]
    public class MapData : ScriptableObject
    {
        [SerializeField] private bool _mainDoorClose = false;
    }
}

