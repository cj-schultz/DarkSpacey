using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RoomSetup : MonoBehaviour
{
    public GameObject[] cornerMarkers;

    private void Start()
    {
        //area.
        HmdQuad_t pRect = new HmdQuad_t();        
        if(SteamVR_PlayArea.GetBounds(SteamVR_PlayArea.Size.Calibrated, ref pRect) && cornerMarkers.Length == 4)
        {
            cornerMarkers[0].transform.position = new Vector3(pRect.vCorners0.v0, pRect.vCorners0.v1, pRect.vCorners0.v2);
            cornerMarkers[1].transform.position = new Vector3(pRect.vCorners1.v0, pRect.vCorners1.v1, pRect.vCorners1.v2);
            cornerMarkers[2].transform.position = new Vector3(pRect.vCorners2.v0, pRect.vCorners2.v1, pRect.vCorners2.v2);
            cornerMarkers[3].transform.position = new Vector3(pRect.vCorners3.v0, pRect.vCorners3.v1, pRect.vCorners3.v2);            
        }        
    }
}
