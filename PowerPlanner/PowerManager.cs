using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows;

namespace PowerPlanner
{
    public class PowerManager
    {
        private enum AccessFlags : uint
        {
            ACCESS_SCHEME = 16,
            ACCESS_SUBGROUP = 17,
            ACCESS_INDIVIDUAL_SETTING = 18
        }

        public static List<PowerPlan> GetAllPlans()
        {
            List<PowerPlan> allPlans = new List<PowerPlan>();
            var schemeGuid = Guid.Empty;

            uint sizeSchemeGuid = (uint)Marshal.SizeOf(typeof(Guid));
            uint schemeIndex = 0;

            while (PowerEnumerate(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, (uint)AccessFlags.ACCESS_SCHEME, schemeIndex, ref schemeGuid, ref sizeSchemeGuid) == 0)
            {
                allPlans.Add(new PowerPlan(GetPowerPlanName(schemeGuid), schemeGuid));
                schemeIndex++;
            }

            return allPlans;
        }

        public static void SetActive(PowerPlan plan)
        {
            PowerSetActiveScheme(IntPtr.Zero, ref plan.Guid);
        }

        public static Guid GetActiveGuid()
        {
            Guid ActiveScheme = Guid.Empty;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)));
            if (PowerGetActiveScheme((IntPtr)null, out ptr) == 0)
            {
                ActiveScheme = (Guid)Marshal.PtrToStructure(ptr, typeof(Guid));
                if (ptr != null)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }
            return ActiveScheme;
        }

        public static string GetPowerPlanName(Guid guid)
        {
            string name = string.Empty;
            IntPtr lpszName = (IntPtr)null;
            uint dwSize = 0;

            PowerReadFriendlyName((IntPtr)null, ref guid, (IntPtr)null, (IntPtr)null, lpszName, ref dwSize);
            if (dwSize > 0)
            {
                lpszName = Marshal.AllocHGlobal((int)dwSize);
                if (0 == PowerReadFriendlyName((IntPtr)null, ref guid, (IntPtr)null, (IntPtr)null, lpszName, ref dwSize))
                {
                    name = Marshal.PtrToStringUni(lpszName);
                }
                if (lpszName != IntPtr.Zero)
                    Marshal.FreeHGlobal(lpszName);
            }

            return name;
        }

        public static void OpenControlPanel()
        {
            var root = Environment.GetEnvironmentVariable("SystemRoot");
            Process.Start(root + "\\system32\\control.exe", "/name Microsoft.PowerOptions");
        }

        #region DLL imports

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern int GetSystemDefaultLCID();

        [DllImportAttribute("powrprof.dll", EntryPoint = "PowerSetActiveScheme")]
        public static extern uint PowerSetActiveScheme(IntPtr UserPowerKey, ref Guid ActivePolicyGuid);

        [DllImportAttribute("powrprof.dll", EntryPoint = "PowerGetActiveScheme")]
        public static extern uint PowerGetActiveScheme(IntPtr UserPowerKey, out IntPtr ActivePolicyGuid);

        [DllImportAttribute("powrprof.dll", EntryPoint = "PowerReadFriendlyName")]
        public static extern uint PowerReadFriendlyName(IntPtr RootPowerKey, ref Guid SchemeGuid, IntPtr SubGroupOfPowerSettingsGuid, IntPtr PowerSettingGuid, IntPtr Buffer, ref uint BufferSize);

        [DllImport("PowrProf.dll")]
        public static extern UInt32 PowerEnumerate(IntPtr RootPowerKey, IntPtr SchemeGuid, IntPtr SubGroupOfPowerSettingGuid, UInt32 AcessFlags, UInt32 Index, ref Guid Buffer, ref UInt32 BufferSize);

        #endregion
    }
}