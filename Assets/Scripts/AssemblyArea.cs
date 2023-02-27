using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyArea : MonoBehaviour
{
    public List<GameObject> ObjectsInArea;
    // Start is called before the first frame update

    private void Awake()
    {
        ObjectsInArea = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ModuleBase")
        {
            ObjectsInArea.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ObjectsInArea.Remove(other.gameObject);
    }
}
