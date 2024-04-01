using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    List<List<int>> grid;
    public int numRows = 9;
    public int numCols = 16;

    // Start is called before the first frame update
    void Start()
    {
        for (int y = 0; y < numRows; y++)
        {
            List<int> list = new List<int>();
            for (int x = 0; x < numRows; x++)
            {
                list.Add(0);
            }
            grid.Add(list);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
