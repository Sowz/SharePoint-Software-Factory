namespace SPALM.SPSF.Library
{
    using System.Collections.Generic;
    using EnvDTE;
    using SPALM.SPSF.SharePointBridge;

    internal class WorkerObject
    {
        public string Operation = "";
        public DTE DTE;
        public List<SharePointDeploymentJob> WSPFiles;
    }
}
