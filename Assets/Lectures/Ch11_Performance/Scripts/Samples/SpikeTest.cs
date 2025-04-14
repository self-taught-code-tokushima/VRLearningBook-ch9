using UnityEngine;

namespace Lectures.Ch11.Sample
{
    public class SpikeTest : MonoBehaviour
    {
        void Update()
        {
            for (int i = 0; i < 100; i++)
            {
                string str = "SpikeTest: " + i;
                Debug.Log(str);
            }
        }
    }
}