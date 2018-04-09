using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellState
{
    Dead, Live
}

public enum GroundType
{
    Water, Grass
}

public class WorldModeCell : MonoBehaviour {




    private GroundType ground;
    private CellState state;

    private GameObject cellImage;

    public WorldModeCell[] neighborhood = new WorldModeCell[8];

    public CellState[] neighborhoodStates = new CellState[8];


    private int[] popToLive;
    private int[] popForBirth;

    private float gridXLoc;
    private float gridYLoc;

    public Sprite liveCellSprite;


    private void Awake()
    {
        setCellState(CellState.Dead);
        setGridXLoc(transform.position.x);
        setGridYLoc(transform.position.y);

        setGroundType(WorldModeGridManager.instance.getGroundTypeFromGrid(getGridXLoc(), getGridYLoc()));

        switch (getGroundType())
        {
            case GroundType.Grass:
                setPopToLive(new int[] { 2, 3 });
                setPopForBirth(new int[] { 3 });
                break;
            case GroundType.Water:
                setPopToLive(new int[] { 3, 6 });
                setPopForBirth(new int[] { 3, 4 });
                break;
            default:
                break;
        }

        WorldModeGameManager.instance.AddCellToArray(GetComponent<WorldModeCell>(), (int)getGridXLoc(), (int)getGridYLoc());

        cellImage = transform.Find("CellImage").gameObject;

        UpdateImage();
        
    }



    public void UpdateImage()
    {
        if (getCellState() == CellState.Dead)
        {
            cellImage.SetActive(false);
        }
        else if (getCellState() == CellState.Live)
        {
            cellImage.SetActive(true);
            //Change Image
            
        }
    }

    public void ChangeState()
    {
        if (getCellState() == CellState.Live)
        {
            setCellState(CellState.Dead);
        }
        else if (getCellState() == CellState.Dead)
        {
            setCellState(CellState.Live);
        }
    }



    public void KillCell()
    {
        if (getCellState() == CellState.Live)
        {
            ChangeState();
            UpdateImage();
        }
    }



    private void OnMouseDown()
    {


        if (WorldModeGameManager.instance.eventSys.IsPointerOverGameObject())
        {
            return;
        }


        ChangeState();
        UpdateImage();


    }


    private void OnMouseEnter()
    {

        if (WorldModeGameManager.instance.eventSys.IsPointerOverGameObject())
        {
            return;
        }


        if (Input.GetMouseButton(0))
        {
            ChangeState();
            UpdateImage();
        }
    }



    public void CreateNeighborhood()
    {
        neighborhood[0] = WorldModeCellManager.instance.NeighborN(gameObject);
        neighborhood[1] = WorldModeCellManager.instance.NeighborNE(gameObject);
        neighborhood[2] = WorldModeCellManager.instance.NeighborE(gameObject);
        neighborhood[3] = WorldModeCellManager.instance.NeighborSE(gameObject);
        neighborhood[4] = WorldModeCellManager.instance.NeighborS(gameObject);
        neighborhood[5] = WorldModeCellManager.instance.NeighborSW(gameObject);
        neighborhood[6] = WorldModeCellManager.instance.NeighborW(gameObject);
        neighborhood[7] = WorldModeCellManager.instance.NeighborNW(gameObject);
    }



    public int CheckNeighborStates()
    {
        int live = 0;

        for (int i = 0; i < neighborhood.Length; i++)
        {
            if (neighborhoodStates[i] == CellState.Live)
            {
                live++;
            }
        }

        return live;


    }


    public void UpdateNeighborhoodStates()
    {
        for (int i = 0; i < neighborhood.Length; i++)
        {
            neighborhoodStates[i] = neighborhood[i].getCellState();
        }
    }



    public void RaiseGeneration()
    {

        int liveNeighbors = CheckNeighborStates();

        bool live = false;
        bool revive = false;

        if (getCellState() == CellState.Live)
        {
            foreach (int i in getPopToLive())
            {
                if (liveNeighbors == i)
                {
                    live = true;
                    break;
                }
            }

            if (!live)
            {
                ChangeState();
                UpdateImage();
            }


            //if (liveNeighbors < underPopDeath || liveNeighbors > overPopDeath)
            //{
            //    ChangeState();
            //    UpdateImage();
            //}


        }
        else if (getCellState() == CellState.Dead)
        {
            foreach (int i in getPopForBirth())
            {
                if (liveNeighbors == i)
                {
                    revive = true;
                    break;
                }
            }

            if (revive)
            {
                ChangeState();
                UpdateImage();
            }

            //if (liveNeighbors == rePop)
            //{
            //    ChangeState();
            //    UpdateImage();
            //}
        }


    }




    public void setGroundType(GroundType mGround)
    {
        ground = mGround;
    }
    public GroundType getGroundType()
    {
        return ground;
    }

    public void setCellState(CellState mState)
    {
        state = mState;
    }
    public CellState getCellState()
    {
        return state;
    }

    public void setGridXLoc(float x)
    {
        gridXLoc = x;
    }
    public float getGridXLoc()
    {
        return gridXLoc;
    }

    public void setGridYLoc(float y)
    {
        gridYLoc = y;
    }
    public float getGridYLoc()
    {
        return gridYLoc;
    }

    public void setCellImage(GameObject image)
    {
        cellImage = image;
    }
    public GameObject getCellImage()
    {
        return cellImage;
    }

    public void setPopToLive(int[] live)
    {
        popToLive = live;
    }
    public int[] getPopToLive()
    {
        return popToLive;
    }

    public void setPopForBirth(int[] birth)
    {
        popForBirth = birth;
    }
    public int[] getPopForBirth()
    {
        return popForBirth ;
    }


}
