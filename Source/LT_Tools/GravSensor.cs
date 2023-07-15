using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace LT_Tools
{
    public class LT_GravSensor : PartModule
    {
        public ModuleGenerator converter;
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
            
            else if (vessel.Landed == false && converter != null)
            {
                if (converter.generatorIsActive == true)
                { 
                    converter.Shutdown();
                    ScreenMessages.PostScreenMessage("Vessel must be landed for converter to start! This converter will no longer work!", 5.0f);
                }
            }
        }
        public override string GetInfo()
        {
            var outputstring = string.Empty;
            outputstring = "Generator can only work if vessel is landed";
            return outputstring;
        }
    }
}
