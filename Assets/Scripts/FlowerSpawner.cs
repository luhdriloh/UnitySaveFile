using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    public int _minFlowersToSpawn;
    public int _maxFlowersToSpawn;

    private float _left;
    private float _right;
    private float _bottom;
    private float _top;


    private void Awake()
    {
        Vector2 lowerLeft = Camera.main.ScreenToWorldPoint(Vector2.zero);
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        _left = lowerLeft.x + 1;
        _bottom = lowerLeft.y + 1;

        _right = topRight.x - 1;
        _top = topRight.y - 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFlowers();
        }
    }


    private void SpawnFlowers()
    {
        int numberOfFlowersToSpawn = Random.Range(_minFlowersToSpawn, _maxFlowersToSpawn);
        Object[] flowerList = Resources.LoadAll("Prefabs", typeof(GameObject));

        for (int i = 0; i < numberOfFlowersToSpawn; i++)
        {
            Vector3 position = new Vector3(Random.Range(_left, _right), Random.Range(_bottom, _top));
            GameObject myObj = Instantiate(flowerList[Random.Range(0, flowerList.Length)], position, Quaternion.identity) as GameObject;
        }
    }
}
