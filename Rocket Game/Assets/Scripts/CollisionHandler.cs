using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay=1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip Landing;
     [SerializeField] ParticleSystem crashpartcles;
    [SerializeField] ParticleSystem Landingparticles;


    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisable=false;



    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        RespondToDebugKeys();
    }

  void  OnCollisionEnter(Collision col)
    {
        if (isTransitioning||collisionDisable) { return; }
       
            switch (col.gameObject.tag)
            {
                case "LaunchPad":
                    Debug.Log("LaunchPad");
                    break;
                case "LandingPad":
                    StartSuccessSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;

            }
        
    }
    void StartSuccessSequence()
    {
        isTransitioning = true;
        
        audioSource.PlayOneShot(Landing);
       Landingparticles.Play();
        GetComponent<RocketMove>().enabled = false;
        Invoke("LoadNextLevel", delay);
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        
        audioSource.PlayOneShot(crash);
        crashpartcles.Play();
        GetComponent<RocketMove>().enabled = false;
      
       Invoke("ReloadLevel",delay);
    }
    
   void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex==SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        
   }
    void ReloadLevel()
    {
       int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }
    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
          LoadNextLevel();
        }
       else if(Input.GetKeyDown(KeyCode.C))
        {
          collisionDisable=!collisionDisable;//toggle collision
        }
    }
    
}
