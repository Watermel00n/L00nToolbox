﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace LT_Tools
{
    public class LT_VesselAnchor : PartModule
    {
        [KSPField]
        public int topSpeed = 5;
        [KSPField(isPersistant = true)]
        public bool anchorState = false;
        [KSPAction(guiName = "Disable Vessel Anchor")]
        public void disableVesselAnchor(KSPActionParam param)
        {
            if (anchorState == true)
            {
                anchorState = false;
                ScreenMessages.PostScreenMessage("Vessel Anchor Disabled!");
                syncUpAnchors();
            }
        }

        [KSPAction(guiName = "Enable Vessel Anchor")]
        public void enableVesselAnchor(KSPActionParam param)
        {
            if (anchorState == false)
            {
                anchorState = true;
                syncUpAnchors();
            }    
        }

        [KSPAction(guiName = "Toggle Vessel Anchor")]
        public void toggleVesselAnchor(KSPActionParam param)
        {
            if (anchorState == true)
            {
                anchorState = false;
                ScreenMessages.PostScreenMessage("Vessel Anchor Disabled!");
                syncUpAnchors();
            } 
            else if (anchorState == false)
            {
                anchorState = true;
                syncUpAnchors();
            }   
        }

        [KSPEvent(guiName = "Vessel Anchor",guiActive = true,externalToEVAOnly = false,guiActiveEditor = false,active = true,guiActiveUnfocused = true,unfocusedRange = 5.0f)]
        public void toggleVesselAnchorEvent()
        {
            if (anchorState == true)
            {
                anchorState = false;
                ScreenMessages.PostScreenMessage("Vessel Anchor Disabled!");
                syncUpAnchors();
            }
            else if (anchorState == false)
            {
                anchorState = true;
                syncUpAnchors();
            }
        }

        public override void OnStart(StartState state)
        {
            base.OnStart(state);
            if (!HighLogic.LoadedSceneIsFlight)
                return;
            setAnchorState(anchorState);
            syncUpAnchors();

        }

        public void setAnchorState(bool anchorState)
        {
            this.anchorState = anchorState;
        }

        public void syncUpAnchors()
        {
            var anchors = vessel.FindPartModulesImplementing<LT_VesselAnchor>();
            for (int i = 0; i < anchors.Count; ++i) 
            {
                anchors[i].anchorState = anchorState;
            }
        }

        public void FixedUpdate()
        {
            if (!HighLogic.LoadedSceneIsFlight)
                return;
            if (vessel.Landed == false)
                return;
            if (anchorState == false)
                return;
            if (vessel.Landed == true && anchorState == true && vessel.horizontalSrfSpeed < topSpeed)
            {
                syncUpAnchors();
                ScreenMessages.PostScreenMessage("Vessel Anchor Enabled!");
                for (int i = 0; i < vessel.parts.Count; ++i)
                {
                    var rigids = vessel.parts[i].Rigidbody;
                    if (rigids != null)
                    {
                        rigids.angularVelocity = Vector3.zero;
                        rigids.velocity = Vector3.zero;
                    }
                }
                return;
            }
            else if (vessel.Landed == true && anchorState == true && vessel.horizontalSrfSpeed > topSpeed)
            {
                ScreenMessages.PostScreenMessage("Vessel is moving faster than " + topSpeed + "m/s");
                anchorState = false;
                return;
            }
            else
            {
                ScreenMessages.PostScreenMessage("Vessel needs to be landed!");
                anchorState = false;
                return;
            }
        }
        public override string GetInfo()
        {
            var outputstring = string.Empty;
            outputstring = "Top Speed: " + topSpeed.ToString();
            return outputstring;
        }
    }
}