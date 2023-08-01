using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT_Tools
{
    public class LT_ConverterLoadChange : PartModule
    {
        public List<ModuleResourceConverter> converterList;
        [KSPField(guiName = "ConverterLoad", isPersistant = true, guiActive = true, guiActiveEditor = false), UI_FloatRange(stepIncrement = 0.025f, maxValue = 1f, minValue = 0f)]
        public float generatorLoad = 0f;
        public override void OnAwake()
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

            else
            {
                foreach (ModuleResourceConverter converter in converterList)
                {
                    if (converter.IsActivated == true)
                    {
                        converter.SetEfficiencyBonus(generatorLoad);
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
