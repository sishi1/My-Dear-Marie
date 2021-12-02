using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour, ILightDarkModeSwitch
{
    private SpriteRenderer spriteR;
    [SerializeField] private Sprite dark, light;

    private void Awake()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

    public void OnDarkModeEnter()
    {
        spriteR.sprite = dark;
    }

    public void OnLightModeEnter()
    {
        spriteR.sprite = light;
    }
}

