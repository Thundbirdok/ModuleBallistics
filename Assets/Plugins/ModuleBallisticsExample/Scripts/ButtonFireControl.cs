using UnityEngine;
using UnityEngine.EventSystems;

namespace ModuleBallistics
{
    /// <summary>
    /// Button fire control
    /// </summary>
    public class ButtonFireControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private AbstractGun[] Guns = default;

        /// <summary>
        /// Start fire
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            foreach (AbstractGun gun in Guns)
            {
                gun.StartFire();
            }
        }

        /// <summary>
        /// Stop fire
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            foreach (AbstractGun gun in Guns)
            {
                gun.StopFire();
            }
        }
    }
}
