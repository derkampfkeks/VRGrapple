using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Gun gun;
    FixedJoint fixedJoint;

    [HideInInspector]
    public GameObject collisionObject;
    public Vector3 hitPoint;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" && gun.Shooted)
        {
            hitPoint = collision.contacts[0].point;
            collisionObject = collision.gameObject;
            fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = collisionObject.GetComponent<Rigidbody>();

            gun.Swing();
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyJoint()
    {
        Destroy(fixedJoint);
    }
}
