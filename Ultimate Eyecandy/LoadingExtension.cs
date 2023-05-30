using ColossalFramework.UI;
using ICities;
using System;

namespace EyeCandyX
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);
            try
            {
                // Create backup:
                EyeCandyXTool.SaveBackup();
            }
            catch (Exception e)
            {
                DebugUtils.LogException(e);
            }
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            // Check if in-game or in Asset Editor:
            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame && mode != LoadMode.LoadAsset && mode != LoadMode.NewAsset)
            {
                return;
            }

            try
            {
                bool isUltimateEyecandyInstalled = CompatibilityHelper.IsUltimateEyecandyInstalled();
                bool isEyecandyXInstalled = CompatibilityHelper.IsEyecandyXInstalled();

                if (isUltimateEyecandyInstalled && isEyecandyXInstalled)
                {
                    
                    PerformActionIfBothEyecandysInstalled();
                }

                // Continue with the remaining code
                EyeCandyXTool.Initialize(mode);
                EyeCandyXTool.SaveInitialValues();
                EyeCandyXTool.LoadConfig();
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                ShowExceptionPanel(ex);
            }
        }

        private void PerformActionIfBothEyecandysInstalled()
        {
            ExceptionPanel panel = UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel");
            panel.SetMessage("Compatibility Error", "Eyecandy X has found incompatibilities with the mod Ultimate Eyecandy. Unsubscribe one of the two mods. Ignoring this message will lead to instability.", true);
        }

        private void ShowExceptionPanel(Exception ex)
        {
            UnityEngine.Debug.Log(ex);

        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            //  
            EyeCandyXTool.isGameLoaded = false;
            EyeCandyXTool.Reset();
        }
    }
}
