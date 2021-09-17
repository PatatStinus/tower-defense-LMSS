using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Practice : MonoBehaviour
{
    //public int[] normalArray;
    //public int[][] jaggedArray;
    //public int[,] multiDimensionalArray;

    public int width = 10;
    public int length = 10;

    public GridSpace[,] grid;

    private void Start()
    {
        grid = new GridSpace[width, length];

        for (int x = 0; x < grid.GetLength(0); x++)
        {

            for (int z = 0; z < grid.GetLength(1); z++)
            {
                //Debug.Log(x + " - " + z);
                grid[x, z] = new GridSpace(x, z);
            }
        }

        Debug.Log(grid[5, 5]);
    }

    private void OnDrawGizmos()
    {
        if (grid == null)
        {
            return;
        }

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int z = 0; z < grid.GetLength(1); z++)
            {
                Gizmos.DrawWireCube(new Vector3(x, 0, z), Vector3.one);
            }
        }

        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}

public class GridSpace
{
    public Vector2Int coordinates;

    public GameObject placedObject;

    public GridSpace(int x, int z)
    {
        coordinates = new Vector2Int(x, z);
    }
}