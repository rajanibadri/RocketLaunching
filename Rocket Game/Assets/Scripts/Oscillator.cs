using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{  Vector3 startPos;
   [SerializeField] Vector3 movementVector;
  float movementFactor;
   [SerializeField] float period=2f;
    // Start is called before the first frame update
    void Start()
    {
        startPos=transform.position;
    }

    // Update is called once per frame
    void Update()
    {   if(period<=Mathf.Epsilon){return;}
         float cycles=Time.time/period;//continually growning over time
         const float tau=Mathf.PI * 2;//constant value 2PI(2*22/7)=6.283
         float rawSineWave=Mathf.Sin(cycles*tau);//going from -1 to 1
         movementFactor=(rawSineWave+1f)/2f;//recalculate to go from 0 to 1
        Vector3 offset=movementVector*movementFactor;
        transform.position=startPos+offset;
    }
}
