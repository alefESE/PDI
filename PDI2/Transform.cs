using System;

namespace PDI2
{
    public class Transform
    {

        public float[] DCT(float[] matrix, int n)
        {
            float[] dct = new float[matrix.Length];

            float ci, dct1, sum;
            int i, j;
            for(i = 0; i < n; i++)
            {
                if (i == 0)
                    ci = 1 / (float)Math.Sqrt(n);
                else
                    ci = (float)Math.Sqrt(2) / (float)Math.Sqrt(n);

                sum = 0;
                for (j = 0; j < n; j++)
                {
                    dct1 = matrix[j] * (float)Math.Cos((2 * j + 1) * i * (float)Math.PI / (2 * n));
                    sum += dct1;
                }
                dct[i] = ci * sum;
            }

            return dct;
        }

        public float[] IDCT(float[] matrix)
        {
            float[] idct = new float[matrix.Length];

            return idct;
        }

        public float[,] DCT(float[,] matrix, int m, int n)
        {
            float[,] dct = new float[m, n];

            float ci, cj, dct1, sum;
            int i, j, k, l;
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < n; j++)
                {

                    // ci and cj depends on frequency as well as 
                    // number of row and columns of specified matrix 
                    if (i == 0)
                        ci = 1 / (float)Math.Sqrt(m);
                    else
                        ci = (float)Math.Sqrt(2) / (float)Math.Sqrt(m);
                    if (j == 0)
                        cj = 1 / (float)Math.Sqrt(n);
                    else
                        cj = (float)Math.Sqrt(2) / (float)Math.Sqrt(n);

                    // sum will temporarily store the sum of 
                    // cosine signals 
                    sum = 0;
                    for (k = 0; k < m; k++)
                    {
                        for (l = 0; l < n; l++)
                        {
                            dct1 = matrix[k, l] *
                                (float)Math.Cos((2 * k + 1) * i * (float)Math.PI / (2 * m)) *
                                (float)Math.Cos((2 * l + 1) * j * (float)Math.PI / (2 * n));
                            sum += dct1;
                        }
                    }
                    dct[i, j] = ci * cj * sum;
                }
            }
            return dct;
        }

        public float[,] IDCT(float[,] matrix)
        {
            float[,] idct = new float[matrix.Length, matrix.Length];


            return idct;
        }
    }
}
