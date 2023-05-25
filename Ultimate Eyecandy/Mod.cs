using CityColors;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using ICities;
using System;
using UnityEngine;
using static ColossalFramework.Plugins.PluginManager;

namespace EyeCandyX
{
    public class Mod : IUserMod
    {
        public const string version = "1.0";

        public string Name
        {
            get { return "Eyecandy X " + version; }
        }

        public string Description
        {
            get { return "Enhance your city's visuals with this beginner-friendly visual customization tool."; }
        }

        // Mod options:
        public void OnSettingsUI(UIHelperBase helper)
        {
            try
            {
                EyeCandyXTool.LoadConfig();

                UIHelperBase group = helper.AddGroup(Name);
                group.AddSpace(10);

                // Keyboard Shortcut:
                UIDropDown keyboardShortcutDropdown = (UIDropDown)group.AddDropdown("Select your preferred keyboard shortcut for toggling the mod panel", new[] { "Shift + U", "Ctrl + U", "Alt + U" }, EyeCandyXTool.config.keyboardShortcut,
                    sel =>
                    {
                        EyeCandyXTool.config.keyboardShortcut = sel;
                        EyeCandyXTool.SaveConfig(false);
                    });
                keyboardShortcutDropdown.tooltip = "Select your preferred keyboard shortcut for toggling the mod panel.";

                group.AddSpace(15);

                // Auto-Load Last Preset:
                UICheckBox loadLastPresetOnStartCheckBox = (UICheckBox)group.AddCheckbox("Load last active preset on start.", EyeCandyXTool.config.loadLastPresetOnStart,
                    sel =>
                    {
                        if (EyeCandyXTool.config.loadLastPresetOnStart != sel)
                        {
                            EyeCandyXTool.config.loadLastPresetOnStart = sel;
                            EyeCandyXTool.SaveConfig(false);
                        }
                    });
                loadLastPresetOnStartCheckBox.tooltip = "Load last active preset on start.";

                group.AddSpace(15);

                // Enable Time of Day:
                bool playItModEnabled = false;

                foreach (PluginInfo mod in PluginManager.instance.GetPluginsInfo())
                {
                    if (mod.publishedFileID.AsUInt64 == 2741726428uL) // Replace with the publishedFileID of the Play It! mod
                    {
                        playItModEnabled = true;
                        break;
                    }
                }

                if (playItModEnabled)
                {
                    EyeCandyXTool.config.enableSimulationControl = false;
                    group.AddCheckbox("Time of Day and simulation speed functionality has been disabled because you have Play It! mod enabled.", false, null);
                }
                else
                {
                    UICheckBox enableSimulationControlCheckBox = (UICheckBox)group.AddCheckbox("Enable simulation control (Time of Day and simulation speed).", EyeCandyXTool.config.enableSimulationControl,
                        sel =>
                        {
                            if (EyeCandyXTool.config.enableSimulationControl != sel)
                            {
                                EyeCandyXTool.config.enableSimulationControl = sel;
                                EyeCandyXTool.SaveConfig(false);
                            }
                        });
                    enableSimulationControlCheckBox.tooltip = "Enable the Time of Day slider functionality.";
                }

                group.AddSpace(15);

                // UI Interface:
                UIDropDown uiColorDropdown = (UIDropDown)group.AddDropdown("Select your preferred color", new[] { "Default", "Pink", "Light Blue", "Emerald Green", "Navy Blue", "Mustard Yellow", "Sky Blue", "Lavender", "Crimson" }, 0, OnColorDropdownSelectionChanged);
                uiColorDropdown.tooltip = "Select your preferred color.";

                void OnColorDropdownSelectionChanged(int index)
                {
                    switch (index)
                    {
                        case 0:
                            ColorData.UIColor = new Color32(255, 192, 203, 255);
                            break;
                        case 1:
                            ColorData.UIColor = new Color32(255, 192, 203, 255);
                            break;
                        case 2:
                            ColorData.UIColor = new Color32(173, 216, 230, 255);
                            break;
                        case 3:
                            ColorData.UIColor = new Color32(0, 201, 87, 255);
                            break;
                        case 4:
                            ColorData.UIColor = new Color32(0, 0, 128, 255);
                            break;
                        case 5:
                            ColorData.UIColor = new Color32(255, 219, 88, 255);
                            break;
                        case 6:
                            ColorData.UIColor = new Color32(135, 206, 235, 255);
                            break;
                        case 7:
                            ColorData.UIColor = new Color32(230, 230, 250, 255);
                            break;
                        case 8:
                            ColorData.UIColor = new Color32(220, 20, 60, 255);
                            break;
                        default:
                            break;
                    }
                }


                group.AddSpace(15);

                // Output Debug Data:
                UICheckBox debugCheckBox = (UICheckBox)group.AddCheckbox("Write data to debug log (it's going to be a lot!)", EyeCandyXTool.config.outputDebug,
                    b =>
                    {
                        if (EyeCandyXTool.config.outputDebug != b)
                        {
                            EyeCandyXTool.config.outputDebug = b;
                            EyeCandyXTool.SaveConfig(false);
                        }
                    });


                debugCheckBox.tooltip = "Output messages to debug log. This may be useful when experiencing issues with this mod.";
                //group.AddSpace(20);

            }
            catch (Exception e)
            {
                DebugUtils.LogException(e);
            }
        }
    }
}
