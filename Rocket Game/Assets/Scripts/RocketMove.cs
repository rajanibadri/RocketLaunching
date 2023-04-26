using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMove : MonoBehaviour
{
   
    [SerializeField] float speed = 10f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThrusterParticle;
    [SerializeField] ParticleSystem leftThrusterParticle;
    [SerializeField] ParticleSystem rightThrusterParticle;

    Rigidbody rb;
    AudioSource audioSource;



    void Awake()
    {
       rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        ProcessRotate(); 
    }
    void ProcessInput()
    {
    if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * speed * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if(!mainThrusterParticle.isPlaying)
            {
            mainThrusterParticle.Play();
            }
        }
        else
        {
            audioSource.Stop();
           // mainThrusterParticle.Stop();
        }
    
     
    }
    void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
           if(!rightThrusterParticle.isPlaying)
           {
               rightThrusterParticle.Play();
           }

        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
            if(!leftThrusterParticle.isPlaying)
           {
               leftThrusterParticle.Play();
           }
        }
        else
        {
           rightThrusterParticle.Stop();
           leftThrusterParticle.Stop();
        }
    }
    void ApplyRotation(float rotationside)
    {
       
        
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotationside * Time.deltaTime);
            rb.freezeRotation = false;
            

    }
}
