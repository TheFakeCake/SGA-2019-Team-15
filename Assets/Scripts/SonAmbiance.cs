using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonAmbiance : MonoBehaviour
{
    //public AudioClip ambiance1;
    //public AudioClip ambiance2;

    public AudioSource _audioSource1;
    public AudioSource _audioSource2;
    int DangerNumber = 0;
    public float additionerVol= 0;
    

    // Start is called before the first frame update
    void Start()
    {
        //_audioSource = this.GetComponent<AudioSource>();

        //_audioSource.clip = ambiance1;
        //_audioSource.Play();
    }

    private void Update()
    {
        if (DangerNumber > 0)
        {
            _audioSource1.volume = _audioSource1.volume - additionerVol;
            _audioSource2.volume = _audioSource2.volume + additionerVol;
        }
        else
        {
            _audioSource1.volume = _audioSource1.volume + additionerVol;
            _audioSource2.volume = _audioSource2.volume - additionerVol;
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Danger")
        {
            //_audioSource.clip = ambiance2;
            //_audioSource.Play();
            DangerNumber = DangerNumber + 1;
        }
    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Danger")
        { ;
        //_audioSource.clip = ambiance1;
        //_audioSource.Play();
            DangerNumber = DangerNumber - 1;
        }
    }
}
