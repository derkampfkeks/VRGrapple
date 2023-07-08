using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    //public InputActionProperty fireButton;

    [Header("Bullet Info")]
    public GameObject bulletGameObj;
    public float bulletSpeed;
    Transform bulletTransform;
    Rigidbody bulletRb;
    Bullet BulletScript;

    [Header("Player Info")]
    public GameObject playerGameObj;
    Transform playerTransform;
    SpringJoint springJoint;

    [Header("Gun Info")]

    public Transform barrel;
    public bool Shooted { get; set; }


    [Header("Grapple Strength")]
    float springStrength = 1000f;
    float damperStrength = 100f;


    // Start is called before the first frame update
    void Start()
    {
        bulletTransform = bulletGameObj.transform;
        bulletRb = bulletGameObj.GetComponent<Rigidbody>();
        BulletScript = bulletGameObj.GetComponent<Bullet>();

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
        BulletScript.DestroyJoint();
    }

    public void Swing()
    {
        springJoint = playerGameObj.AddComponent<SpringJoint>();
        springJoint.connectedBody = BulletScript.collisionObject.GetComponent<Rigidbody>();
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.connectedAnchor = BulletScript.collisionObject.transform.InverseTransformPoint(BulletScript.hitPoint);
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
        //if (fireButton.action.IsPressed())
        //{
        //    fire();


        //}
    }
}
