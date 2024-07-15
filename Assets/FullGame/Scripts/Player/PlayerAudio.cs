using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip movementSound;
    private AudioSource audioSource;

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (player.IsMoving && !audioSource.isPlaying)
        {
            audioSource.clip = movementSound;
            audioSource.Play();
        }
        else if (!player.IsMoving && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
