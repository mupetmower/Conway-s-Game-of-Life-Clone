using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour {

    public enum CellState
    {
        Dead, Live
    }


    public CellState state;

    GameObject deadCell;
    GameObject liveCell;

    public CellManager[] neighborhood = new CellManager[8];

    public CellState[] neighborhoodStates = new CellState[8];


    public int[] popToLive;
    public int[] popForBirth;


    //public int underPopDeath = 2;
    //public int overPopDeath = 3;
    //public int rePop = 3;


    //int liveNeighbors;



    private void Awake()
    {
        state = CellState.Dead;


        deadCell = transform.Find("DeadCell").gameObject;
        liveCell = transform.Find("LiveCell").gameObject;

        GameManager.instance.AddCellToArray(GetComponent<CellManager>(), (int)gameObject.transform.position.x, (int)gameObject.transform.position.y);

        UpdateImage();



    }


    public void CreateNeighborhood()
    {
        neighborhood[0] = GameManager.instance.NeighborN(gameObject);
        neighborhood[1] = GameManager.instance.NeighborNE(gameObject);
        neighborhood[2] = GameManager.instance.NeighborE(gameObject);
        neighborhood[3] = GameManager.instance.NeighborSE(gameObject);
        neighborhood[4] = GameManager.instance.NeighborS(gameObject);
        neighborhood[5] = GameManager.instance.NeighborSW(gameObject);
        neighborhood[6] = GameManager.instance.NeighborW(gameObject);
        neighborhood[7] = GameManager.instance.NeighborNW(gameObject);
    }


    public CellState GetState()
    {
        return state;
    }


    public void UpdateImage()
    {
        if (GetState() == CellState.Dead)
        {
            deadCell.SetActive(true);
            liveCell.SetActive(false);
        } else if (GetState() == CellState.Live)
        {
            deadCell.SetActive(false);
            liveCell.SetActive(true);
        }
    }

    public void ChangeState()
    {
        if (GetState() == CellState.Live)
        {
            state = CellState.Dead;
        } else if (GetState() == CellState.Dead)
        {
            state = CellState.Live;
        }
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
            neighborhoodStates[i] = neighborhood[i].GetState();
        }
    }



    public void RaiseGeneration()
    {

        int liveNeighbors = CheckNeighborStates();

        bool live = false;
        bool revive = false;

        if (GetState() == CellState.Live)
        {
            foreach (int i in popToLive)
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


        } else if (GetState() == CellState.Dead)
        {
            foreach (int i in popForBirth)
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


    public void KillCell()
    {
        if (GetState() == CellState.Live)
        {
            ChangeState();
            UpdateImage();
        }
    }


    private void OnMouseDown()
    {


        if (GameManager.instance.eventSys.IsPointerOverGameObject())
        {
            return;
        }
        

        ChangeState();
        UpdateImage();


    }


    private void OnMouseEnter()
    {

        if (GameManager.instance.eventSys.IsPointerOverGameObject())
        {
            return;
        }


        if (Input.GetMouseButton(0))
        {
            ChangeState();
            UpdateImage();
        }
    }






}
