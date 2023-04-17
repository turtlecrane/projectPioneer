using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource musicsource;
    public AudioSource effectsource;
    public AudioSource attacksource;
    public AudioSource damagesource;
    public AudioSource dashsource;
    public AudioSource enemyAttack;

    public void SetMusicVolume(float volume)
    {
        musicsource.volume = volume;
    }

    public void SetEffectVolume(float volume)
    {
        effectsource.volume = volume;
    }

    public void SetAttackVolume(float volume)
    {
        attacksource.volume = volume;
    }
    public void SetDamageVolume(float volume)
    {
        damagesource.volume = volume;
    }
    public void SetDashVolume(float volume)
    {
        dashsource.volume = volume;
    }
    public void SetEnemyAttackVolume(float volume)
    {
        enemyAttack.volume = volume;
    }


    public void OnSfx()
    {
        effectsource.Play();
    }
    public void AttackAuidio()
    {
        attacksource.Play();
    }
    public void DamageAuidio()
    {
        damagesource.Play();
    }
    public void DashAuidio()
    {
        dashsource.Play();
    }
    public void EnemyAttackAuidio()
    {
        enemyAttack.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
