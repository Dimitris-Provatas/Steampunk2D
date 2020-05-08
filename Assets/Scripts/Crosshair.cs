using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public GameObject crosshair;
    public Camera cam;

    void Update()
    {
        #region Crosshair
        Vector3 aim = cam.ScreenToWorldPoint(Input.mousePosition);
        aim.z = 0;
        crosshair.transform.position = aim;
        #endregion
    }
}
