using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractions : MonoBehaviour
{
    [SerializeField] private Transform roomPosition;
    [SerializeField] private Transform gardenPosition;
    [SerializeField] private GameObject player;
    [SerializeField] private MeshRenderer handleMesh;

    public void ChangeScenery(bool toRoom)
    {
        if (toRoom)
        {
            print("Going to room");
            player.transform.position = roomPosition.position;
        }
        else
        {
            print("Going to garden");
            player.transform.position = gardenPosition.position;
        }
    }

    public void ActivateDoor()
    {
        handleMesh.material.color = Color.green;
    }

    public void DeactivateDoor()
    {
        handleMesh.material.color= Color.white;
    }
}
