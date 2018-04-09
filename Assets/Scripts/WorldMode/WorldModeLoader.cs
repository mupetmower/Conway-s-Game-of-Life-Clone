using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldModeLoader : MonoBehaviour {

    public GameObject worldModeGameManager;

    void Awake()
    {
        if (GameManager.instance == null)
        {
            //Instantiate gameManager prefab if none exists
            Instantiate(worldModeGameManager);

        }
    }
}
