using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT_Tools
{
    public class LT_GeneratorOrConverter : PartModule
    {
        public ModuleGenerator converter;
        public ModuleResourceConverter converter2;
        public List<ModuleResourceConverter> converterList;
        public void Start()
        {
            if (part != null)
            {
                converter = part.FindModuleImplementing<ModuleGenerator>();
                converterList = part.FindModulesImplementing<ModuleResourceConverter>();
            }
        }
        public void FixedUpdate()
        {
            if (!HighLogic.LoadedSceneIsFlight)
            {
                return;
            }

            else if (converter != null && converterList != null)
            {
                foreach (ModuleResourceConverter converter2 in converterList)
                {
                    if (converter2.IsActivated == true)
                    {
                        if (converter.generatorIsActive == true)
                        {
                            converter.Shutdown();
                            ScreenMessages.PostScreenMessage("You must turn off the ResourceConverter for this Generator to work!", 5.0f);
                        }
                    }
                }
                
            }
        }
        public override string GetInfo()
        {
            var outputstring = string.Empty;
            outputstring = "Generator can only work if ResourceConverter is off";
            return outputstring;
        }
    }
}
