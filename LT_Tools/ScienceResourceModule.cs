using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT_Tools
{
    public class LT_ScienceResourceModule : PartModule
    {
        [KSPField]
        public double efficiency = 1;
        [KSPField]
        public string resourceName = null;
        [KSPEvent(guiName = "Transmit ScienceResource", guiActive = true, externalToEVAOnly = false, guiActiveEditor = false, active = true, guiActiveUnfocused = true, unfocusedRange = 5.0f)]
        public void TransmitResource()
        {
            if (part != null && part.Resources.Contains(resourceName)) 
            {
                if (ResearchAndDevelopment.Instance != null)
                    ResearchAndDevelopment.Instance.AddScience(((float)part.Resources[resourceName].amount), TransactionReasons.None);
                ScreenMessages.PostScreenMessage(part.Resources[resourceName].amount + "of ScienceResource was transmitted successfully!");
                part.Resources[resourceName].amount = 0;
            }
            else
                ScreenMessages.PostScreenMessage("Part doesn't have any ScienceResource");
        }
    }
}
