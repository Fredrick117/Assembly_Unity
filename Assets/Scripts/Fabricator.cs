using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fabricator : MonoBehaviour
{
    [SerializeField]
    private Transform spawnLocation;

    public List<GameObject> shipPrefabs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    int randomNum = Random.Range(0, shipPrefabs.Count);

        //    GameObject shipPart = GameObject.Instantiate(shipPrefabs[randomNum], transform.position, Quaternion.identity);
        //    shipPart.GetComponent<Rigidbody2D>().velocity = new Vector3(0, Random.Range(0.25f, 1.0f), 0);
        //    shipPart.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-1.0f, 1.0f);
        //}
    }

    public GameObject SpawnPrefab(GameObject prefabToSpawn)
    {
        return GameObject.Instantiate(prefabToSpawn, spawnLocation.position, prefabToSpawn.transform.rotation);
    }
}
