using System;
using UnityEngine;

public class WindowsSpawner : MonoBehaviour
{
    public GameObject victoryWindow;
    public GameObject failWindow;
    public GameObject failByInactiveWindow;

    private Vector3 spawnPosition;

    private void Awake()
    {
        spawnPosition = GetComponent<RectTransform>().position;
    }

    public void SpawnWindow(GameObject window)
    {
        Instantiate(window, spawnPosition, Quaternion.identity, transform);
    }
}
