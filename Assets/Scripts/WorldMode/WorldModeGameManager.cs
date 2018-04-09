using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldModeGameManager : MonoBehaviour {

    public static WorldModeGameManager instance = null;


    public EventSystem eventSys;

    //WorldModeGridManager gmi;

    public WorldModeCell[][] cells;

    public GameObject cellManager;

    GameObject liveCount;
    GameObject genCount;


    public static int generationCount;


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


    private void Start()
    {

        liveCount = GameObject.Find("WMLiveCellCount");
        genCount = GameObject.Find("WMGenerationCount");

        

        eventSys = GameObject.Find("EventSystem").GetComponent<EventSystem>();


        cells = new WorldModeCell[WorldModeGridManager.instance.getColCount()][];
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = new WorldModeCell[WorldModeGridManager.instance.getRowCount()];
        }



        WorldModeGridManager.instance.InitGrid();



        if (WorldModeCellManager.instance == null)
        {
            //Instantiate gameManager prefab if none exists
            Instantiate(cellManager);

        }




        for (int x = 0; x < WorldModeCellManager.instance.getCellArray().Length; x++)
        {
            for (int y = 0; y < WorldModeCellManager.instance.getCellArray()[x].Length; y++)
            {
                cells[x][y].CreateNeighborhood();
            }
        }


        generationCount = 0;

        Time.timeScale = 0f;
    }


    private void Update()
    {

        UpdateCellCountText();

    }

    private void FixedUpdate()
    {
        for (int x = 0; x < WorldModeGridManager.instance.getColCount(); x++)
        {
            for (int y = 0; y < WorldModeGridManager.instance.getRowCount(); y++)
            {
                cells[x][y].UpdateNeighborhoodStates();
            }
        }

        for (int x = 0; x < WorldModeGridManager.instance.getColCount(); x++)
        {
            for (int y = 0; y < WorldModeGridManager.instance.getRowCount(); y++)
            {
                cells[x][y].RaiseGeneration();
            }
        }

        generationCount += 1;
        UpdateGenerationCountText();

    }


    public void AddCellToArray(WorldModeCell c, int x, int y)
    {
        cells[x][y] = c;
    }


    public void UpdateGenerationCountText()
    {
        genCount.GetComponent<Text>().text = "Generation: " + generationCount.ToString();
    }

    public void UpdateCellCountText()
    {
        liveCount.GetComponent<Text>().text = "Live Cells: " + CountLiveCells().ToString();
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


    public WorldModeCell[][] getCellArray()
    {
        return cells;
    }



    //#region Neighborhood
    //public WorldModeCell NeighborN(GameObject c)
    //{
    //    int x;
    //    int y;

    //    if ((int)c.transform.position.y == cells[(int)c.transform.position.x].Length - 1)
    //    {
    //        x = (int)c.transform.position.x;
    //        y = 0;
    //    }
    //    else
    //    {
    //        x = (int)c.transform.position.x;
    //        y = (int)c.transform.position.y + 1;
    //    }

    //    return cells[x][y];
    //}
    //public WorldModeCell NeighborNE(GameObject c)
    //{
    //    int x;
    //    int y;

    //    if ((int)c.transform.position.x == cells.Length - 1 && (int)c.transform.position.y == cells[(int)c.transform.position.x].Length - 1)
    //    {
    //        x = 0;
    //        y = 0;
    //    }

    //    else if ((int)c.transform.position.x == cells.Length - 1)
    //    {
    //        x = 0;
    //        y = (int)c.transform.position.y + 1;
    //    }
    //    else if ((int)c.transform.position.y == cells[(int)c.transform.position.x].Length - 1)
    //    {
    //        x = (int)c.transform.position.x + 1;
    //        y = 0;
    //    }
    //    else
    //    {
    //        x = (int)c.transform.position.x + 1;
    //        y = (int)c.transform.position.y + 1;
    //    }

    //    return cells[x][y];
    //}
    //public WorldModeCell NeighborE(GameObject c)
    //{
    //    int x;
    //    int y;

    //    if ((int)c.transform.position.x == cells.Length - 1)
    //    {
    //        x = 0;
    //        y = (int)c.transform.position.y;
    //    }
    //    else
    //    {
    //        x = (int)c.transform.position.x + 1;
    //        y = (int)c.transform.position.y;
    //    }

    //    return cells[x][y];
    //}
    //public WorldModeCell NeighborSE(GameObject c)
    //{
    //    int x;
    //    int y;

    //    if ((int)c.transform.position.x == cells.Length - 1 && (int)c.transform.position.y == 0)
    //    {
    //        x = 0;
    //        y = cells[x].Length - 1;
    //    }

    //    else if ((int)c.transform.position.x == gm.columns - 1)
    //    {
    //        x = 0;
    //        y = (int)c.transform.position.y - 1;
    //    }
    //    else if ((int)c.transform.position.y == 0)
    //    {
    //        x = (int)c.transform.position.x + 1;
    //        y = cells[x].Length - 1;
    //    }
    //    else
    //    {
    //        x = (int)c.transform.position.x + 1;
    //        y = (int)c.transform.position.y - 1;
    //    }

    //    return cells[x][y];
    //}
    //public WorldModeCell NeighborS(GameObject c)
    //{
    //    int x;
    //    int y;

    //    if ((int)c.transform.position.y == 0)
    //    {
    //        x = (int)c.transform.position.x;
    //        y = cells[x].Length - 1;
    //    }
    //    else
    //    {
    //        x = (int)c.transform.position.x;
    //        y = (int)c.transform.position.y - 1;
    //    }

    //    return cells[x][y];
    //}
    //public WorldModeCell NeighborSW(GameObject c)
    //{
    //    int x;
    //    int y;

    //    if ((int)c.gameObject.transform.position.x == 0 && (int)c.gameObject.transform.position.y == 0)
    //    {
    //        x = cells.Length - 1;
    //        y = cells[x].Length - 1;
    //    }

    //    else if ((int)c.gameObject.transform.position.x == 0)
    //    {
    //        x = cells.Length - 1;
    //        y = (int)c.gameObject.transform.position.y - 1;
    //    }
    //    else if ((int)c.gameObject.transform.position.y == 0)
    //    {
    //        x = (int)c.gameObject.transform.position.x - 1;
    //        y = cells[x].Length - 1;
    //    }
    //    else
    //    {
    //        x = (int)c.gameObject.transform.position.x - 1;
    //        y = (int)c.gameObject.transform.position.y - 1;
    //    }

    //    return cells[x][y];
    //}
    //public WorldModeCell NeighborW(GameObject c)
    //{
    //    int x;
    //    int y;

    //    if ((int)c.transform.position.x == 0)
    //    {
    //        x = cells.Length - 1;
    //        y = (int)c.transform.position.y;
    //    }
    //    else
    //    {
    //        x = (int)c.transform.position.x - 1;
    //        y = (int)c.transform.position.y;
    //    }

    //    return cells[x][y];
    //}
    //public WorldModeCell NeighborNW(GameObject c)
    //{
    //    int x;
    //    int y;

    //    if ((int)c.transform.position.x == 0 && (int)c.transform.position.y == cells[(int)c.transform.position.x].Length - 1)
    //    {
    //        x = cells.Length - 1;
    //        y = 0;
    //    }

    //    else if ((int)c.transform.position.x == 0)
    //    {
    //        x = cells.Length - 1;
    //        y = (int)c.transform.position.y + 1;
    //    }
    //    else if ((int)c.transform.position.y == cells[(int)c.transform.position.x].Length - 1)
    //    {
    //        x = (int)c.transform.position.x - 1; 
    //        y = 0;
    //    }
    //    else
    //    {
    //        x = (int)c.transform.position.x - 1;
    //        y = (int)c.transform.position.y + 1;
    //    }

    //    return cells[x][y];
    //}

    //#endregion


    public void KillAllCells()
    {
        for (int x = 0; x < cells.Length; x++)
        {
            for (int y = 0; y < cells[x].Length; y++)
            {
                WorldModeCellManager.instance.getCellArray()[x][y].KillCell();
            }
        }
    }


    public int GetCellD1Length()
    {
        return cells.Length;
    }
    public int GetCellD2Length()
    {
        return cells[0].Length;
    }





}
