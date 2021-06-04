using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Base projectile
    /// </summary>
    public abstract class AbstractProjectile : MonoBehaviour
    {
        private bool isActive;

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
            transform.position = position;
            transform.rotation = direction;
        }

        /// <summary>
        /// Move projectile while it active    
        /// </summary>
        protected abstract void Move();
    }
}
