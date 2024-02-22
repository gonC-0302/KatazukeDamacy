using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private List<TargetController> _smallTargetControllersList;
    [SerializeField] private List<TargetController> _middleTargetControllersList;
    [SerializeField] private List<TargetController> _largeTargetControllersList;


    public void Spawn()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnSmallTarget();
        }
        for (int i = 0; i < 2; i++)
        {
            SpawnMiddleTarget();
        }
    }

    private void SpawnSmallTarget()
    {
        for (int i = 0; i < _smallTargetControllersList.Count; i++)
        {
            var radius = 10f;
            var posCircle = radius * Random.insideUnitCircle;
            var spawnPos = new Vector3(posCircle.x, 10, posCircle.y);
            var rot = Random.rotation;
            Instantiate(_smallTargetControllersList[i], spawnPos, rot);
        }
    }

    private void SpawnMiddleTarget()
    {
        for (int i = 0; i < _middleTargetControllersList.Count; i++)
        {
            var radius = 10f;
            var posCircle = radius * Random.insideUnitCircle;
            var spawnPos = new Vector3(posCircle.x, 10, posCircle.y);
            var rot = Random.rotation;
            Instantiate(_middleTargetControllersList[i], spawnPos, rot);
        }
    }
}
