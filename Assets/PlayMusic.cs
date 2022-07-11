using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){


    }
    public AudioSource audioSource;
    private static PlayMusic instance = null;
    public static PlayMusic Instance
    {
        get{return instance;}
    }

    void Awake(){

        if (instance !=null && instance != this)
        {
           Destroy(this.gameObject);
           return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Update(){

    }
}
