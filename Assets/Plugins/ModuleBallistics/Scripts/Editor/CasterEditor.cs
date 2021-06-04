using UnityEditor;
using UnityEngine;

namespace ModuleBallistics
{
    [CustomEditor(typeof(Caster))]
    public class CasterEditor : Editor
    {
        private Caster caster;

        private void OnEnable()
        {
            caster = target as Caster;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Check Projectile Pool"))
            {
                bool result = caster.CheckProjectilePool();

#if UNITY_EDITOR

                Debug.Log("Projectile Pool" + (result ? " already" : "") + " linked");

#endif

            }
        }
    }
}
