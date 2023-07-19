using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace LT_Tools
{
    public class LT_GravSensorConverter : PartModule
    {
        public List<ModuleResourceConverter> converterList;
        public void Start()
        {
            if (part != null)
            {
                if (part.FindModulesImplementing<ModuleResourceConverter>() != null)
                    converterList = part.FindModulesImplementing<ModuleResourceConverter>();
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
                foreach (ModuleResourceConverter converter in converterList)
                {
                    if (converter.IsActivated)
                    {
                        converter.StopResourceConverter();
                        ScreenMessages.PostScreenMessage("Vessel must be landed for converter to start! This converter will no longer work!", 5.0f);
                    }
                }
            }
        }
        public override string GetInfo()
        {
            var outputstring = string.Empty;
            outputstring = "Converters can only work if vessel is landed";
            return outputstring;
        }
    }
}
