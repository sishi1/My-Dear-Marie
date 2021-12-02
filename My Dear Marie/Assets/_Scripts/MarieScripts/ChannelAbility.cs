using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChannelAbility : AbstractAbility
{
    // Other variables
    MarieController player;
    ToggleWorldMode toggleWorld;

    [Header("Charge bar")]
    [SerializeField] private Image channelBar;
    public Image barBG;

    [Header("Audio")]
    public AudioSource ChannelSound;

    [Header("Particles")]
    public ParticleSystem ChannelParticles;

    // KeyCode variables
    KeyCode chargeKey = KeyCode.E;
    KeyCode switchWorldKey = KeyCode.F; 

    // Variables for charging happy thoughts
    private float maxCharge = 100f;
    private float chargeRate; 
    private float dischargeRate;

    [Header("Charge rate")]
    public float secondsTillFullyCharged;
    public float secondsTillFullyDrained;

    private bool MaxCharged { get { return CurrentCharge >= maxCharge; } }

    public bool IsEmpty { get { return CurrentCharge <= 0; } }
    public float CurrentCharge { get; set; }
    public float Charge { set { maxCharge = value; } get { return maxCharge; } }

    private void Awake()
    {
        player = GetComponent<MarieController>();
        toggleWorld = FindObjectOfType<ToggleWorldMode>();
        chargeRate = maxCharge / secondsTillFullyCharged;
        dischargeRate = maxCharge / secondsTillFullyDrained;
    }

    private void Update()
    {
        ExecuteBehavior();
    }

    // Switch statement to switch between the worlds
    private void ExecuteBehavior()
    {
        switch (toggleWorld.CurrentWorld)
        {
            case WorldStatus.darkworld:
                DarkModeBehavior();
                break;

            case WorldStatus.lightworld:
                LightModeBehavior();
                break;

            default:
                Debug.Log("Neither dark nor lightworld");
                break;
        }
    }

    // The logic you can do when you're in the dark world
    private void DarkModeBehavior()
    {
        // Charge the value until you release the button and player is unable to move
        if (Input.GetKey(chargeKey) &! MaxCharged &! DialogueTrigger.activated)
        {
            if (!ChannelSound.isPlaying) ChannelSound.Play();
            if (!ChannelParticles.isPlaying) ChannelParticles.Play();
            player.moveSpeed = 0;
            CurrentCharge += chargeRate * Time.deltaTime;
        }
        else
        {
            player.moveSpeed = 5;
            ChannelSound.Stop();
            ChannelParticles.Stop();
        }

        // Rounds the value to int instead of float to make the bar accurate
        channelBar.fillAmount = (int)CurrentCharge / maxCharge;

        // Makes it so the value can't exceed the max
        if (MaxCharged) CurrentCharge = maxCharge;

        // Current way of switching to the light world
        if (Input.GetKeyDown(switchWorldKey) && MaxCharged & !DialogueTrigger.activated)
        {
            toggleWorld.SwitchWorld();
        }
    }

    // The logic you can do when you're in the light world
    private void LightModeBehavior()
    {
        CurrentCharge -= dischargeRate * Time.deltaTime;
        channelBar.fillAmount = (int)CurrentCharge / maxCharge;

        // Makes it so the value doesn't exceed 0 and switch worlds
        if (IsEmpty)
        {
            CurrentCharge = 0;
            toggleWorld.SwitchWorld();
        }
    }
}
