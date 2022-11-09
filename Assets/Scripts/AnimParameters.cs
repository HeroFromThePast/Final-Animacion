using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimParameters : MonoBehaviour
{
    public float velocidad = 0.0f;
    //public float aceleracion = 0.1f;
    //public float desaceleracion = 0.5f;
    public float cooldown = 0;
    bool timeRunning = false;
    [SerializeField] CharacterMovement characterMovement;

    [SerializeField] Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
    }
    
   
    // Update is called once per frame
    void Update()
    {
       

        if ((Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")) && velocidad < 0.5f)
        {
            velocidad += Time.deltaTime *2f ;
            characterMovement.movementSpeed = 2;
        }
        if(Input.GetKey(KeyCode.LeftShift) && velocidad < 1.0f && velocidad !=0)
        {
            velocidad += Time.deltaTime *2f;
            characterMovement.movementSpeed = 5;
        
        }
        if (!Input.GetKey(KeyCode.LeftShift) && (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")) &&  velocidad > 0.5f)
        {
            velocidad -= Time.deltaTime * 2f;
            characterMovement.movementSpeed = 2;
        }
        if ((!Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("a")) && !Input.GetKey("d") && !Input.GetKey(KeyCode.LeftShift) && velocidad > 0.0f)
        {
            velocidad -= Time.deltaTime*3f;
        }
        
        if ((!Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d"))&& !Input.GetKey(KeyCode.LeftShift) && velocidad < 0.0f)
        {
            velocidad = 0.0f;
        }
        if ((Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")) && Input.GetKey(KeyCode.LeftShift) && velocidad > 1f)
        {
            velocidad = 1f;
        }
        if (Input.GetKey("space"))
        {
            anim.SetBool("Jump", true);
        }
        if (!Input.GetKey("space"))
        {
            anim.SetBool("Jump", false);
        }
        if (Input.GetKey("c"))
        {
            anim.SetBool("Roll", true);
            characterMovement.movementSpeed = 5;
        }
        if (!Input.GetKey("c"))
        {
            anim.SetBool("Roll", false);
            characterMovement.movementSpeed = 2;
        }
        anim.SetFloat("velocity", velocidad);

        if (Input.GetKeyDown("l"))
        {
            anim.SetTrigger("Damage");
        }
        if (Input.GetKeyDown("p"))
        {
            anim.SetTrigger("Dead");
        }

        if (timeRunning)
        {
            cooldown += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldown < 0.8f)
        {
            timeRunning = true;
            cooldown = 0;
            if (anim.GetInteger("Attack") == 0 ) 
            {         
                anim.SetInteger("Attack", 1);                         
            } 
            else if (anim.GetInteger("Attack") == 1)
            {    
                anim.SetInteger("Attack", 2);
            }
            else if (anim.GetInteger("Attack") == 2)
            {         
                anim.SetInteger("Attack", 3);
            }
        }
        else if (cooldown >= 0.8f)
        {
            anim.SetInteger("Attack", 0);
            timeRunning = false;
            cooldown = 0;
        }
       

    }
}
