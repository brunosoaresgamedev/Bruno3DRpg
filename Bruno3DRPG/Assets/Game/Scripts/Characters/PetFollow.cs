using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFollow : MonoBehaviour
{

    public GameObject ThePlayer;
    public float TargetDistance;
    public float AllowedDistance = 1000000000;
    public GameObject theNpc;
    public float FollowSpeed;
    public RaycastHit shot;
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(ThePlayer.transform);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot))
        {
            TargetDistance = shot.distance;
            if(TargetDistance >= AllowedDistance)
            {
                FollowSpeed = 0.02f;
              
                transform.position = Vector3.MoveTowards(transform.position, ThePlayer.transform.position, FollowSpeed);

            }
            else
            {
                FollowSpeed = 0;
                
            }
        }
    }
}
