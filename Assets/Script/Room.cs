using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JyanKenPon
{
    public class Room : MonoBehaviour
    {
        public PlayerCard[] playerCards;
        public PlayerData Mydata;
        public GameObject GoBTN;

        private IEnumerator Start()
        {
            yield return null;
            GoBTN.SetActive(Mydata.host);
        }

        public void RecieveData()
        {

        }

        public void Go()
        {

        }

    }
}
