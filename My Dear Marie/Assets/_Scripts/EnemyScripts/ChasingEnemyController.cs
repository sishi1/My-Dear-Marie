using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class ChasingEnemyController : AbstractEnemy
{
    public CapsuleCollider colliderBox;
    public float followingDistance;
    public Material myMaterial;
    public LayerMask playerLayer;
    GameObject target = null;
    Collider[] hitColliders;

    [Range(0, 1)]
    public float opacityInDarkMode;
    [Range(0, 1)]
    public float opacityInLightMode;

    private Rigidbody body;
    private MeshRenderer meshRenderer;

    private AudioSource monsterSound;

    [SerializeField]
    Status status = Status.idle;

    public enum Status
    {
        idle,
        chasing
    }

    //
    public void Awake()
    {
#if UNITY_EDITOR
        if (myMaterial == null)
        {
            Debug.LogError("Yikes, looks like you didn't add a material... Moron.");
        }
#endif
        body = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        monsterSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, followingDistance, playerLayer);
        if (hitColliders.Length >= 1)
            target = checkForClosest(hitColliders).gameObject;
        else target = null;
    }

    //
    private void FixedUpdate()
    {
        switch (status)
        {
            case Status.idle:
                ExecuteIdleBehaviour();
                break;
            case Status.chasing:
                ExecuteChasingBehaviour();
                break;
            default:
                Debug.LogWarningFormat("Enemy encountered an unknown status: ({0}) ", status);
                break;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<MarieController>();
        if (player)
        {
            player.GameOverScreen.SetActive(true);
        }
    }


    //Stand still untill the player gets near enough, then start chasing
    private void ExecuteIdleBehaviour()
    {
        if (target != null)
        {
            status = Status.chasing;
            return;
        }
    }

    //Chase the player untill they are too far away, then start idling
    private void ExecuteChasingBehaviour()
    {
        if (target == null)
        {
            status = Status.idle;
            return;
        }
        body.velocity = (Vector3.Normalize(new Vector3(target.transform.position.x - transform.position.x, 0, 0)) * speed) + new Vector3(0, body.velocity.y, 0);
    }

    //Stop moving and become invisible
    public override void OnLightModeEnter()
    {
        body.isKinematic = true;
        colliderBox.enabled = false;
        //myMaterial.color = new Color(myMaterial.color.r, myMaterial.color.g, myMaterial.color.b, opacityInLightMode);
        status = Status.idle;
        monsterSound.volume = 0.1f;
        enabled = false;
    }

    //Become visible and start looking for the player again
    public override void OnDarkModeEnter()
    {
        body.isKinematic = false;
        colliderBox.enabled = true;
        //myMaterial.color = new Color(myMaterial.color.r, myMaterial.color.g, myMaterial.color.b, opacityInDarkMode);
        monsterSound.volume = 1;
        enabled = true;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, followingDistance);

        if (target != null)
            Gizmos.DrawLine(transform.position, target.transform.position);

    }
#endif

    Collider checkForClosest(Collider[] hitColliders)
    {
        Collider closest = null;
        float distanceToClosest = float.PositiveInfinity;

        foreach (Collider collider in hitColliders)
        {
            float distanceToCollider = (collider.transform.position - transform.position).sqrMagnitude;
            if (distanceToCollider < distanceToClosest)
            {
                closest = collider;
                distanceToClosest = distanceToCollider;
            }
        }

        return closest;
    }
}
