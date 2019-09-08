using System;
using System.Threading.Tasks;

namespace PDI2
{
    public class Transform
    {
        /// <summary>
        /// Return a vector with DCT transformation of <paramref name="vector"/>
        /// </summary>
        /// <param name="vector">The vector to transform</param>
        /// <returns>Vector with DCT elements</returns>
        public float[] DCT(float[] vector)
        {
            int len = vector.Length;
            float[] dct = new float[len];

            float x = (float)Math.Sqrt(2);
            float y = (float)Math.Sqrt(len);
            float ckaux = x / y;
            float aux = 2 * len;
            float ck, dct1, sum;
            
            // Parallel for, where each iteration calculate the dct of "k" element
            Parallel.For(0, len, (k) =>
            {
                if (k == 0)
                    ck = 1 / y;
                else
                    ck = ckaux;

                sum = 0;
                for (int n = 0; n < len; n++)
                {
                    dct1 = vector[n] * (float)Math.Cos(((2.0f * n + 1.0f) / aux) * k * (float)Math.PI);
                    sum += dct1;
                }
                // OBS.: Round for test, remove after testing.
                dct[k] = (float)Math.Round(ck * sum);
            });

            return dct;
        }
        /// <summary>
        /// Return a vector with iDCT transformation of <paramref name="vector"/>
        /// </summary>
        /// <param name="vector">The vector to transform</param>
        /// <returns>Vector with iDCT elements</returns>
        public float[] IDCT(float[] vector)
        {
            int len = vector.Length;
            float[] idct = new float[len];

            float x = (float)Math.Sqrt(2);
            float y = (float)Math.Sqrt(len);
            float ckaux = x / y;
            float aux = 2 * len;
            float ck, idct1, sum;

            // Parallel for, where each iteration calculate the idct of "n" element
            Parallel.For(0, len, (n) =>
            {
                if (n == 0)
                    ck = 1 / y;
                else
                    ck = ckaux;

                sum = 0;
                for (int k = 0; k < len; k++)
                {
                    idct1 = ck * vector[k] * (float)Math.Cos((2 * k + 1) * n * (float)Math.PI / aux);
                    sum += idct1;
                }
                // OBS.: Round for test, remove after testing.
                idct[n] = (float)Math.Round(sum);
            });

            return idct;
        }
        /// <summary>
        /// Return a matrix with DCT transformation of <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix">The matrix to transform</param>
        /// <returns>Matrix with DCT elements</returns>
        public float[,] DCT(float[,] matrix)
        {
            //TODO: implement a way that don't change source matrix with memory optimization
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            float[,] dct = new float[rows, cols];

            float x = (float)Math.Sqrt(2);
            float y = (float)Math.Sqrt(cols);
            float aux = (2 * cols);
            float caux = x / y;

            // Horizontal
            /* Parallel for, where each iteration calculate the dct1d of row "i":
             * -> 00 01
             * -> 10 11 
             */
            Parallel.For(0, rows, (i, state) =>
            {
                float cl, dct1, sum;
                for (int l = 0; l < cols; l++)
                {
                    if (l == 0)
                        cl = 1 / y;
                    else
                        cl = caux;

                    sum = 0;
                    for (int m = 0; m < cols; m++)
                    {
                        dct1 = matrix[i, m] * (float)Math.Cos((2 * m + 1) * (l * (float)Math.PI / aux));
                        sum += dct1;
                    }
                    dct[i, l] = cl * sum;
                }
            });

            y = (float)Math.Sqrt(rows);
            caux = x / y;

            // Vertical
            /* Parallel for, where each iteration calculate the dct1d of column "j":
             * |  |
             * v  v
             * 00 01
             * 10 11 
             */
            Parallel.For(0, cols, (j, state) =>
            {
                float ck, dct1, sum;
                for (int k = 0; k < rows; k++)
                {
                    if (k == 0)
                        ck = 1 / y;
                    else
                        ck =  caux;

                    sum = 0;
                    for (int n = 0; n < rows; n++)
                    {
                        dct1 = dct[n, j] * (float)Math.Cos((2 * n + 1) * (k * (float)Math.PI / aux));
                        sum += dct1;
                    }
                    // OBS.: Round for test, remove after testing.
                    matrix[k, j] = (float)Math.Round(ck * sum);
                }
            });
            return matrix;
        }
        /// <summary>
        /// Return a matrix with iDCT transformation of <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix">The matrix to transform</param>
        /// <returns>Matrix with iDCT elements</returns>
        public float[,] IDCT(float[,] matrix)
        {
            //TODO: implement a way that don't change source matrix with memory optimization
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            float[,] idct = new float[rows, cols];

            float x = (float)Math.Sqrt(2);
            float y = (float)Math.Sqrt(cols);
            float aux = (2 * cols);

            // Horizontal
            /* Parallel for, where each iteration calculate the dct1d of row "i":
             * -> 00 01
             * -> 10 11 
             */
            Parallel.For(0, rows, (i, state) =>
            {
                float ck, idct1, sum;
                for (int n = 0; n < cols; n++)
                {
                    sum = 0;
                    for (int k = 0; k < cols; k++)
                    {
                        if (k == 0)
                            ck = 1 / x;
                        else
                            ck = 1;
                        idct1 = ck * matrix[i, k] * (float)Math.Cos((2 * n + 1) * k * (float)Math.PI / aux);
                        sum += idct1;
                    }
                    idct[i, n] = sum;
                }
            });

            y = (float)Math.Sqrt(rows);
            float cia = 2 / (float)Math.Sqrt(matrix.Length);

            // Vertical
            /* Parallel for, where each iteration calculate the dct1d of column "j":
             * |  |
             * v  v
             * 00 01
             * 10 11 
             */
            Parallel.For(0, cols, (j, state) =>
            {
                float cl, idct1, sum;
                for (int m = 0; m < rows; m++)
                {
                    sum = 0;
                    for (int l = 0; l < rows; l++)
                    {
                        if (l == 0)
                            cl = 1 / x;
                        else
                            cl = 1;
                        idct1 = cl * idct[l, j] * (float)Math.Cos((2 * m + 1) * l * (float)Math.PI / aux);
                        sum += idct1;
                    }
                    // OBS.: Round for test, remove after testing.
                    matrix[m, j] = (float)Math.Round(cia * sum);
                }
            });

            return matrix;
        }
    }
}
