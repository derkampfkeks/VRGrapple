using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunNoInteractable : MonoBehaviour
{
    public InputActionProperty fireButton;

    [Header("Bullet Info")]
    public GameObject bulletGameObj;
    public float bulletSpeed;
    Transform bulletTransform;
    Rigidbody bulletRb;
    BulletNoInteractable bulletScript;

    [Header("Player Info")]
    public GameObject playerGameObj;
    Transform playerTransform;
    SpringJoint springJoint;

    [Header("Gun Info")]

    public Transform barrel;
    public bool Shooted { get; set; }


    [Header("Grapple Strength")]
    float springStrength = 400f;
    float damperStrength = 100f;


    // Start is called before the first frame update
    void Start()
    {
        bulletTransform = bulletGameObj.transform;
        bulletRb = bulletGameObj.GetComponent<Rigidbody>();
        bulletScript = bulletGameObj.GetComponent<BulletNoInteractable>();

        playerTransform = playerGameObj.transform;
    }

    public void fire()
    {
        Shooted = true;
        bulletTransform.position = barrel.position;
        bulletRb.velocity = barrel.forward * bulletSpeed;
    }


    public void CancelFire()
    {
        Shooted = false;
        Destroy(springJoint);
        bulletScript.DestroyJoint();
    }

    public void Swing()
    {
        springJoint = playerGameObj.AddComponent<SpringJoint>();
        springJoint.connectedBody = bulletScript.collisionObject.GetComponent<Rigidbody>();
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.connectedAnchor = bulletScript.collisionObject.transform.InverseTransformPoint(bulletScript.hitPoint);
        springJoint.anchor = Vector3.zero;

        float disJointToPlayer = Vector3.Distance(playerTransform.position, bulletTransform.position);

        springJoint.maxDistance = disJointToPlayer * 0.6f;
        springJoint.minDistance = disJointToPlayer * 0.1f;

        springJoint.damper = damperStrength;
        springJoint.spring = springStrength;
    }


    //Update is called once per frame
    public void Update()
    {
        if (!Shooted)
        {
            bulletTransform.position = barrel.position;
            bulletTransform.forward = barrel.forward;
        }
        if (fireButton.action.WasPressedThisFrame())
        {
            fire();
        }
        if (fireButton.action.WasReleasedThisFrame())
        {
            CancelFire();
        }



    }
}
