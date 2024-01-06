using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableComponent : MonoBehaviour
{
    private bool IsDragging = false;
    private Vector3 Offset;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsDragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Offset;
        }
    }

    private void OnMouseDown()
    {
        Offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        IsDragging = true;
        transform.SetParent(null);
    }

    private void OnMouseUp()
    {
        IsDragging = false;
    }


}
