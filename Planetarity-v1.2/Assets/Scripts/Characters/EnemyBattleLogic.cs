namespace Planetarity
{
    using System;
    using Input;
    using UnityEditor.UIElements;
    using UnityEngine;

    public class EnemyBattleLogic : MonoBehaviour
    {
        [HideInInspector] public Transform enemy; // Enemy of this character

        private void Awake()
        {
        }

        private void Update()
        {
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            
            if (enemy != null)
                Gizmos.DrawLine(transform.position, enemy.transform.position);
        }
    }
}