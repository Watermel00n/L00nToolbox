using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT_Tools
{
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    public class LT_KerbalSensor : PartModule
    {
        public ModuleGenerator converter;
        public int kerbalmodulecount;
        public override void OnAwake()
        {
            if (part != null)
            {
                converter = part.FindModuleImplementing<ModuleGenerator>();
            }
        }
        public void FixedUpdate()
        {
            
            if (!HighLogic.LoadedSceneIsFlight)
            {
                return;
            }
            else
            {
                int kerbalmodulecount = vessel.FindPartModulesImplementing<LT_KerbalSensor>().Count();
                int crewcount = vessel.GetCrewCount();

                if (crewcount < kerbalmodulecount)
                {
                    if (converter.generatorIsActive == true)
                    {
                        converter.Shutdown();
                        ScreenMessages.PostScreenMessage("Kerbal Count " + crewcount + "Modules Count " + kerbalmodulecount, 5.0f);
                        ScreenMessages.PostScreenMessage("Part requires Kerbal to be inside Vessel in order to function!", 5.0f);
                    }
                }
            }
        }
    }
}
