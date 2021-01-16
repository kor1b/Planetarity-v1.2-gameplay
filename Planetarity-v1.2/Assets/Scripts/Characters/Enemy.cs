namespace Planetarity
{
    using System;
    using UnityEngine;

    public class Enemy : Character
    {
        [HideInInspector] public Character enemy; // Enemy of this character
    }
}