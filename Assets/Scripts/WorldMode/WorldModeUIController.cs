using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldModeUIController : MonoBehaviour {

    public void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void TimeX1()
    {
        Time.timeScale = .005f;
    }

    public void TimeX5()
    {
        Time.timeScale = .025f;
    }

    public void TimeX10()
    {
        Time.timeScale = .05f;
    }

    public void TimeX20()
    {
        Time.timeScale = .1f;
    }

    public void TimeX50()
    {
        Time.timeScale = .25f;
    }

    public void TimeX100()
    {
        Time.timeScale = .5f;
    }

    public void TimeX200()
    {
        Time.timeScale = 1f;
    }




    public void KillAllCells()
    {
        WorldModeGameManager.instance.KillAllCells();
    }
}
