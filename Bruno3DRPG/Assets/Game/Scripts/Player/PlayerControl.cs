using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Player")]
    public float movementSpeed = 3f;




    private Vector3 direction;
    [SerializeField]
    private bool isAttack;
    private bool isWalk;

    private float horizontal;
    private float vertical;

    private CharacterController controller;
    private Animator anim;

    [Header("Attack config")]
    public ParticleSystem fxAttack;
    public Transform Hitbox;
    [Range(0.2f, 1f)]
    public float hitRange = 0.5f;
    public LayerMask hitMask;


    [SerializeField]
     Collider[] hitInfo;
    public int amountDmg;
    // Start is called before the first frame update
    void Start()
    {
      
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();

        MoveCharacter();

        UpdateAnimator();

        
    }

    #region MEUS METODOS
        void Inputs()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Fire1") && isAttack == false)
        {
            Attack();
        }

       
    }

    void MoveCharacter()
    {
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetangle, 0);
            isWalk = true;
        }
        else //if(direction.magnitude > 0.1f)
        {
            isWalk = false;
        }

        controller.Move(direction * movementSpeed * Time.deltaTime);
    }


    void UpdateAnimator()
    {
        anim.SetBool("isWalk", isWalk);
    }


    void Attack()
    {
       
            isAttack = true;
            anim.SetTrigger("Attack");
            fxAttack.Emit(1);

       hitInfo = Physics.OverlapSphere(Hitbox.position, hitRange,hitMask);

        foreach(Collider c in hitInfo)
        {
            c.gameObject.SendMessage("GetHit", amountDmg, SendMessageOptions.DontRequireReceiver);
        }
    }

    void AttackEnd()
    {
        isAttack = false;
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        if(Hitbox != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Hitbox.position, hitRange);
        }
        
    }
}
