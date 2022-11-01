using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBehaviour : MonoBehaviourPun
{
    [SerializeField] private Material[] materials;
    private MeshRenderer meshRenderer;
    private Vector3 eulerAngle = new Vector3(0, 15, 0);
    private bool rotatingEnabled = true;
    private int currentMaterialIndex = 0;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetKeyDown(KeyCode.R)) rotatingEnabled = !rotatingEnabled;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentMaterialIndex++;
            if (currentMaterialIndex >materials.Length - 1)
            {
                currentMaterialIndex = 0;
            }
            photonView.RPC("ChangeMaterial", RpcTarget.AllBuffered, currentMaterialIndex);
        }
        if (rotatingEnabled) Rotate();
    }
    private void Rotate() 
    {
        transform.Rotate(eulerAngle * Time.deltaTime);
    }

    [PunRPC]
    public void ChangeMaterial(int materialIndex)
    {
        meshRenderer.material = materials[materialIndex];
    }
}
