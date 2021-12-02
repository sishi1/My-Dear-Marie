using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SjorsGielen.Events;

public class ToggleWorldMode : MonoBehaviour
{

    //GameEvents
    public GameEvent setToDarkWorld;
    public GameEvent setToLightWorld;   

    public WorldStatus CurrentWorld { get; private set; }
  
    private void Awake()
    {
        CurrentWorld = WorldStatus.darkworld;
    }

    public void Start()
    {
        //trigger the event once at the start of play.
        switch (CurrentWorld)
        {
            case WorldStatus.lightworld:
                setToLightWorld.Raise();
                break;

            case WorldStatus.darkworld:
                setToDarkWorld.Raise();
                break;

            default:
                Debug.LogFormat("State {(0)} not found ", CurrentWorld);
                break;
        }
    }

    //Method to change states based on the current world state
    public void SwitchWorld()
    {
        switch (CurrentWorld)
        {
            case WorldStatus.lightworld:

                CurrentWorld = WorldStatus.darkworld;
                setToDarkWorld.Raise();
                break;

            case WorldStatus.darkworld:

                CurrentWorld = WorldStatus.lightworld;
                setToLightWorld.Raise();
                break;

            default:

                Debug.LogFormat("WorldSwap issue: 404 Status not Found ({0})", CurrentWorld);

                break;
        }
    }
}
