using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDarkSwitch : MonoBehaviour, ILightDarkModeSwitch
{
    
    Color lightModeColor = Color.white;
    [SerializeField]
    Color darkMordeColor = Color.grey;

    [SerializeField]
    public Light myLight;

    public void OnDarkModeEnter()
    {
        myLight.color = darkMordeColor;
    }

    public void OnLightModeEnter()
    {
        myLight.color = lightModeColor;
    }
}
