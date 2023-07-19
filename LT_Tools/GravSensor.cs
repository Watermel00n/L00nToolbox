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
        public List<ModuleGenerator> converterList;
        public void Start()
        {
            if (part != null)
            {
                if (part.FindModulesImplementing<ModuleGenerator>() != null)
                    converterList = part.FindModulesImplementing<ModuleGenerator>();
            }
        }
        public void FixedUpdate()
        {
            if (!HighLogic.LoadedSceneIsFlight) 
            {
                return;
            }
            
            else if (vessel.Landed == false && converterList != null)
            {
                foreach (ModuleGenerator converter in converterList)
                {
                    if (converter.generatorIsActive == true)
                    {
                        converter.Shutdown();
                        ScreenMessages.PostScreenMessage("Vessel must be landed for generator to start! This generator will no longer work!", 5.0f);
                    }
                }
            }
        }
        public override string GetInfo()
        {
            var outputstring = string.Empty;
            outputstring = "Generators can only work if vessel is landed";
            return outputstring;
        }
    }
}
