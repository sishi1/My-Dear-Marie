using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeableObject : AbstractObject
{
    // Declare and initialise varibales and constants

    // Change this based on how long you want it to last/take before it's full
    private const int MAXCHARGE = 50;

    ChannelAbility theChannelAbility;
    Material chargeMaterial;
    private float currentCharge = 0;
    private float drainSpeed = 1, chargeSpeed = 0.25f;
    private float rangeDistance = 1.75f;
    private float timer;
    private bool isDraining = false;
    private bool hasCollision = false;
    public int secondsTillDrain;
    public KeyCode chargeKey;
    private Color emptyColor = Color.white;
    public Behaviour inChargeRangeGlow;
    ParticleSystem chargeParticles;
    AudioSource chargeBeamSound;
    public GameObject chargeDestinationPoint;

    // Properties

    // A condition which may very depending on the type of object
    public bool SpecialChargeCondition { get; set; }

    // Checks if disctance from Marie to object is in range
    protected bool IsInRange { 
        get 
        { 
            return Mathf.Abs(theChannelAbility.gameObject.transform.position.x - 
                gameObject.transform.position.x) <= rangeDistance; 
        } 
    } 

    public bool IsFullyCharged { get { return currentCharge >= MAXCHARGE; } }

    // Object can only be charges when in range but does not collide with player, player has to stand still
    protected bool IsChargeable { get
        { return !theChannelAbility.IsEmpty 
                & !IsFullyCharged & !hasCollision && SpecialChargeCondition
                && theChannelAbility.gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero; } }

    public bool IsUsed { get; set; }

    private void Awake()
    {
        timer = secondsTillDrain;
        SpecialChargeCondition = true;
        theChannelAbility = FindObjectOfType<ChannelAbility>();
        chargeMaterial = GetComponentInChildren<Renderer>().material;
        chargeMaterial.color = emptyColor;
        chargeParticles = theChannelAbility.gameObject.GetComponentInChildren<ParticleSystem>();
        chargeBeamSound = theChannelAbility.gameObject.GetComponentInChildren<AudioSource>();
    }

    void FixedUpdate()
    {
        // Testing script with use of range and key Q (May change because this is also Sishi's part.)

        if (Input.GetKey(chargeKey) && IsChargeable && IsInRange & !DialogueTrigger.activated)
        {
            //Activates particles and sound if it's not playing yet
            if (!chargeParticles.isPlaying)
            {
                chargeParticles.Play();
                chargeBeamSound.Play();
            }

            theChannelAbility.CurrentCharge -= chargeSpeed;
            currentCharge += chargeSpeed;
        }
        // Dactivates particles and sound when you are not charging anymore
        else if ((!Input.GetKey(chargeKey) || !IsChargeable) && IsInRange)
        {
            chargeParticles.Stop();
            chargeBeamSound.Stop();
        }

        if (IsFullyCharged)
        {
            // Countdown timer
            timer -= Time.deltaTime;
            if (timer <= 0 || IsUsed)
            {
                timer = secondsTillDrain;
                isDraining = true;
                IsUsed = false;
            }
        }

        if (isDraining) Discharge();

        // Chaning collor based on charge
        chargeMaterial.color = new Color(chargeMaterial.color.r,
            chargeMaterial.color.g, 1f - currentCharge * (1f / MAXCHARGE));

        // Gives slight glow to the object when in charge range
        if (IsInRange &! hasCollision) inChargeRangeGlow.enabled = true;
        else inChargeRangeGlow.enabled = false;
    }

    private void LateUpdate()
    {
        AttractParticles();
    }

    private void AttractParticles()
    {
        // Copy charge partcile system in a new particle system so it can be edited
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[chargeParticles.particleCount];
        int particleCount = chargeParticles.GetParticles(particles);

        // Changes velocity of all particles to direct towards te object in range
        if (IsInRange)
        {
            for (int iParticle = 0; iParticle < particleCount; iParticle++)
            {
                Vector3 direction = chargeDestinationPoint.transform.position - chargeParticles.transform.position;
                particles[iParticle].velocity = direction;
            }
        }
        // Replace the old partciles with the updated particles
        chargeParticles.SetParticles(particles, particleCount);
    }

    private void Discharge()
    {
        if (currentCharge <= 1) isDraining = false;
        currentCharge -= drainSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        hasCollision = true;
    }

    private void OnTriggerExit(Collider other)
    {
        hasCollision = false;
    }

    public override void OnDarkModeEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnLightModeEnter()
    {
        throw new System.NotImplementedException();
    }
}
