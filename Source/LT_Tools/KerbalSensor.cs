using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT_Tools
{
    public class LT_KerbalSensor : PartModule
    {
        public ModuleGenerator converter;
        public int kerbalmodulecount;
        public void Start()
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
                        ScreenMessages.PostScreenMessage("Not enough Kerbals! Kerbals: " + crewcount + " KerbalSensor Generators: " + kerbalmodulecount, 5.0f);
                    }
                }
            }
        }
        public override string GetInfo()
        {
            var outputstring = string.Empty;
            outputstring = "Generator can only work if there are at least as many kerbals as KerbalSensor generators";
            return outputstring;
        }
    }
}
