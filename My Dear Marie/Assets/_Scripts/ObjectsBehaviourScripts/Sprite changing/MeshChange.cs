using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshChange : MonoBehaviour, ILightDarkModeSwitch
{
    [SerializeField] private Mesh dark, light;
    private MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    public void OnDarkModeEnter()
    {
        meshFilter.mesh = dark;
    }

    public void OnLightModeEnter()
    {
        meshFilter.mesh = light;    
    }
}
