using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FOnlineScalex.FRMFile;
using FOnlineScalex.Logger;

namespace FOnlineScalex
{
    public static class Performance
    {
        public static void Work(string inDir, string outDir, bool recursive, IFOSLogger logger)
        {
            float oldProgress = 0.0f, progress = 0.0f;
            progress = 0.0f;

            if (!Directory.Exists(inDir))
            {
                progress = 100.0f;
                //firePropertyChange("progress", oldProgress, progress);
                return;
            }

            if (!Directory.Exists(outDir))
            {
                Directory.CreateDirectory(outDir);
            }

            bool stopped = false;

            logger.Log("Starting Performance Work");
            logger.Log($"Progress: {progress}");
            if (Directory.Exists(inDir))
            {
                string[] fileArray = recursive ? Directory.GetFileSystemEntries(inDir, "*.FRM", new EnumerationOptions { RecurseSubdirectories = true, MatchCasing = MatchCasing.CaseInsensitive }) : Directory.GetFiles(inDir);

                foreach (string srcFile in fileArray)
                {
                    if (stopped)
                    {
                        logger.Log("Performance stopped!");
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
                            Scalex.Scalex.Scalex2x(srcFrame, out dstFrame, 0.5);
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
                        progress += 100.0f / (float)fileArray.Length;
                        logger.Log($"Progress: {progress}");
                        //firePropertyChange("progress", oldProgress, progress);
                    }

                }
                
            }
            
            logger.Log("Performance work finished!");
            progress = 100.0f;
            logger.Log($"Progress: {progress}");
            //firePropertyChange("progress", oldProgress, progress);
        }
        
    }
}
