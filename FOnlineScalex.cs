﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
        public enum Algorithm { Scalex2x, Scalex3x, Scalex4x }

        public string? ProcessingFile { get; private set; }

        public float Progress { get; private set; }

        public Bitmap? Frame { get; private set; }

        public bool Cancelled { get; private set; }        

        /// <summary>
        /// The ProgressUpdate.
        /// </summary>
        /// <param name="progress">The value<see cref="int"/>.</param>
        public delegate void ProgressUpdate(int progress, string? file, Bitmap? frame);

        /// <summary>
        /// Defines the OnProgressUpdate.
        /// </summary>
        public event ProgressUpdate? OnProgressUpdate;

        public event Cancel? OnCancel;
        public delegate void Cancel();

        public FOnlineScalex()
        {
            
        }

        public void DoWork(string inDir, string outDir, bool recursive, double eqDiff, double neqDiff, Algorithm? algorithm, IFOSLogger logger)
        {
            logger.Log($"App started work with parameters: ALGO:{algorithm}, EQ:{eqDiff}, NEW:{neqDiff}");
            Cancelled = false;
            Progress = 0.0f;

            if (!Directory.Exists(inDir))
            {
                Progress = 100.0f;
                if (OnProgressUpdate != null)
                {
                    OnProgressUpdate((int)Math.Floor(Progress), null, null);
                }
                return;
            }

            if (!Directory.Exists(outDir))
            {
                Directory.CreateDirectory(outDir);
            }

            logger.Log("Starting FOnlineScalex DoWork");
            logger.Log($"Progress: {Progress}");
            if (Directory.Exists(inDir))
            {
                string[] fileArray = recursive ? Directory.GetFileSystemEntries(inDir, "*", new EnumerationOptions { RecurseSubdirectories = true, MatchCasing = MatchCasing.CaseInsensitive }) : Directory.GetFiles(inDir);
                string outFile = string.Empty;
                foreach (string srcFile in fileArray)
                {                    
                    // if it's png or FRM file
                    // get extensionless filename
                    // string extLessFilename = Regex.Replace(srcFile, "[.][^.]+$", string.Empty);
                    //----------------------------------------------------------
                    if (srcFile.ToLower().EndsWith(".frm"))
                    {
                        List<Frame>? srcFrames = null;
                        List<Frame>? dstFrames = null;

                        FRM srcFRM = new FRM(srcFile);
                        //-----------------------------------------------------
                        srcFrames = srcFRM.Frames;
                        dstFrames = new List<Frame>();
                        foreach (Frame srcFrame in srcFrames)
                        {
                            Frame dstFrame;
                            switch (algorithm)
                            {
                                case Algorithm.Scalex2x:
                                default:
                                    Scalex.Scalex.Scalex2x(srcFrame, out dstFrame, eqDiff, neqDiff);
                                    break;
                                case Algorithm.Scalex3x:
                                    Scalex.Scalex.Scalex3x(srcFrame, out dstFrame, eqDiff, neqDiff);
                                    break;
                                case Algorithm.Scalex4x:
                                    Scalex.Scalex.Scalex4x(srcFrame, out dstFrame, eqDiff, neqDiff);
                                    break;
                            }

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
                        dstFRM.Export(outFile);

                        logger.Log(outFile);
                        Progress += 100.0f / (float)fileArray.Length;
                        logger.Log($"Progress: {Progress}");
                        if (OnProgressUpdate != null)
                        {
                            ProcessingFile = outFile;
                            Frame = dstFrames?.FirstOrDefault()?.ToBitmap();
                            OnProgressUpdate((int)Math.Floor(Progress), outFile, dstFrames?.FirstOrDefault()?.ToBitmap());
                        }
                    }
                    else if (srcFile.ToLower().EndsWith(".bmp") || srcFile.ToLower().EndsWith(".png"))
                    {
                        //-----------------------------------------------------
                        Bitmap inPic = (Bitmap)Bitmap.FromFile(srcFile);
                        Bitmap outPic;

                        switch (algorithm)
                        {
                            case Algorithm.Scalex2x:
                            default:
                                Scalex.ScalexBitmap.Scalex2x(inPic, out outPic, eqDiff, neqDiff);
                                break;
                            case Algorithm.Scalex3x:
                                Scalex.ScalexBitmap.Scalex3x(inPic, out outPic, eqDiff, neqDiff);
                                break;
                            case Algorithm.Scalex4x:
                                Scalex.ScalexBitmap.Scalex4x(inPic, out outPic, eqDiff, neqDiff);
                                break;
                        }
                        string extension = Path.GetExtension(srcFile).ToLower();
                        if (recursive)
                        {
                            var srcFileName = Path.GetFileName(srcFile);
                            var relative = Path.GetRelativePath(inDir, srcFile).Replace(srcFileName, string.Empty);
                            outFile = Path.Combine(outDir, relative, Regex.Replace(srcFileName, "[.][^.]+$", extension.ToUpper()));
                        }
                        else
                        {
                            outFile = Path.Combine(outDir, Regex.Replace(Path.GetFileName(srcFile), "[.][^.]+$", extension.ToUpper()));
                        }
                        if (File.Exists(outFile))
                        {
                            File.Delete(outFile);
                        }

                        Directory.CreateDirectory(outDir);

                        if (extension.Equals(".png"))
                        {
                            outPic.Save(outFile, ImageFormat.Png);
                        } 
                        else if (extension.Equals(".bmp"))
                        {
                            outPic.Save(outFile, ImageFormat.Bmp);
                        }

                        logger.Log(outFile);
                        Progress += 100.0f / (float)fileArray.Length;
                        logger.Log($"Progress: {Progress}");
                        if (OnProgressUpdate != null)
                        {
                            ProcessingFile = outFile;
                            Frame = outPic;
                            OnProgressUpdate((int)Math.Floor(Progress), outFile, outPic);
                        }
                    }                    
                    //logger.Log(outFile);
                    //Progress += 100.0f / (float)fileArray.Length;
                    //logger.Log($"Progress: {Progress}");
                    //if (OnProgressUpdate != null)
                    //{
                    //    ProcessingFile = outFile;
                    //    Frame = dstFrames?.FirstOrDefault();
                    //    OnProgressUpdate((int)Math.Floor(Progress), outFile, dstFrames?.FirstOrDefault());
                    //}

                    // can be cancelled externally
                    if (Cancelled && OnCancel != null)
                    {
                        OnCancel();
                        logger.Log("FOnlineScalex cancelled by user!");
                        break;
                    }

                }

                
            }
            
            logger.Log("FOnlineScalex work finished!");
            Progress = 100.0f;
            logger.Log($"Progress: {Progress}");
            if (OnProgressUpdate != null)
            {
                ProcessingFile = string.Empty;
                Frame = null;
                OnProgressUpdate((int)Math.Floor(Progress), null, null);
            }
        }

        public void CancelWork()
        {
            Cancelled = true;
        }

    }
}
