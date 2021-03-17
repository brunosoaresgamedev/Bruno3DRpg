using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Player")]
    public float movementSpeed = 3f;

    private Vector3 direction;

    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude > 0.1f)
        {
            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetangle, 0);
        }

        controller.Move(direction * movementSpeed * Time.deltaTime);
    }
}
