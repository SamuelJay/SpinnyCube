using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBehaviour : MonoBehaviourPun 
{

    private Vector3 eulerAngle = new Vector3(0, 15, 0);
    private bool rotatingEnabled = true;

    private void Update()
    {
        if (!photonView.IsMine) return;
        
        if (Input.GetKeyDown(KeyCode.R)) rotatingEnabled = !rotatingEnabled;
    
        if (rotatingEnabled) transform.Rotate(eulerAngle * Time.deltaTime);
    }
}
