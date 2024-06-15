using System;
using System.Collections.Generic;

namespace EasyGunLauncherLite
{
    public class ProcessMgr
    {
        public static List<int> Processes;

        public static void AddProcess(int psHandle)
        {
            if (Processes == null)
            {
                Processes = new List<int>();
            }

            Processes.Add(psHandle);
        }
        
        public static void RemoveProcess(int psHandle)
        {
            if (Processes == null)
            {
                Processes = new List<int>();
            }

            Processes.Remove(psHandle);
        }
    }
}