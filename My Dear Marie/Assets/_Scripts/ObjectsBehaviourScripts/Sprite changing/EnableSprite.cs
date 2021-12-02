using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSprite : MonoBehaviour, ILightDarkModeSwitch
{
    private SpriteRenderer spriteR;
    private void Awake()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }
    public void OnDarkModeEnter()
    {
        spriteR.enabled = true;
    }

    public void OnLightModeEnter()
    {
        spriteR.enabled = false;
    }
}
