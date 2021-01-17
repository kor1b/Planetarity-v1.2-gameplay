namespace Planetarity
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class GravityAttractive : MonoBehaviour
    {
        [Min(0)] public float mass = 1;
    }
}