using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformParentHack : MonoBehaviour
{
    [SerializeField] private Transform parent;
    void Awake()
    {
        this.transform.parent = parent;
    }
}
