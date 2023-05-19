using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using System.Diagnostics;

namespace IntelliSension
{

    public class SyncInvoker : IVsRunningDocTableEvents
    {
        private string scriptPath;
        private bool syncOnEdit;
        private bool syncOnOpen;

        public void ScriptPathChangedHandler(string newPath)
        {
            scriptPath = newPath;
        }

        public void SyncOnEditChanged(bool value)
        {
            syncOnEdit = value;
        }

        public void SyncOnOpenChanged(bool value)
        {
            syncOnOpen = value;
        }

        public SyncInvoker(IVsRunningDocumentTable runningDocTable)
        {
            this.runningDocTable = runningDocTable;
        }

        public IVsRunningDocumentTable runningDocTable { get; set; }

        public int OnAfterSave(uint docCookie)
        {
            // File saved event
            return VSConstants.S_OK;
        }

        public int OnBeforeDocumentWindowShow(uint docCookie, int fFirstShow, IVsWindowFrame pFrame)
        {
            if (syncOnOpen)
            {
                string filePath = QueryDocumentPath(docCookie);

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = scriptPath + " " + filePath,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = false
                };

                // Start the process
                using (Process process = Process.Start(startInfo))
                {
                    // Read the output from the Python script
                    string output = process.StandardOutput.ReadToEnd();

                    Console.WriteLine(output);
                }
            }
            return VSConstants.S_OK;
        }

        private string QueryDocumentPath(uint docCookie)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            uint pgrfRDTFlags;
            uint pdwReadLocks;
            uint pdwEditLocks;
            string path;
            IVsHierarchy ppHier;
            uint pitemid;
            IntPtr ppunkDocData;

            runningDocTable.GetDocumentInfo(docCookie, out pgrfRDTFlags, out pdwReadLocks, out pdwEditLocks, out path, out ppHier, out pitemid, out ppunkDocData);

            return path;
        }

        public int OnAfterDocumentWindowHide(uint docCookie, IVsWindowFrame pFrame)
        {
            // File closed event
            return VSConstants.S_OK;
        }

        public int OnAfterFirstDocumentLock(uint docCookie, uint dwRDTLockType, uint dwReadLocksRemaining, uint dwEditLocksRemaining)
        {
            throw new NotImplementedException();
        }

        public int OnBeforeLastDocumentUnlock(uint docCookie, uint dwRDTLockType, uint dwReadLocksRemaining, uint dwEditLocksRemaining)
        {
            throw new NotImplementedException();
        }

        public int OnAfterAttributeChange(uint docCookie, uint grfAttribs)
        {
            throw new NotImplementedException();
        }

        // Implement other methods of the IVsRunningDocTableEvents interface as needed
    }
}
