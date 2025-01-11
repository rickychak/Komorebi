using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.Jobs;
using UnityEngine;

namespace Komorebi.Debug
{
    public class DebugDisplayManager: MonoBehaviour
    {
        private List<DebugObject> _debugObjects = new();
        private TextMeshProUGUI _textMesh;

        public void Awake()
        {
            _textMesh = GetComponentInChildren<TextMeshProUGUI>();
            if (_textMesh == null)
            {
                UnityEngine.Debug.Log("cannot find TextMeshPro");
            }
        }
        
        public void AppendToDebugObjects(DebugObject debugObject)
        {
            _debugObjects.Add(debugObject);
        }

        private string JoinDebugObjects()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var debugObject in _debugObjects)
            {
                builder.AppendLine(debugObject.RetrieveDebugObject());
            }
            return builder.ToString();
        }

        private void Update()
        {
            _textMesh.text = JoinDebugObjects();
        }
    }
}
