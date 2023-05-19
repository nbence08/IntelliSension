using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace IntelliSension
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("f24d9d41-4b3c-4059-bc3f-8fa342bb7f0d")]
    public class IntelliSyncSettings : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntelliSyncSettings"/> class.
        /// </summary>
        public IntelliSyncSettings() : base(null)
        {
            this.Caption = "IntelliSyncSettings";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new SettingsWindowControl();
        }
    }
}
