using FOnlineScalex.FRMFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOnlineScalex.Algorithm.HqxFamily
{
    /*
     * Copyright © 2003 Maxim Stepin (maxst@hiend3d.com)
     *
     * Copyright © 2010 Cameron Zemek (grom@zeminvaders.net)
     *
     * Copyright © 2011 Tamme Schichler (tamme.schichler@googlemail.com)
     *
     * Copyright © 2012 A. Eduardo García (arcnorj@gmail.com)
     */

    // --------------------------------
    // Algorithm provided from:
    // https://github.com/VincenzoLaSpesa/hqxcli-java
    // --------------------------------

    /// <summary>
    /// Maxim Stepin Hqx2x class
    /// </summary>
    public class Hqx2x : Hqx
    {
        public static void Hqx2xMeth(Frame src, out Frame dst, double eqDiff, bool wrapX = false, bool wrapY = false)
        {
            uint Xres, Yres;
            Xres = (uint)src.Width;
            Yres = (uint)src.Height;
            dst = new Frame(Xres * 2, Yres * 2, src.OffsetY, src.OffsetY);
            uint spIdx = 0, dpIdx = 0;
            //Don't shift trA, as it uses shift right instead of a mask for comparisons.
            uint dpL = Xres * 2;

            uint prevline, nextline;
            uint[] w = new uint[9];

            for (uint j = 0; j < Yres; j++)
            {
                prevline = (uint)(int)((j > 0)
                        ? -Xres
                        : wrapY
                            ? Xres * (Yres - 1)
                            : 0);
                nextline = (uint)(int)((j < Yres - 1)
                        ? Xres
                        : wrapY
                            ? -(Xres * (Yres - 1))
                            : 0);
                for (uint i = 0; i < Xres; i++)
                {
                    w[1] = src.GetPixel(spIdx + prevline);
                    w[4] = src.GetPixel(spIdx);
                    w[7] = src.GetPixel(spIdx + nextline);

                    if (i > 0)
                    {
                        w[0] = src.GetPixel(spIdx + prevline - 1);
                        w[3] = src.GetPixel(spIdx - 1);
                        w[6] = src.GetPixel(spIdx + nextline - 1);
                    }
                    else
                    {
                        if (wrapX)
                        {
                            w[0] = src.GetPixel(spIdx + prevline + Xres - 1);
                            w[3] = src.GetPixel(spIdx + Xres - 1);
                            w[6] = src.GetPixel(spIdx + nextline + Xres - 1);
                        }
                        else
                        {
                            w[0] = w[1];
                            w[3] = w[4];
                            w[6] = w[7];
                        }
                    }

                    if (i < Xres - 1)
                    {
                        w[2] = src.GetPixel(spIdx + prevline + 1);
                        w[5] = src.GetPixel(spIdx + 1);
                        w[8] = src.GetPixel(spIdx + nextline + 1);
                    }
                    else
                    {
                        if (wrapX)
                        {
                            w[2] = src.GetPixel(spIdx + prevline - Xres + 1);
                            w[5] = src.GetPixel(spIdx - Xres + 1);
                            w[8] = src.GetPixel(spIdx + nextline - Xres + 1);
                        }
                        else
                        {
                            w[2] = w[1];
                            w[5] = w[4];
                            w[8] = w[7];
                        }
                    }

                    uint pattern = 0;
                    uint flag = 1;

                    for (uint k = 0; k < 9; k++)
                    {
                        if (k == 4) continue;

                        if (w[k] != w[4])
                        {
                            if (PixelRGBNotEqual(w[4], w[k], eqDiff))
                                pattern |= flag;
                        }
                        flag <<= 1;
                    }

                    switch (pattern)
                    {
                        case 0:
                        case 1:
                        case 4:
                        case 32:
                        case 128:
                        case 5:
                        case 132:
                        case 160:
                        case 33:
                        case 129:
                        case 36:
                        case 133:
                        case 164:
                        case 161:
                        case 37:
                        case 165:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 2:
                        case 34:
                        case 130:
                        case 162:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 16:
                        case 17:
                        case 48:
                        case 49:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 64:
                        case 65:
                        case 68:
                        case 69:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 8:
                        case 12:
                        case 136:
                        case 140:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 3:
                        case 35:
                        case 131:
                        case 163:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 6:
                        case 38:
                        case 134:
                        case 166:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 20:
                        case 21:
                        case 52:
                        case 53:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 144:
                        case 145:
                        case 176:
                        case 177:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 192:
                        case 193:
                        case 196:
                        case 197:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 96:
                        case 97:
                        case 100:
                        case 101:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 40:
                        case 44:
                        case 168:
                        case 172:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 9:
                        case 13:
                        case 137:
                        case 141:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 18:
                        case 50:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 80:
                        case 81:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 72:
                        case 76:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 10:
                        case 138:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 66:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 24:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 7:
                        case 39:
                        case 135:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 148:
                        case 149:
                        case 180:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 224:
                        case 228:
                        case 225:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 41:
                        case 169:
                        case 45:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 22:
                        case 54:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 208:
                        case 209:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 104:
                        case 108:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 11:
                        case 139:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 19:
                        case 51:
                            {
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To3To3(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 146:
                        case 178:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To3To3(w[4], w[1], w[5]));
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                break;
                            }
                        case 84:
                        case 85:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To3To3(w[4], w[5], w[7]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                break;
                            }
                        case 112:
                        case 113:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To3To3(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 200:
                        case 204:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To3To3(w[4], w[7], w[3]));
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
                                }
                                break;
                            }
                        case 73:
                        case 77:
                            {
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To3To3(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 42:
                        case 170:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To3To3(w[4], w[3], w[1]));
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 14:
                        case 142:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To3To3(w[4], w[3], w[1]));
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 67:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 70:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 28:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 152:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 194:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 98:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 56:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 25:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 26:
                        case 31:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 82:
                        case 214:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 88:
                        case 248:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 74:
                        case 107:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 27:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 86:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 216:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 106:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 30:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 210:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 120:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 75:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 29:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 198:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 184:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 99:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 57:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 71:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 156:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 226:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 60:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 195:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 102:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 153:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 58:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 83:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 92:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 202:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 78:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 154:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 114:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 89:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 90:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 55:
                        case 23:
                            {
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To3To3(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 182:
                        case 150:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To3To3(w[4], w[1], w[5]));
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                break;
                            }
                        case 213:
                        case 212:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To3To3(w[4], w[5], w[7]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                break;
                            }
                        case 241:
                        case 240:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To3To3(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 236:
                        case 232:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To3To3(w[4], w[7], w[3]));
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
                                }
                                break;
                            }
                        case 109:
                        case 105:
                            {
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To3To3(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 171:
                        case 43:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To3To3(w[4], w[3], w[1]));
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 143:
                        case 15:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To3To3(w[4], w[3], w[1]));
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 124:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 203:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 62:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 211:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 118:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 217:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 110:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 155:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 188:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 185:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 61:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 157:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 103:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 227:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 230:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 199:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 220:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 158:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 234:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 242:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 59:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 121:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 87:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 79:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 122:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 94:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 218:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 91:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 229:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 167:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 173:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 181:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 186:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 115:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 93:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 206:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 205:
                        case 201:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 174:
                        case 46:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 179:
                        case 147:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 117:
                        case 116:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 189:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 231:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 126:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 219:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 125:
                            {
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To3To3(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 221:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To3To3(w[4], w[5], w[7]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                break;
                            }
                        case 207:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To3To3(w[4], w[3], w[1]));
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 238:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To3To3(w[4], w[7], w[3]));
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
                                }
                                break;
                            }
                        case 190:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To3To3(w[4], w[1], w[5]));
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 187:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To3To3(w[4], w[3], w[1]));
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 243:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To3To3(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 119:
                            {
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To3To3(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 237:
                        case 233:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix14To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 175:
                        case 47:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix14To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 183:
                        case 151:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix14To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 245:
                        case 244:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix14To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 250:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 123:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 95:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 222:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 252:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix14To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 249:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix14To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 235:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix14To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 111:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix14To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 63:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix14To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 159:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix14To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 215:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix14To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 246:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix14To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 254:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix14To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 253:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix14To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix14To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 251:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix14To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 239:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix14To1To1(w[4], w[3], w[1]));
                                }
                                dst.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix14To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 127:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix14To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 191:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix14To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix14To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 223:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix14To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 247:
                            {
                                dst.SetPixel(dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix14To1To1(w[4], w[1], w[5]));
                                }
                                dst.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix14To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 255:
                            {
                                if (PixelRGBNotEqual(w[3], w[1], eqDiff))
                                {
                                    dst.SetPixel(dpIdx, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx, Interpolation.Mix14To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBNotEqual(w[1], w[5], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + 1, Interpolation.Mix14To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBNotEqual(w[7], w[3], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL, Interpolation.Mix14To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBNotEqual(w[5], w[7], eqDiff))
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    dst.SetPixel(dpIdx + dpL + 1, Interpolation.Mix14To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                    }
                    spIdx++;
                    dpIdx += 2;
                }
                dpIdx += dpL;
            }
        }
        public static void Hqx2xMeth(Bitmap src, out Bitmap dst, double eqDiff, bool wrapX = false, bool wrapY = false)
        {
            uint Xres, Yres;
            Xres = (uint)src.Width;
            Yres = (uint)src.Height;
            dst = new Bitmap((int)Xres * 2, (int)Yres * 2);
            uint spIdx = 0, dpIdx = 0;
            //Don't shift trA, as it uses shift right instead of a mask for comparisons.
            uint dpL = Xres * 2;

            uint prevline, nextline;
            uint[] w = new uint[9];

            for (uint j = 0; j < Yres; j++)
            {
                prevline = (uint)(int)((j > 0)
                        ? -Xres
                        : wrapY
                            ? Xres * (Yres - 1)
                            : 0);
                nextline = (uint)(int)((j < Yres - 1)
                        ? Xres
                        : wrapY
                            ? -(Xres * (Yres - 1))
                            : 0);

                for (uint i = 0; i < Xres; i++)
                {
                    w[1] = GetPixel(src, spIdx + prevline);
                    w[4] = GetPixel(src, spIdx);
                    w[7] = GetPixel(src, spIdx + nextline);

                    if (i > 0)
                    {
                        w[0] = GetPixel(src, spIdx + prevline - 1);
                        w[3] = GetPixel(src, spIdx - 1);
                        w[6] = GetPixel(src, spIdx + nextline - 1);
                    }
                    else
                    {
                        if (wrapX)
                        {
                            w[0] = GetPixel(src, spIdx + prevline + Xres - 1);
                            w[3] = GetPixel(src, spIdx + Xres - 1);
                            w[6] = GetPixel(src, spIdx + nextline + Xres - 1);
                        }
                        else
                        {
                            w[0] = w[1];
                            w[3] = w[4];
                            w[6] = w[7];
                        }
                    }

                    if (i < Xres - 1)
                    {
                        w[2] = GetPixel(src, spIdx + prevline + 1);
                        w[5] = GetPixel(src, spIdx + 1);
                        w[8] = GetPixel(src, spIdx + nextline + 1);
                    }
                    else
                    {
                        if (wrapX)
                        {
                            w[2] = GetPixel(src, spIdx + prevline - Xres + 1);
                            w[5] = GetPixel(src, spIdx - Xres + 1);
                            w[8] = GetPixel(src, spIdx + nextline - Xres + 1);
                        }
                        else
                        {
                            w[2] = w[1];
                            w[5] = w[4];
                            w[8] = w[7];
                        }
                    }

                    uint pattern = 0;
                    uint flag = 1;

                    for (uint k = 0; k < 9; k++)
                    {
                        if (k == 4) continue;

                        if (w[k] != w[4])
                        {
                            if (PixelRGBANotEqual(w[4], w[k], eqDiff))
                                pattern |= flag;
                        }
                        flag <<= 1;
                    }

                    switch (pattern)
                    {
                        case 0:
                        case 1:
                        case 4:
                        case 32:
                        case 128:
                        case 5:
                        case 132:
                        case 160:
                        case 33:
                        case 129:
                        case 36:
                        case 133:
                        case 164:
                        case 161:
                        case 37:
                        case 165:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 2:
                        case 34:
                        case 130:
                        case 162:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 16:
                        case 17:
                        case 48:
                        case 49:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 64:
                        case 65:
                        case 68:
                        case 69:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 8:
                        case 12:
                        case 136:
                        case 140:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 3:
                        case 35:
                        case 131:
                        case 163:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 6:
                        case 38:
                        case 134:
                        case 166:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 20:
                        case 21:
                        case 52:
                        case 53:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 144:
                        case 145:
                        case 176:
                        case 177:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 192:
                        case 193:
                        case 196:
                        case 197:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 96:
                        case 97:
                        case 100:
                        case 101:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 40:
                        case 44:
                        case 168:
                        case 172:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 9:
                        case 13:
                        case 137:
                        case 141:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 18:
                        case 50:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 80:
                        case 81:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 72:
                        case 76:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 10:
                        case 138:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 66:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 24:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 7:
                        case 39:
                        case 135:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 148:
                        case 149:
                        case 180:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 224:
                        case 228:
                        case 225:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 41:
                        case 169:
                        case 45:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 22:
                        case 54:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 208:
                        case 209:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 104:
                        case 108:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 11:
                        case 139:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 19:
                        case 51:
                            {
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To3To3(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 146:
                        case 178:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To3To3(w[4], w[1], w[5]));
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                break;
                            }
                        case 84:
                        case 85:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To3To3(w[4], w[5], w[7]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                break;
                            }
                        case 112:
                        case 113:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To3To3(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 200:
                        case 204:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To3To3(w[4], w[7], w[3]));
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
                                }
                                break;
                            }
                        case 73:
                        case 77:
                            {
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To3To3(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 42:
                        case 170:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To3To3(w[4], w[3], w[1]));
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 14:
                        case 142:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To3To3(w[4], w[3], w[1]));
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 67:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 70:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 28:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 152:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 194:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 98:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 56:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 25:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 26:
                        case 31:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 82:
                        case 214:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 88:
                        case 248:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 74:
                        case 107:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 27:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 86:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 216:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 106:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 30:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 210:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 120:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 75:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 29:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 198:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 184:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 99:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 57:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 71:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 156:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 226:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 60:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 195:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 102:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 153:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 58:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 83:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 92:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 202:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 78:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 154:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 114:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 89:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 90:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 55:
                        case 23:
                            {
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To3To3(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 182:
                        case 150:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To3To3(w[4], w[1], w[5]));
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                break;
                            }
                        case 213:
                        case 212:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To3To3(w[4], w[5], w[7]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                break;
                            }
                        case 241:
                        case 240:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To3To3(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 236:
                        case 232:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To3To3(w[4], w[7], w[3]));
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
                                }
                                break;
                            }
                        case 109:
                        case 105:
                            {
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To3To3(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 171:
                        case 43:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To3To3(w[4], w[3], w[1]));
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 143:
                        case 15:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To3To3(w[4], w[3], w[1]));
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 124:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 203:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 62:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 211:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 118:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 217:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 110:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 155:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 188:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 185:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 61:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 157:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 103:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 227:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 230:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 199:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 220:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 158:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 234:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 242:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 59:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 121:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 87:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 79:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 122:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 94:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 218:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 91:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 229:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 167:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 173:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 181:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 186:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 115:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 93:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 206:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 205:
                        case 201:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix6To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 174:
                        case 46:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 179:
                        case 147:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix6To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 117:
                        case 116:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 189:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 231:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 126:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 219:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 125:
                            {
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To3To3(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 221:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To3To3(w[4], w[5], w[7]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                break;
                            }
                        case 207:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To3To3(w[4], w[3], w[1]));
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 238:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To3To3(w[4], w[7], w[3]));
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
                                }
                                break;
                            }
                        case 190:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To3To3(w[4], w[1], w[5]));
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 187:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To3To3(w[4], w[3], w[1]));
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 243:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To3To3(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 119:
                            {
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To3To3(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 237:
                        case 233:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix14To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 175:
                        case 47:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix14To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                break;
                            }
                        case 183:
                        case 151:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix14To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 245:
                        case 244:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix14To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 250:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 123:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 95:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 222:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 252:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix14To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 249:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[1]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix14To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 235:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[2], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix14To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 111:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix14To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[5]));
                                break;
                            }
                        case 63:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix14To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[8], w[7]));
                                break;
                            }
                        case 159:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix14To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 215:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix14To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[6], w[3]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 246:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[0], w[3]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix14To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 254:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[0]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix14To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 253:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[1]));
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[1]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix14To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix14To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 251:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[2]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix14To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 239:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix14To1To1(w[4], w[3], w[1]));
                                }
                                SetPixel(dst, dpIdx + 1, Interpolation.Mix3To1(w[4], w[5]));
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix14To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[5]));
                                break;
                            }
                        case 127:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix14To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
                                }
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[8]));
                                break;
                            }
                        case 191:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix14To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix14To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[7]));
                                SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix3To1(w[4], w[7]));
                                break;
                            }
                        case 223:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix2To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix14To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[6]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix2To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 247:
                            {
                                SetPixel(dst, dpIdx, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix14To1To1(w[4], w[1], w[5]));
                                }
                                SetPixel(dst, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[3]));
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix14To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                        case 255:
                            {
                                if (PixelRGBANotEqual(w[3], w[1], eqDiff))
                                {
                                    SetPixel(dst, dpIdx, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx, Interpolation.Mix14To1To1(w[4], w[3], w[1]));
                                }
                                if (PixelRGBANotEqual(w[1], w[5], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + 1, Interpolation.Mix14To1To1(w[4], w[1], w[5]));
                                }
                                if (PixelRGBANotEqual(w[7], w[3], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL, Interpolation.Mix14To1To1(w[4], w[7], w[3]));
                                }
                                if (PixelRGBANotEqual(w[5], w[7], eqDiff))
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, w[4]);
                                }
                                else
                                {
                                    SetPixel(dst, dpIdx + dpL + 1, Interpolation.Mix14To1To1(w[4], w[5], w[7]));
                                }
                                break;
                            }
                    }
                    spIdx++;
                    dpIdx += 2;
                }
                dpIdx += dpL;
            }
        }

        public override void Process(Frame src, out Frame dst, double eqDiff, bool scale = true)
        {
            Hqx2xMeth(src, out dst, eqDiff);
            if (!scale)
            {
                dst = new Frame(new Bitmap(src.ToBitmap(), (int)src.Width, (int)src.Height), src.OffsetX, src.OffsetY);
            }
        }

        public override void Process(Bitmap src, out Bitmap dst, double eqDiff, bool scale = true)
        {
            Hqx2xMeth(src, out dst, eqDiff);
            if (!scale)
            {
                dst = new Bitmap(src, (int)src.Width, (int)src.Height);
            }
        }
    }
}
