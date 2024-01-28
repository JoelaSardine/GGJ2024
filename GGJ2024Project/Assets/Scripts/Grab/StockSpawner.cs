using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StockSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] Objects;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private float MaxDistance;

    private Vector3[] BasePosition;
    
    void Start()
    {
        BasePosition = new Vector3[Objects.Length];
        for (int i = 0; i < Objects.Length; i++)
        {
            BasePosition[i] = Objects[i].position;
        }
    }
    
    void Update()
    {
        for (int i = 0; i < Objects.Length; i++)
        {
            float distance = Vector3.Distance(BasePosition[i], Objects[i].position);

            if (distance > MaxDistance)
            {
                string name = Objects[i].name;

                Objects[i] = Instantiate(Prefab, BasePosition[i], Quaternion.identity).transform;
                Objects[i].name = name;
            }
        }
    }
}
