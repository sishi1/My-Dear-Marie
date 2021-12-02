using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour, ILightDarkModeSwitch
{
    public float speed;
    public Collider coll;

    public abstract void OnDarkModeEnter();
    public abstract void OnLightModeEnter();
}
