using UnityEditor;
using UnityEngine;

namespace ModuleBallistics
{
    [CustomEditor(typeof(ProjectilesPool))]
    public class ProjectilePoolEditor : Editor
    {
        private ProjectilesPool projectilePool;

        private void OnEnable()
        {
            projectilePool = target as ProjectilesPool;
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