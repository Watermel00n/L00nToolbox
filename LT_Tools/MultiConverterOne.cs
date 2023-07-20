using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT_Tools
{
    public class LT_MultiConverter : PartModule
    {
        public List<ModuleResourceConverter> converterList;
        public string usedConverters = "none";
        public void Start()
        {
            if (part != null)
            {
                if (part.FindModulesImplementing<ModuleResourceConverter>() != null)
                {
                    converterList = part.FindModulesImplementing<ModuleResourceConverter>();
                    foreach (ModuleResourceConverter converter in converterList) 
                    {
                        if (converter.IsActivated) { converter.StopResourceConverter(); }
                    }
                }
            }
        }
        public void FixedUpdate()
        {
            if (!HighLogic.LoadedSceneIsFlight)
            {
                return;
            }

            else if (converterList != null)
            {
                foreach (ModuleResourceConverter converter in converterList)
                {
                    if (converter.IsActivated)
                    {
                        if (usedConverters == "none")
                        {
                            usedConverters = converter.ConverterName;
                        }
                        else if (usedConverters != converter.ConverterName)
                        {
                            converter.StopResourceConverter();
                            ScreenMessages.PostScreenMessage("Only 1 converter can be used at one time", 5.0f);
                        }
                    }
                    else if (!converter.IsActivated)
                    {
                        if (usedConverters == converter.ConverterName)
                        {
                            usedConverters = "none";
                        }
                    }
                }
            }
        }
        public override string GetInfo()
        {
            var outputstring = string.Empty;
            outputstring = "Only one converter is active at time";
            return outputstring;
        }
    }
}
