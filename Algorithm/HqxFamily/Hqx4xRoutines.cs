/* Copyright (C) 2023 Aleksandar Stojanovic <coas91@rocketmail.com>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/> */
using FOnlineScalex.FRMFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOnlineScalex.Algorithm.HqxFamily
{
    // --------------------------------
    // Algorithm provided from:
    // https://github.com/VincenzoLaSpesa/hqxcli-java
    // --------------------------------

    /// <summary>
    /// Routines for Maxim Stepin Hqx4x class
    /// </summary>
    public class Hqx4xRoutines
    {
        public static void case0(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case2(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[0]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix3To1(w[4], w[2]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[0]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[2]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case16(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[2]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix3To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix3To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case64(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix3To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix3To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case8(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[0]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix3To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case3(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[3]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix7To1(w[4], w[3]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix3To1(w[4], w[2]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix5To3(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[2]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case6(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[0]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix7To1(w[4], w[5]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[0]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case20(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix5To3(w[4], w[1]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[1]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[1]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix7To1(w[4], w[1]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix3To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case144(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[2]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix3To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix7To1(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix5To3(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[7]));
        }

        public static void case192(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix3To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
        }

        public static void case96(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix3To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case40(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[0]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix7To1(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix5To3(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case9(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[1]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix5To3(w[4], w[1]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix7To1(w[4], w[1]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[1]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix3To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case66(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[0]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix3To1(w[4], w[2]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[0]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[2]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix3To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix3To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case24(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[0]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[2]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix3To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix3To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix3To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case7(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[3]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix7To1(w[4], w[3]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix7To1(w[4], w[5]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix5To3(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case148(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix5To3(w[4], w[1]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[1]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[1]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix7To1(w[4], w[1]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix7To1(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix5To3(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[7]));
        }

        public static void case224(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
        }

        public static void case41(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[1]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix5To3(w[4], w[1]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix7To1(w[4], w[1]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[1]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix7To1(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix5To3(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case67(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[3]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix7To1(w[4], w[3]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix3To1(w[4], w[2]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix5To3(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[2]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix3To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix3To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case70(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[0]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix7To1(w[4], w[5]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[0]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix3To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix3To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case28(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[0]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix5To3(w[4], w[1]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[1]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[1]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix7To1(w[4], w[1]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix3To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix3To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case152(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[0]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[2]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix3To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix3To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix7To1(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix5To3(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[7]));
        }

        public static void case194(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[0]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix3To1(w[4], w[2]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[0]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[2]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix3To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
        }

        public static void case98(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix3To1(w[4], w[0]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix3To1(w[4], w[2]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[0]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[2]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix3To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case56(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[0]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[2]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix3To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix3To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix7To1(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix3To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix5To3(w[4], w[7]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case25(Frame dp, uint dpIdx, uint dpL, uint[] w)
        {
            dp.SetPixel(dpIdx, Interpolation.Mix5To3(w[4], w[1]));
            dp.SetPixel(dpIdx + 1, Interpolation.Mix5To3(w[4], w[1]));
            dp.SetPixel(dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[2]));
            dp.SetPixel(dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL, Interpolation.Mix7To1(w[4], w[1]));
            dp.SetPixel(dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[1]));
            dp.SetPixel(dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + 3, Interpolation.Mix3To1(w[4], w[2]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix3To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + 3, Interpolation.Mix3To1(w[4], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[6]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[8]));
            dp.SetPixel(dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }
        // -------------------------------------------------------------------------------------------------------------------------------
        public static void case0(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case2(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix3To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix3To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case16(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[2]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix3To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix3To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case64(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix3To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix3To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case8(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[0]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix3To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case3(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix7To1(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix3To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix5To3(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case6(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix3To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix7To1(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case20(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix5To3(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix7To1(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix3To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case144(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[2]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix3To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix7To1(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix5To3(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[7]));
        }

        public static void case192(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix3To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
        }

        public static void case96(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix3To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case40(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[0]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix7To1(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix5To3(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case9(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix5To3(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix7To1(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix3To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case66(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix3To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix3To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix3To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix3To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case24(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[0]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[2]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix3To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix3To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix3To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case7(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix7To1(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix7To1(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix5To3(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case148(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix5To3(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix7To1(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix7To1(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix2To1To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix5To3(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[7]));
        }

        public static void case224(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix2To1To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[3]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix6To1To1(w[4], w[3], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
        }

        public static void case41(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix5To3(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix2To1To1(w[4], w[1], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix7To1(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix7To1(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix6To1To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix5To3(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix2To1To1(w[4], w[7], w[5]));
        }

        public static void case67(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix7To1(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix3To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix5To3(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix3To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix3To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case70(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix3To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix7To1(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix3To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix3To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case28(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[0]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix5To3(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix7To1(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix3To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix3To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case152(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[0]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[2]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix3To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix3To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix7To1(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix5To3(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[7]));
        }

        public static void case194(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix3To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix3To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix3To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[5]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[5]));
        }

        public static void case98(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix3To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix3To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix4To2To1(w[4], w[3], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix4To2To1(w[4], w[5], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[3]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix3To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case56(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix4To2To1(w[4], w[1], w[0]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[2]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix3To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[0]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix3To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix7To1(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix3To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix5To3(w[4], w[7]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }

        public static void case25(Bitmap dp, uint dpIdx, uint dpL, uint[] w)
        {
            Hqx.SetPixel(dp, dpIdx, Interpolation.Mix5To3(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + 1, Interpolation.Mix5To3(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + 2, Interpolation.Mix4To2To1(w[4], w[1], w[2]));
            Hqx.SetPixel(dp, dpIdx + 3, Interpolation.Mix5To3(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL, Interpolation.Mix7To1(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 1, Interpolation.Mix7To1(w[4], w[1]));
            Hqx.SetPixel(dp, dpIdx + dpL + 2, Interpolation.Mix7To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + 3, Interpolation.Mix3To1(w[4], w[2]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix3To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 1, Interpolation.Mix7To1(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 2, Interpolation.Mix7To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + 3, Interpolation.Mix3To1(w[4], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL, Interpolation.Mix5To3(w[4], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 1, Interpolation.Mix4To2To1(w[4], w[7], w[6]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 2, Interpolation.Mix4To2To1(w[4], w[7], w[8]));
            Hqx.SetPixel(dp, dpIdx + dpL + dpL + dpL + 3, Interpolation.Mix5To3(w[4], w[8]));
        }
    }
}
