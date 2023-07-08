using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LT_Tools
{
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    public class Hello : MonoBehaviour
    {
        public void Start()
        {
            UnityEngine.Debug.Log("LT_Tools loaded!");
        }
    }
}
