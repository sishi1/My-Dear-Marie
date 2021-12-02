using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    MarieController player;
    ChannelAbility channelAbility;

    //How much more charges you want to give the player
    private const float EXTRACHARGE = 10;
    //The position change is based on the scale extension 
    private const float POSITIONCHANGE = 10;
    private const float SCALEEXTENSION = .1f;

    private void Awake()
    {
        channelAbility = GameObject.FindObjectOfType<ChannelAbility>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        player = collision.gameObject.GetComponent<MarieController>();

        if (player != null)
        {
            //Gives the maxCharge some extra charges
            channelAbility.Charge += EXTRACHARGE;
            //Changes the scale
            channelAbility.barBG.rectTransform.localScale += new Vector3(SCALEEXTENSION, 0, 0);
            //Changes the position
            channelAbility.barBG.rectTransform.anchoredPosition3D += new Vector3(POSITIONCHANGE, 0, 0);
            Destroy(gameObject);
        }
    }
}
