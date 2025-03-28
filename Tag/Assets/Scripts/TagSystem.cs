using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagSystem : MonoBehaviour
{
    [SerializeField]
    private bool _startTagged = false;

    [SerializeField]
    private float _tagImmunityDuration = 1.0f;

    [SerializeField]
    private GameObject _tagParticlesPrefab;

    private bool _tagged = false;
    private bool _tagImmune = false;
    public bool Tagged { get { return _tagged; } }

    public bool Tag()
    { 
        //If already tagged, do nothing
        if (Tagged) return false;
        // If immune to tag, do nothing
        if (_tagImmune) return false;

        if (TryGetComponent(out TrailRenderer renderer))
            renderer.emitting = true;

        _tagged = true;
        SpawnParticles();
        return true;
    }

    private void SpawnParticles()
    {
        if (!_tagParticlesPrefab)
            return;

        Instantiate(_tagParticlesPrefab, gameObject.transform.position, gameObject.transform.rotation);
    }

    private void SetTagImmuneFalse()
    {
        _tagImmune = false;
    }

    private void Start()
    {
        _tagged = _startTagged;
        if (TryGetComponent(out TrailRenderer renderer))
            renderer.emitting = _startTagged;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If we are not tagged, do nothing
        if (!Tagged) return;

        if (collision.gameObject.TryGetComponent(out TagSystem tagSystem))
        {
            //Tag the other player
            if(tagSystem.Tag())
            { 
                _tagged = false;
                _tagImmune = true;
                if (TryGetComponent(out TrailRenderer renderer))
                    renderer.emitting = false;
                Invoke(nameof(SetTagImmuneFalse), _tagImmunityDuration);
            }

        }
    }
}
