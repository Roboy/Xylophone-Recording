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

        public KeyCode TestKeyCode;
        public Vector3 ShootForce;

        private GameObject m_Cassette;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.gameObject.tag == TagsConstants.CASSETTE)
            {
                m_Cassette = collision.transform.gameObject;
            }

            SongManager.Instance.StartSong(m_Cassette.GetComponent<CassetteInfo>().GetSong());


        }

        private void Update()
        {
            if (Input.GetKeyDown(TestKeyCode) && m_Cassette != null)
            {
                m_Cassette.GetComponent<Rigidbody>().AddForce(ShootForce);
                m_Cassette = null;

                SongManager.Instance.StopSong();
            }
        }
    }

}
