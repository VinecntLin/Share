using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioPlay : MonoBehaviour
{
    
    AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.impulse.magnitude>10)
        {
            audioData.volume = 1.0f;
            audioData.Play();
        }
        else if(col.impulse.magnitude>3)
        {
            audioData.volume = 0.5f;
            audioData.Play();
        }
        else if(col.impulse.magnitude>1)
        {
            audioData.volume = 0.2f;
            audioData.Play();
        }  
    }
}
