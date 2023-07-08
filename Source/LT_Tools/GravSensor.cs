using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace LT_Tools
{
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    public class LT_GravSensor : PartModule
    {
        public ModuleGenerator converter;
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
            
            else if (vessel.Landed == false && converter != null)
            {
                if (converter.generatorIsActive == true)
                { 
                    converter.Shutdown();
                    ScreenMessages.PostScreenMessage("Vessel must be landed for converter to start! This converter will no longer work!", 5.0f);
                }
            }
        }
    }
}
