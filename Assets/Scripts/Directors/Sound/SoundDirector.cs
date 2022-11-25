using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDirector : MonoBehaviour
{
    public AudioSource typing;
    public AudioSource text_displayed;
    public AudioSource ambient;
    int i = 0;

    public void PlayTyping(bool start)
    {
        if (start)
        {
            typing.loop = true;
            typing.Play();
            Debug.Log("Currently Playing: Typing" + i);
            i++;
        }
        else
        {
            typing.Stop();
        }
    }

    public void StopTyping()
    {
        typing.loop = false;
        typing.Stop();
    }

    public void PlayText_Displayed()
    {
        text_displayed.Play();
    }

}


