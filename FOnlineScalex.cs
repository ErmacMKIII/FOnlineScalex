using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FOnlineScalex.FRMFile;
using FOnlineScalex.Logger;

namespace FOnlineScalex
{
    public class FOnlineScalex
    {
        public float Progress { get; private set; }

        /// <summary>
        /// The ProgressUpdate.
        /// </summary>
        /// <param name="progress">The value<see cref="int"/>.</param>
        public delegate void ProgressUpdate(int progress);

        /// <summary>
        /// Defines the OnProgressUpdate.
        /// </summary>
        public event ProgressUpdate OnProgressUpdate;

        public FOnlineScalex()
        {
            
        }

        public void Work(string inDir, string outDir, bool recursive, IFOSLogger logger)
        {
            Progress = 0.0f;

            if (!Directory.Exists(inDir))
            {
                Progress = 100.0f;
                if (OnProgressUpdate != null)
                {
                    OnProgressUpdate((int)Math.Round(Progress));
                }
                return;
            }

            if (!Directory.Exists(outDir))
            {
                Directory.CreateDirectory(outDir);
            }

            bool stopped = false;

            logger.Log("Starting FOnlineScalex Work");
            logger.Log($"Progress: {Progress}");
            if (Directory.Exists(inDir))
            {
                string[] fileArray = recursive ? Directory.GetFileSystemEntries(inDir, "*.FRM", new EnumerationOptions { RecurseSubdirectories = true, MatchCasing = MatchCasing.CaseInsensitive }) : Directory.GetFiles(inDir);

                foreach (string srcFile in fileArray)
                {
                    if (stopped)
                    {
                        logger.Log("FOnlineScalex stopped!");
                        break;
                    }

                    // if it's png or FRM file
                    // get extensionless filename
                    string extLessFilename = Regex.Replace(srcFile, "[.][^.]+$", string.Empty);
                    //----------------------------------------------------------
                    if (srcFile.ToLower().EndsWith(".frm"))
                    {
                        FRM srcFRM = new FRM(srcFile);
                        //-----------------------------------------------------
                        List<Frame> srcFrames = srcFRM.Frames;
                        List<Frame> dstFrames = new List<Frame>();                        
                        foreach (Frame srcFrame in srcFrames)
                        {
                            Frame dstFrame;
                            Scalex.Scalex.Scalex3x(srcFrame, out dstFrame, 1.0);
                            dstFrames.Add(dstFrame);
                        }

                        FRM dstFRM = new FRM(
                                srcFRM.Version,
                                srcFRM.Fps,
                                srcFRM.ActionFrame,
                                srcFRM.FramesPerDirection,
                                srcFRM.ShiftX,
                                srcFRM.ShiftY,
                                dstFrames
                        );

                        string outFile;
                        if (recursive)
                        {
                            var srcFileName = Path.GetFileName(srcFile);
                            var relative = Path.GetRelativePath(inDir, srcFile).Replace(srcFileName, string.Empty);                            
                            outFile = Path.Combine(outDir, relative, Regex.Replace(srcFileName, "[.][^.]+$", ".FRM"));
                        }
                        else
                        {
                            outFile = Path.Combine(outDir, Regex.Replace(Path.GetFileName(srcFile), "[.][^.]+$", ".FRM"));
                        }

                        if (File.Exists(outFile))
                        {
                            File.Delete(outFile);
                        }

                        Directory.CreateDirectory(outDir);
                        logger.Log(outFile);
                        dstFRM.Export(outFile);
                        Progress += 100.0f / (float)fileArray.Length;
                        logger.Log($"Progress: {Progress}");
                        if (OnProgressUpdate != null)
                        {
                            OnProgressUpdate((int)Math.Round(Progress));
                        }
                    }

                }
                
            }
            
            logger.Log("FOnlineScalex work finished!");
            Progress = 100.0f;
            logger.Log($"Progress: {Progress}");
            if (OnProgressUpdate != null)
            {
                OnProgressUpdate((int)Math.Round(Progress));
            }
        }
        
    }
}
