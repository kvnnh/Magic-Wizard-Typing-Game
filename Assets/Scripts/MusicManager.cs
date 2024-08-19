using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public Player player;
    public AudioSource battleMusic;
    public AudioSource castSpellSFX;
    public AudioSource gameOverSFX;
    public AudioSource playerFallSFX;
    public AudioSource knockedSFX;

    // Start is called before the first frame update
    void Start()
    {

        if(player.isDead == false)
        {
            battleMusic.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isDead == true && !playerFallSFX.isPlaying && !gameOverSFX.isPlaying)
        {
            battleMusic.Stop();
            playerFallSFX.Play();
            gameOverSFX.Play();
        }

        if (player.isCastSpell && !castSpellSFX.isPlaying && player.isDead == false)
        {
            castSpellSFX.Play();
        }

        if (player.isKnocked == true && !knockedSFX.isPlaying)
        {
            knockedSFX.Play();
        }

    }
}
