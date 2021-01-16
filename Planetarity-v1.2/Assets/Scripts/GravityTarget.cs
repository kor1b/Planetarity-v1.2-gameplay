namespace Planetarity
{
    using UnityEngine;

    // Todo: possible, rename? Think about it like about property
    [RequireComponent(typeof(Rigidbody))]
    public class GravityTarget : MonoBehaviour
    {
        [Min(0)] public float mass = 1;
    }
}