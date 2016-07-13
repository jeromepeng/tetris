using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Math;

namespace TestConsole
{
    class Program
    {
        //-1 3 -7 10
        //-7 -3 5 10
        //3 1 -1 2
        //1 1 -1 2
        
        //0 0 1/2 -1/2 
        //-5/18 -1/18 -7/6 17/6 
        //-2/9 1/18 -1/3 7/6 
        //1/36 1/18 1/6 -1/12
        static void Main(string[] args)
        {
            /*MatrixAdv test1 = new MatrixAdv(4, 4, new double[16] { 
             -1, 3, -7, 10 , 
             -7, -3, 5, 10 , 
             3, 1, -1, 2 , 
             1, 1, -1, 2  });
  
            MatrixAdv test2 = MatrixAdv.Converse(test1);
            test2.OutputInConsole();
            test2 = test1 * test2;
            test2.OutputInConsole();*/
            MatrixAdv gaussTest = SIFT.GetGaussTemplate(1.5, 7);
            MatrixAdv input = new MatrixAdv(8, 8, new double[] 
            {255,255,255,255,255,255,255,255,
             255,255,255,255,255,255,255,255,
             255,255,255,255,255,255,255,255,
             255,255,255,255,255,255,255,255,
             255,255,255,255,255,255,255,255,
             255,255,255,255,255,255,255,255,
             255,255,255,255,255,255,255,255,
             255,255,255,255,255,255,255,255}
            );
            gaussTest.OutputInConsole();
            SIFT.GaussConvolutionResult(input, gaussTest).OutputInConsole();
        }
    }
}
