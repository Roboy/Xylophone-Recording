using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XylophoneHero.SongSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class DestroySongNote : MonoBehaviour
    {

        #region MONOBEHAVIOR_METHODS
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "SongNote")
            {
                Destroy(other.gameObject);
            }
        }
        #endregion // MONOBEHAVIOR_METHODS
    }
}
