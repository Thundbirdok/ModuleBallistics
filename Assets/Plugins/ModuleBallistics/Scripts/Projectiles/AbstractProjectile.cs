using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Base projectile
    /// </summary>
    public abstract class AbstractProjectile : MonoBehaviour
    {
        private bool isActive = false;

        /// <summary>
        /// Is projectile moving
        /// </summary>
        public bool IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                isActive = value;
                gameObject.SetActive(value);
            }
        }

        /// <summary>
        /// Init projectile
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="direction">Direction</param>
        /// <param name="data">Data</param>
        public virtual void Init(Vector3 position, Quaternion direction, AbstractProjectileData data)
        {
            InitTransform(position, direction);
        }

        protected virtual void InitTransform(Vector3 position, Quaternion direction)
        {
            transform.position = position;
            transform.rotation = direction;
        }
    }
}
