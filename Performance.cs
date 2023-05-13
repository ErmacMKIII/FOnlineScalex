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
            if (Directory.Exists(inDir))
            {
                string[] fileArray = recursive ? BuildTree(inDir) : Directory.GetFiles(inDir);

                foreach (string srcFile in fileArray)
                {
                    if (stopped)
                    {
                        logger.Log("Performance stopped!", null);
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
                            Scalex.Scalex.Scalex2x(srcFrame, out dstFrame);
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
                            string dirs = srcFile;
                            StringBuilder sb = new StringBuilder();
                            while (!dirs.Equals(inDir))
                            {
                                var dirsInfo = Directory.GetParent(dirs);
                                dirs = dirsInfo.Name;
                                sb.Insert(0, dirs);
                                sb.Insert(0, Path.PathSeparator);
                            }
                            outFile = Path.Combine(outDir + Path.PathSeparator + sb.ToString() + Path.PathSeparator + Regex.Replace(srcFile, "[.][^.]+$", ".FRM"));
                        }
                        else
                        {
                            outFile = Path.Combine(outDir + Path.PathSeparator + Regex.Replace(srcFile, "[.][^.]+$", ".FRM"));
                        }

                        if (File.Exists(outFile))
                        {
                            File.Delete(outFile);
                        }

                        Directory.CreateDirectory(outDir);

                        dstFRM.Export(outFile);
                        progress += 100.0f / fileArray.Length;
                        //firePropertyChange("progress", oldProgress, progress);
                    }

                    logger.Log("Performance work finished!", null);
                    progress = 100.0f;
                    //firePropertyChange("progress", oldProgress, progress);
                }

                
            }
        }

        private static string[] BuildTree(string inDir)
        {
            List<string> result = new List<string>();

            Stack<string> stack = new Stack<string>();
            stack.Push(inDir);

            while (stack.Count != 0)
            {
                string file = stack.Pop();
                if (Directory.Exists(file))
                {
                    string[] list = Directory.GetFiles(file);
                    for (int i = list.Length - 1; i >= 0; i--)
                    {
                        string chldFile = list[i];
                        stack.Push(chldFile);
                    }
                }
                else
                {
                    result.Add(file);
                }
            }

            return result.ToArray();
        }
    }
}
