using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldModeCellManager : MonoBehaviour {


    public static WorldModeCellManager instance = null;


    //WorldModeGridManager gridManager;


    public WorldModeCell[][] cells;






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


        cells = new WorldModeCell[WorldModeGridManager.instance.getColCount()][];
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = new WorldModeCell[WorldModeGridManager.instance.getRowCount()];
        }




    }


    private void Start()
    {


        cells = WorldModeGameManager.instance.getCellArray();





    }



    


    public int CountLiveCells()
    {
        int live = 0;

        for (int x = 0; x < cells.Length; x++)
        {
            for (int y = 0; y < cells[x].Length; y++)
            {
                if (cells[x][y].getCellState() == CellState.Live)
                {
                    live += 1;
                }
            }
        }

        return live;
    }


    public int GetCellD1Length()
    {
        return cells.Length;
    }
    public int GetCellD2Length()
    {
        return cells[0].Length;
    }






    public WorldModeCell[][] getCellArray()
    {
        return cells;
    }








  




    #region Neighborhood
    public WorldModeCell NeighborN(GameObject c)
    {
        int x;
        int y;

        if ((int)c.transform.position.y == cells[(int)c.transform.position.x].Length - 1)
        {
            x = (int)c.transform.position.x;
            y = 0;
        }
        else
        {
            x = (int)c.transform.position.x;
            y = (int)c.transform.position.y + 1;
        }

        return WorldModeGameManager.instance.cells[x][y];
    }
    public WorldModeCell NeighborNE(GameObject c)
    {
        int x;
        int y;

        if ((int)c.transform.position.x == cells.Length - 1 && (int)c.transform.position.y == cells[(int)c.transform.position.x].Length - 1)
        {
            x = 0;
            y = 0;
        }

        else if ((int)c.transform.position.x == cells.Length - 1)
        {
            x = 0;
            y = (int)c.transform.position.y + 1;
        }
        else if ((int)c.transform.position.y == cells[(int)c.transform.position.x].Length - 1)
        {
            x = (int)c.transform.position.x + 1;
            y = 0;
        }
        else
        {
            x = (int)c.transform.position.x + 1;
            y = (int)c.transform.position.y + 1;
        }

        return WorldModeGameManager.instance.cells[x][y];
    }
    public WorldModeCell NeighborE(GameObject c)
    {
        int x;
        int y;

        if ((int)c.transform.position.x == cells.Length - 1)
        {
            x = 0;
            y = (int)c.transform.position.y;
        }
        else
        {
            x = (int)c.transform.position.x + 1;
            y = (int)c.transform.position.y;
        }

        return WorldModeGameManager.instance.cells[x][y];
    }
    public WorldModeCell NeighborSE(GameObject c)
    {
        int x;
        int y;

        if ((int)c.transform.position.x == cells.Length - 1 && (int)c.transform.position.y == 0)
        {
            x = 0;
            y = cells[x].Length - 1;
        }

        else if ((int)c.transform.position.x == WorldModeGridManager.instance.getColCount() - 1)
        {
            x = 0;
            y = (int)c.transform.position.y - 1;
        }
        else if ((int)c.transform.position.y == 0)
        {
            x = (int)c.transform.position.x + 1;
            y = cells[x].Length - 1;
        }
        else
        {
            x = (int)c.transform.position.x + 1;
            y = (int)c.transform.position.y - 1;
        }

        return WorldModeGameManager.instance.cells[x][y];
    }
    public WorldModeCell NeighborS(GameObject c)
    {
        int x;
        int y;

        if ((int)c.transform.position.y == 0)
        {
            x = (int)c.transform.position.x;
            y = cells[x].Length - 1;
        }
        else
        {
            x = (int)c.transform.position.x;
            y = (int)c.transform.position.y - 1;
        }

        return WorldModeGameManager.instance.cells[x][y];
    }
    public WorldModeCell NeighborSW(GameObject c)
    {
        int x;
        int y;

        if ((int)c.gameObject.transform.position.x == 0 && (int)c.gameObject.transform.position.y == 0)
        {
            x = cells.Length - 1;
            y = cells[x].Length - 1;
        }

        else if ((int)c.gameObject.transform.position.x == 0)
        {
            x = cells.Length - 1;
            y = (int)c.gameObject.transform.position.y - 1;
        }
        else if ((int)c.gameObject.transform.position.y == 0)
        {
            x = (int)c.gameObject.transform.position.x - 1;
            y = cells[x].Length - 1;
        }
        else
        {
            x = (int)c.gameObject.transform.position.x - 1;
            y = (int)c.gameObject.transform.position.y - 1;
        }

        return WorldModeGameManager.instance.cells[x][y];
    }
    public WorldModeCell NeighborW(GameObject c)
    {
        int x;
        int y;

        if ((int)c.transform.position.x == 0)
        {
            x = cells.Length - 1;
            y = (int)c.transform.position.y;
        }
        else
        {
            x = (int)c.transform.position.x - 1;
            y = (int)c.transform.position.y;
        }

        return WorldModeGameManager.instance.cells[x][y];
    }
    public WorldModeCell NeighborNW(GameObject c)
    {
        int x;
        int y;

        if ((int)c.transform.position.x == 0 && (int)c.transform.position.y == cells[(int)c.transform.position.x].Length - 1)
        {
            x = cells.Length - 1;
            y = 0;
        }

        else if ((int)c.transform.position.x == 0)
        {
            x = cells.Length - 1;
            y = (int)c.transform.position.y + 1;
        }
        else if ((int)c.transform.position.y == cells[(int)c.transform.position.x].Length - 1)
        {
            x = (int)c.transform.position.x - 1;
            y = 0;
        }
        else
        {
            x = (int)c.transform.position.x - 1;
            y = (int)c.transform.position.y + 1;
        }

        return WorldModeGameManager.instance.cells[x][y];
    }

    #endregion



}
