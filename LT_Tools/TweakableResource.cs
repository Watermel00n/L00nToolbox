using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT_Tools
{
    public class LT_UnTweakableResource:PartModule
    {
        [KSPField]
        public string resourceName = "ScienceResource";
        public void FixedUpdate()
        {
            if (part.Resources.Contains(resourceName))
            {
                part.Resources[resourceName].isTweakable = false;
            }
            else
                ScreenMessages.PostScreenMessage("Part doesn't have any of the resource specified in the config!");
        }
        public override string GetInfo()
        {
            var outputstring = string.Empty;
            outputstring = "Untweakable Resource:" + resourceName;
            return outputstring;
        }
    }
}
