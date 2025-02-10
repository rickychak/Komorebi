using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class DonutPool: MonoBehaviour
    {
        [SerializeField] private int _defaultDonutCount = 10;
        [SerializeField] private GameObject _donutPrefab;

        public static DonutPool DonutPoolInstance { get; set; }

        private Queue<GameObject> _donutPool = new();

        private void Awake()
        {
            if (DonutPoolInstance == null)
            {
                DonutPoolInstance = this;
            }
            else if (DonutPoolInstance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            AddDonut(_defaultDonutCount);
        }

        public GameObject GetDonut()
        {
            GameObject returnedDonut = null;
            if (_donutPool.Count <= 0)
            {
                AddDonut(_defaultDonutCount);
            }
            
            returnedDonut = _donutPool.Dequeue();
            returnedDonut.SetActive(true);
            return returnedDonut;
        }

        private void AddDonut(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject currentIndexDonut = Instantiate(_donutPrefab, transform, true);
                _donutPool.Enqueue(currentIndexDonut);
                currentIndexDonut.SetActive(false);
            }
        }

        public void ReturnDonut(GameObject donut)
        {
            donut.SetActive(false);
            _donutPool.Enqueue(donut);
            
        }
        
        
        
    }
}