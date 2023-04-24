using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    public GameObject[] ObjectPrefabs;

    public void SpawnObject()
    {
        GameObject objectToSpawn = ObjectPrefabs[Random.Range(0, ObjectPrefabs.Length)];
        GameObject.Instantiate(objectToSpawn, new Vector3(Random.Range(-3.179f, -1.058f), Random.Range(-2.78f, 2.207f), objectToSpawn.tag == "ModuleBase" ? 0.0f : -5.0f), Quaternion.identity);
    }
}
