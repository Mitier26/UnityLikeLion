using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    public AudioSource sfxSource;
    
    public AudioClip[] sfxClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySfx(SfxTypes sfxType)
    {
        sfxSource.PlayOneShot(sfxClips[(int)sfxType]);
    }

    public void PlaySfxBlock(BlockTypes blockType)
    {
        SfxTypes sfxType;
        
        switch (blockType)
        {
            case BlockTypes.Glass:
                sfxType = SfxTypes.GlassImpact;
                break;

            case BlockTypes.Wood:
                sfxType = SfxTypes.WoodImpact;
                break;

            case BlockTypes.Metal:
                sfxType = SfxTypes.MetalImpact;
                break;

            default:
                return;
        }
        PlaySfx(sfxType);
    }
}
