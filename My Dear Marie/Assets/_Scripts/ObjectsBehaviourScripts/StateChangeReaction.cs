using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChangeReaction : MonoBehaviour, ILightDarkModeSwitch
{
    Color darkColor = Color.magenta;
    Color lightColor = Color.grey;

    [SerializeField] private Material dark, light;
    private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    // Changes color in dark and light mode for indication.
    public void OnDarkModeEnter()
    {
        if (dark == null) renderer.material.color = darkColor;
        else renderer.material = dark;
    }

    public void OnLightModeEnter()
    {
        if (light == null) renderer.material.color = lightColor;
        else renderer.material = light;
    }
}
