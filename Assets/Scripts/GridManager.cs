using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    public enum CellType
    {
        Dead, Alive
    }

    public enum GroundType
    {
        Grass, Water
    }



    public GameObject gridHolder;
    public GameObject cellHolder;

    public int columns = 100;                                 // The number of columns on the board (how wide it will be).
    public int rows = 100;

    public CellType[][] grid;

    public GroundType[][] groundGrid;

    public GameObject Cell;

    public GameObject grassTile;
    public GameObject waterTile;



    public void InitGrid()
    {
        gridHolder = new GameObject("GridHolder");
        cellHolder = new GameObject("CellHolder");

        SetupCellGridArray();
        CreateCells();
        PlaceCells();

    }


    void SetupCellGridArray()
    {
        // Set the grid to contain the correct width and height.
        grid = new CellType[columns][];
        for (int i = 0; i < grid.Length; i++)
        {            
            grid[i] = new CellType[rows];
        }
    }

    void SetupCellGridArray(int _rows, int _cols)
    {
        // Set the grid to contain the correct width and height.
        grid = new CellType[columns][];
        for (int i = 0; i < grid.Length; i++)
        {            
            grid[i] = new CellType[rows];
        }
    }

    void SetupGroundGridArray(int _rows, int _cols)
    {
        // Set the grid to contain the correct width and height.
        groundGrid = new GroundType[columns][];
        for (int i = 0; i < grid.Length; i++)
        {
            groundGrid[i] = new GroundType[rows];
        }
    }

    //Used to create and also clear grid.
    public void CreateCells()
    {
        for (int x = 0; x < grid.Length; x++)
        {
            for (int y = 0; y < grid[x].Length; y++)
            {
                grid[x][y] = CellType.Dead;                
            }
        }

    }


    public void CreateGroundTiles()
    {
        for (int x = 0; x < groundGrid.Length; x++)
        {
            for (int y = 0; y < groundGrid[x].Length; y++)
            {
                groundGrid[x][y] = GroundType.Grass;
            }
        }

    }


    private void PlaceCells()
    {
        for (int x = 0; x < grid.Length; x++)
        {
            for (int y = 0; y < grid[x].Length; y++)
            {
                InstantiateCells(Cell, x, y);
            }
        }
    }

    private void PlaceGroundTiles()
    {
        for (int x = 0; x < groundGrid.Length; x++)
        {
            for (int y = 0; y < groundGrid[x].Length; y++)
            {
                switch (groundGrid[x][y])
                {
                    case GroundType.Grass:
                        InstantiateCells(grassTile, x, y);
                        break;
                    case GroundType.Water:
                        InstantiateCells(waterTile, x, y);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void InstantiateCells(GameObject pCell, float x, float y)
    {

        Vector3 position = new Vector3(x, y, 0f);

        // Create an instance of the prefab from the random index of the array.
        GameObject tileInstance = Instantiate(pCell, position, Quaternion.identity) as GameObject;

        // Set the tile's parent to the board holder.
        tileInstance.transform.parent = gridHolder.transform;

        tileInstance.name += " " + tileInstance.transform.position.x.ToString() + ", " + tileInstance.transform.position.y.ToString();
    }

}
