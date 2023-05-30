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
            get { return "Eyecandy X"; }
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

                // Auto-Load Last Preset:
                UICheckBox loadLastPresetOnStartCheckBox = (UICheckBox)group.AddCheckbox("Load most recent saved configuration upon start", EyeCandyXTool.config.loadLastPresetOnStart,
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

                // Keyboard Shortcut:
                UIDropDown keyboardShortcutDropdown = (UIDropDown)group.AddDropdown("Choose your desired key combination for activating the mod interface.", new[] { "Shift + U", "Ctrl + U", "Alt + U" }, EyeCandyXTool.config.keyboardShortcut,
                    sel =>
                    {
                        EyeCandyXTool.config.keyboardShortcut = sel;
                        EyeCandyXTool.SaveConfig(false);
                    });
                keyboardShortcutDropdown.tooltip = "Choose your desired key combination for activating the mod interface.";

                group.AddSpace(15);

                // Output Debug Data:
                UICheckBox debugCheckBox = (UICheckBox)group.AddCheckbox("Write data to debug log. Might harm performance.", EyeCandyXTool.config.outputDebug,
                    b =>
                    {
                        if (EyeCandyXTool.config.outputDebug != b)
                        {
                            EyeCandyXTool.config.outputDebug = b;
                            EyeCandyXTool.SaveConfig(false);
                        }
                    });

                // Set the tooltip for the debugCheckBox
                debugCheckBox.tooltip = "Direct messages to the debug log. This can prove beneficial when encountering problems with this mod.";
            }
            catch (Exception e)
            {
                DebugUtils.LogException(e);
            }
        }
    }
}
