using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileModifierEntry
{
    public IProjectileModifier modifier;
    public float lifetime;

    public ProjectileModifierEntry(IProjectileModifier modifier, float lifetime)
    {
        this.modifier = modifier;
        this.lifetime = lifetime;
    }
}
