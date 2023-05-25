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
            //  
            EyeCandyXTool.Initialize(mode);
            EyeCandyXTool.SaveInitialValues();
            EyeCandyXTool.LoadConfig();
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
