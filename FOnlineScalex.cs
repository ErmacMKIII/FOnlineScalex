using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FOnlineScalex.Algorithm;
using FOnlineScalex.Algorithm.HqxFamily;
using FOnlineScalex.FRMFile;
using FOnlineScalex.Logger;
using FOnlineScalex.PostProcessing;
using FOnlineScalex.ScalexFamily;
using static FOnlineScalex.FOnlineScalex;

namespace FOnlineScalex
{
    public class FOnlineScalex
    {
        public string? ErrorMessage { get; protected set; }        

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

        /// <summary>
        /// Perform pixel art on the images {BMP, PNG, FRM} in input directories and write to output directories
        /// </summary>
        /// <param name="inDir">input directory</param>
        /// <param name="outDir">output directory</param>
        /// <param name="recursive">discover directories recursively</param>
        /// <param name="eqDiff">difference when equal</param>
        /// <param name="algorithmId">pixel art scaling algorithm</param>
        /// <param name="scale">scale image</param>
        /// <param name="postProcessing">post processing</param>
        /// <param name="logger">output logger to console (or file)</param>
        public void DoWork(string inDir, string outDir, bool recursive, double eqDiff,  IAlgorithm.AlgorithmId? algorithmId, bool scale, bool postProcessing, IFOSLogger logger)
        {
            logger.Log($"App started work with parameters: ALGOID:{algorithmId}, DIFF:{eqDiff}, SCALE:{scale}, POSTPROC:{postProcessing}");
            Cancelled = false;
            Progress = 0.0f;
            this.ErrorMessage = string.Empty;
            try
            {
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
                                IAlgorithm algorithm;
                                switch (algorithmId)
                                {
                                    case IAlgorithm.AlgorithmId.Scalex2x:
                                    default:
                                        algorithm = new Scalex2x();
                                        break;
                                    case IAlgorithm.AlgorithmId.Scalex3x:
                                        algorithm = new Scalex3x();
                                        break;
                                    case IAlgorithm.AlgorithmId.Scalex4x:
                                        algorithm = new Scalex3x();
                                        break;
                                    case IAlgorithm.AlgorithmId.Hqx2x:
                                        algorithm = new Hqx2x();
                                        break;
                                    case IAlgorithm.AlgorithmId.Hqx3x:
                                        algorithm = new Hqx3x();
                                        break;
                                    case IAlgorithm.AlgorithmId.Hqx4x:
                                        algorithm = new Hqx4x();
                                        break;
                                }
                                algorithm.Process(srcFrame, out dstFrame, eqDiff, scale);
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
                            Bitmap? outPic = null;

                            IAlgorithm algorithm;
                            switch (algorithmId)
                            {
                                case IAlgorithm.AlgorithmId.Scalex2x:
                                default:
                                    algorithm = new Scalex2x();
                                    break;
                                case IAlgorithm.AlgorithmId.Scalex3x:
                                    algorithm = new Scalex3x();
                                    break;
                                case IAlgorithm.AlgorithmId.Scalex4x:
                                    algorithm = new Scalex3x();
                                    break;
                                case IAlgorithm.AlgorithmId.Hqx2x:                                    
                                    algorithm = new Hqx2x();
                                    break;
                                case IAlgorithm.AlgorithmId.Hqx3x:
                                    algorithm = new Hqx3x();
                                    break;
                                case IAlgorithm.AlgorithmId.Hqx4x:
                                    algorithm = new Hqx4x();
                                    break;
                            }
                            algorithm.Process(inPic, out outPic, eqDiff, scale);
                            
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

                            if (outPic != null && postProcessing)
                            {
                                Bitmap finalOutPic;
                                PostProcessor.Process(outPic, out finalOutPic);
                                outPic = finalOutPic;
                            }

                            if (outPic != null)
                            {

                                if (extension.Equals(".png"))
                                {
                                    outPic.Save(outFile, ImageFormat.Png);
                                }
                                else if (extension.Equals(".bmp"))
                                {
                                    outPic.Save(outFile, ImageFormat.Bmp);
                                }
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

                
            } 
            catch (Exception ex) 
            {
                ErrorMessage = $"Internal error {ex.Message}!";
                logger.Log(ErrorMessage, ex, IFOSLogger.LogLevel.ERR);
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
