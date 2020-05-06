using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XylophoneHero.SongSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class CassettePlayerManeger : MonoBehaviour
    {

        #region PUBLIC_MEMBER_VARIABLES

        public KeyCode TestKeyCode;
        public Vector3 ShootForce;

        #endregion // PUBLIC_MEMBER_VARIABLES

        #region PRIVATE_MEMBER_VARIABLES

        private GameObject m_Cassette;

        #endregion // PRIVATE_MEMBER_VARIABLES

        #region MONOBEHAVIOUR_FUNCTIONS

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.gameObject.tag == TagsConstants.CASSETTE)
            {
                m_Cassette = collision.transform.gameObject;
            }

            SongManager.Instance.StartSong(m_Cassette.GetComponent<CassetteInfo>().GetSong());


        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.transform.gameObject.tag == TagsConstants.CASSETTE && m_Cassette != null)
            {
                m_Cassette = null;
                SongManager.Instance.StopSong();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(TestKeyCode))
            {
                EjectCasstte();
            }
        }

        #endregion // MONOBEHAVIOUR_FUNCTIONS

        #region PUBLIC_MEMBER_FUNCTIONS

        public void EjectCasstte()
        {
            if (m_Cassette != null)
            {
                m_Cassette.GetComponent<Rigidbody>().AddForce(ShootForce);
            }
        }

        #endregion


    }

}
