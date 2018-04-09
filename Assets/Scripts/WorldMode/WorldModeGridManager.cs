using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldModeGridManager : MonoBehaviour {


    public static WorldModeGridManager instance = null;


    public GameObject gridHolder;
    public GameObject cellHolder;

    
    int columns = 175;
    int rows = 175;

    //public CellState[][] grid;

    public GroundType[][] groundGrid;

    public GameObject Cell;

    public GameObject grassTile;
    public GameObject waterTile;



    void Awake()
    {
        //Enforce singelton pattern, so there will always only ever be one GameManager
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        else if (instance != this)                //If instance already exists and it's not this:
        {
            //Then destroy this. This enforces singleton pattern
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

    }


    public void InitGrid()
    {
        gridHolder = new GameObject("WMGridHolder");
        cellHolder = new GameObject("WMCellHolder");

        SetupGroundGridArray();
        CreateGroundTiles();
        PlaceGroundTiles();
        PlaceCells();

    }


    void SetupGroundGridArray()
    {
        // Set the grid to contain the correct width and height.
        groundGrid = new GroundType[getColCount()][];
        for (int i = 0; i < groundGrid.Length; i++)
        {
            groundGrid[i] = new GroundType[getRowCount()];
        }
    }

    

    //Used to create and also clear grid.
    public void CreateGroundTiles()
    {
        for (int x = 0; x < groundGrid.Length; x++)
        {
            for (int y = 0; y < groundGrid[x].Length; y++)
            {
                if ((x >= 50 && y >= 60) && (x <= 100 && y <= 120))
                {
                    groundGrid[x][y] = GroundType.Grass;
                }
                else if ((x >= 100 && y >= 101) && (x <= 145 && y <= 130))
                {
                    groundGrid[x][y] = GroundType.Grass;
                }
                else if ((x >= 105 && y >= 15) && (x <= 130 && y <= 45))
                {
                    groundGrid[x][y] = GroundType.Grass;
                }
                else if ((x >= 0 && x <= 30) && (y >= 30 && y <= 55))
                {
                    groundGrid[x][y] = GroundType.Grass;
                }
                else
                {
                    groundGrid[x][y] = GroundType.Water;
                }
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
                        InstantiateObject(grassTile, x, y);
                        break;
                    case GroundType.Water:
                        InstantiateObject(waterTile, x, y);
                        break;
                    default:
                        break;
                }
            }
        }
    }



    private void PlaceCells()
    {
        for (int x = 0; x < groundGrid.Length; x++)
        {
            for (int y = 0; y < groundGrid[x].Length; y++)
            {
                InstantiateObject(Cell, x, y);
            }
        }
    }



    void InstantiateObject(GameObject obj, float x, float y)
    {

        Vector3 position = new Vector3(x, y, 0f);

        // Create an instance of the prefab from the random index of the array.
        GameObject tileInstance = Instantiate(obj, position, Quaternion.identity) as GameObject;

        // Set the tile's parent to the board holder.
        if (tileInstance.tag.Equals("Cell"))
        {
            tileInstance.transform.parent = cellHolder.transform;
        } else if (tileInstance.tag.Equals("Ground"))
        {
            tileInstance.transform.parent = gridHolder.transform;
        }

        //Give the new object instance a name.
        tileInstance.name += " (" + tileInstance.transform.position.x.ToString() + ", " + tileInstance.transform.position.y.ToString() + ")";
    }



    public GroundType getGroundTypeFromGrid(float x, float y)
    {
        return groundGrid[(int)x][(int)y];
    }

    public int getRowCount()
    {
        return rows;
    }
    public int getColCount()
    {
        return columns;
    }

}
