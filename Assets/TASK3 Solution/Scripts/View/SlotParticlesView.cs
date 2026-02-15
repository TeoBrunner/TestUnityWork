using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotParticlesView : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> particles;

    public void PlayParticles()
    {
        foreach (var particle in particles)
        {
            particle.Play();
        }
    }
    public void UpdateSprite(Sprite sprite)
    {
        foreach (var particle in particles)
        {
            //particle.GetComponent<ParticleSystemRenderer>().material.SetTexture("_MainTex", sprite.texture);
            var tsa = particle.textureSheetAnimation;
            tsa.enabled = true;
            tsa.mode = ParticleSystemAnimationMode.Sprites;
            if (tsa.spriteCount > 0)
                tsa.SetSprite(0, sprite);
            else
                tsa.AddSprite(sprite);
        }
    }
}
