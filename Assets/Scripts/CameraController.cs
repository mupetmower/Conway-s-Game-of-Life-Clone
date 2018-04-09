using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    const int camMaxOrthoSize = 200;
    const int camMinOrthoSize = 20;
    const int camOrthoSizeChange = 5;

    int camOrthoSize = 55;

    Vector3 camOriginalPos;
    Vector3 camStartDragPos;
    Vector3 camDragDif;
    bool dragging = false;


    private void Start()
    {
        Camera.main.transform.position = new Vector3((float)GameManager.instance.GetCellD1Length() / 2f, (float)GameManager.instance.GetCellD2Length() / 2f, -10f);
        camOriginalPos = GetComponent<Camera>().transform.position;
    }

    // Update is called once per frame
    void Update () {

		if (Input.mouseScrollDelta.y < 0)
        {
            camOrthoSize += camOrthoSizeChange;
            camOrthoSize = Mathf.Clamp(camOrthoSize, camMinOrthoSize, camMaxOrthoSize);

            GetComponent<Camera>().orthographicSize = camOrthoSize;
        } else if (Input.mouseScrollDelta.y > 0)
        {
            camOrthoSize -= camOrthoSizeChange;
            camOrthoSize = Mathf.Clamp(camOrthoSize, camMinOrthoSize, camMaxOrthoSize);

            GetComponent<Camera>().orthographicSize = camOrthoSize;
        }
	}


       
    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            camDragDif = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (dragging == false)
            {
                dragging = true;
                camOriginalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            dragging = false;
        }
        if (dragging == true)
        {
            Camera.main.transform.position = camOriginalPos - camDragDif;
        }


    }
}
