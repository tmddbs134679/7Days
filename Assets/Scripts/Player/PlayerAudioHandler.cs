using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{
    AudioManager audioManager;

    private const string FOOTSTEP_CLIP = "PlayerStep";
    public void Init()
    {
        audioManager = AudioManager.Instance;
    }

    public void PlayStepSound()
    {
        int num = Random.Range(1, 4);
        string clipName = FOOTSTEP_CLIP + num.ToString();
        audioManager.PlaySFX(clipName);
    }
}
