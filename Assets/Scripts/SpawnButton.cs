using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    public GameObject ObjectPrefab;

    public void SpawnObject()
    {
        GameObject.Instantiate(ObjectPrefab, new Vector3(Random.Range(-3.179f, -1.058f), Random.Range(-2.78f, 2.207f), ObjectPrefab.tag == "ModuleBase" ? 0.0f : -5.0f), Quaternion.identity);
    }
}
