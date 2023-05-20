using FOnlineScalex.FRMFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOnlineScalex.Algorithm
{
    public interface IAlgorithm
    {
        public enum AlgorithmId { Scalex2x, Scalex3x, Scalex4x, Hqx2x, Hqx3x, Hqx4x }

        public void Process(
                Frame src, out Frame dst, double eqDiff, bool scale = true
        );

        public void Process(
                Bitmap src, out Bitmap dst, double eqDiff, bool scale = true
        );
    }
}
