using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractObject : MonoBehaviour, ILightDarkModeSwitch
{
    public abstract void OnDarkModeEnter();
    public abstract void OnLightModeEnter();
}
