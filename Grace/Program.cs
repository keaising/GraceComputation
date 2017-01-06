using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace Grace
{
    class Program
    {
        static void Main(string[] args)
        {
            var grace = "Grace!";
            WriteLine($"Hello {nameof(grace)}");
            Mat();
            ReadKey();
        }

        public static void Mat()
        {
            Matrix<double> A = DenseMatrix.OfArray(new double[,] {
                                                    {1,1,1,1},
                                                    {1,2,3,4},
                                                    {4,3,2,1}});
            Vector<double>[] nullspace = A.Kernel();
            WriteLine(A.ToString());
        }
    }
}
