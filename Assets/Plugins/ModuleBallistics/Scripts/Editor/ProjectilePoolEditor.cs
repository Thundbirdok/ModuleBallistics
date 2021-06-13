using UnityEditor;
using UnityEngine;

namespace ModuleBallistics
{
    [CustomEditor(typeof(ProjectilePool))]
    public class ProjectilePoolEditor : Editor
    {
        private ProjectilePool projectilePool;

        private void OnEnable()
        {
            projectilePool = target as ProjectilePool;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Init Projectile Pools"))
            {
                projectilePool.InitPools();
            }
        }
    }
}