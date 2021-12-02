using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class BwosyScwript : MonoBehaviour
{
    [SerializeField]
    MarieController player;
    public LayerMask ignoreMask;
    RaycastHit rayhit;
    [Header("feedback stuff")]
    public Material eyeMat;
    public float maxTimeSeen;
    public float timeSeen;

    private int deathCount;
    public Color eyesWhenNotMadieWadie = Color.white, eyesWhenMaddieWaddie = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        timeSeen = maxTimeSeen;
        ignoreMask = ~ignoreMask;
#if UNITY_EDITOR
        if (!player) { Debug.LogWarning("You forgotty watty to swet the pwayer. Why ywu so dwumb? (￣ω￣;)"); }
#endif
    }

    private void FixedUpdate()
    {
        // Does the ray intersect any objects excluding the player layer
        if(player)
            if (Physics.Raycast(transform.position, player.transform.position - this.transform.position, out rayhit, float.MaxValue))
            {
                if (rayhit.collider.gameObject == player.gameObject)
                {
                    timeSeen -= Time.fixedDeltaTime;
                    timeSeen = Mathf.Clamp(timeSeen, 0, maxTimeSeen);
                    if(timeSeen == 0)
                    {
                        deathCount++;
                        DeathByBwosy("Bwosy went uwu", deathCount);
                        //fuck the player up fam
                        player.GameOverScreen.SetActive(true);
                    }
                }
                else
                {
                    timeSeen += Time.fixedDeltaTime;
                    timeSeen = Mathf.Clamp(timeSeen, 0, maxTimeSeen);
                }

            }
        var timePercento = timeSeen / maxTimeSeen;
        eyeMat.color = Color.Lerp(eyesWhenMaddieWaddie, eyesWhenNotMadieWadie, timePercento);
    }

    private void OnDrawGizmosSelected()
    {
        if(rayhit.collider)
            if (rayhit.collider.gameObject == player.gameObject)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, rayhit.point);

               // Debug.Log("Hwit this nwot so swady wady pwayer 	┬┴┬┴┤(･_├┬┴┬┴");
            }
            else
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, rayhit.point);

                Gizmos.color = Color.green;
                Gizmos.DrawLine(rayhit.point, player.transform.position);
               // Debug.Log("Player is Sweady Whady 	┬┴┬┴┤     ├┬┴┬┴");
            }
    }

    public void DeathByBwosy(string name, int amountOfDeaths)
    {
        AnalyticsEvent.Custom(name, new Dictionary<string, object>
        {
            { "Amounts of deaths", amountOfDeaths }
        });
    }
}
