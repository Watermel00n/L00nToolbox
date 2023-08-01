using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LT_Tools
{
    public class LT_SetupResources : PartModule
    {
        public List<ModuleResourceConverter> converterList;

        public List<ModuleGenerator> generatorList;

        [KSPField(isPersistant = true)]
        public bool setupOrNot = false;
        [KSPField]
        public string resourceName = null;
        [KSPField]
        public double resourceAmount = 0;
        [KSPEvent(guiName = "Setup Module", guiActive = true, externalToEVAOnly = false, guiActiveEditor = false, active = true, guiActiveUnfocused = true, unfocusedRange = 5.0f)]
        public void setupModule()
        {
            if (setupOrNot == false)
            {
                if (part.Resources.Count > 0)
                {
                    if (part.Resources.Contains(resourceName))
                    {
                        var partResourceAmount = part.Resources[resourceName].amount;
                        if (partResourceAmount >= resourceAmount)
                        {
                            part.Resources[resourceName].amount -= resourceAmount;
                            setupOrNot = true;
                            ScreenMessages.PostScreenMessage("Part was set up for " + resourceAmount + " of " + resourceName);
                            
                        }
                        else 
                        { 
                            double partResourceAmountDisplay = Math.Round(partResourceAmount,3);
                            ScreenMessages.PostScreenMessage("Part lacks enough of " + resourceName + " as it needs " + resourceAmount + " and part has " + partResourceAmountDisplay); 
                        }
                    }
                    else { ScreenMessages.PostScreenMessage("Part lacks the resource needed," + resourceName); }
                }
                else { ScreenMessages.PostScreenMessage("Part lacks resources! You need " + resourceAmount + "and the amount is " + resourceName); }
            }
            else if (setupOrNot == true) { ScreenMessages.PostScreenMessage("Module is already set up!"); }
        }
        public override void OnAwake()
        {
            if (part != null) 
            {
                setupOrNot = false;
                if (part.FindModulesImplementing<ModuleGenerator>() != null)
                    generatorList = part.FindModulesImplementing<ModuleGenerator>();
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
            if (generatorList != null)
            {
                if (!setupOrNot) 
                { 
                    foreach (ModuleGenerator generator in generatorList)
                    {
                        if (generator.generatorIsActive) 
                        {
                            generator.Shutdown();
                            ScreenMessages.PostScreenMessage("Module needs to be set up!");
                        }
                    }
                }
            }
            if (converterList != null) 
            {
                if (!setupOrNot)
                {
                    foreach (ModuleResourceConverter converter in converterList)
                    {
                        if (converter.IsActivated)
                        {
                            converter.StopResourceConverter();
                            ScreenMessages.PostScreenMessage("Module needs to be set up!");
                        }
                    }
                }
            }
        }
        public override string GetInfo()
        {
            var outputstring = string.Empty;
            outputstring = "Part needs: " + resourceName + " Amount: " + resourceAmount;
            return outputstring;
        }
    }
}
