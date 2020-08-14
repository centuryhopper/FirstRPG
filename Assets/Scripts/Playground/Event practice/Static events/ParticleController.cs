using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] GameObject impactParticlePrefab = null;

    private void Awake()
    {
        ClickableBoxStaticEvent.onAnyBoxClicked += SpawnParticleOnBox;
    }

    // was Clickable box, but is now ClickableBoxStaticEvent
    public void SpawnParticleOnBox(ClickableBoxStaticEvent box)
    {
        // spawns a particle that gets destroyed 3 seconds later
        var particle =
        Instantiate(impactParticlePrefab, box.transform.position, Quaternion.identity);

        Destroy(particle, 3f);
    }
}
