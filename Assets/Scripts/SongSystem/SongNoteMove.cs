using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace XylophoneHero
{
    [RequireComponent(typeof(Rigidbody))]
    public class SongNoteMove : MonoBehaviour
    {

        #region PUBLIC_MEMBER_VARIABLES
        public float MoveVelocity = 3;
        #endregion

        #region PRIVATE_MEMBER_VARIABLES
        private Rigidbody m_Rb;
        #endregion

        #region MONOBEHAVIOR_METHODS
        void Start()
        {
            m_Rb = GetComponent<Rigidbody>();
            if (m_Rb)
            {
                m_Rb.velocity = transform.forward * MoveVelocity;
            }
        }

        void Update()
        {
        }
        #endregion // MONOBEHAVIOR_METHODS
    }
}

