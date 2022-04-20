using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioClip swap, nextLevel;
    static AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        swap = Resources.Load<AudioClip>("swipSound");
        nextLevel = Resources.Load<AudioClip>("nextLevel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "swip":
                audioSource.PlayOneShot(swap);
                break;
            case "nextLevel":
                audioSource.PlayOneShot(nextLevel);
                break;
        }
    }
}
