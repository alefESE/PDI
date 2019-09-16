using System;
using System.Threading.Tasks;

namespace PDI2
{
    public abstract class Transform
    {
        /// <summary>
        /// Return a vector with DCT transformation of <paramref name="vector"/>
        /// </summary>
        /// <param name="vector">The vector to transform</param>
        /// <returns>Vector with DCT elements</returns>
        static public float[] DCT(float[] vector)
        {
            int len = vector.Length;
            float[] dct = new float[len];

            float x = (float)Math.Sqrt(2);
            float y = (float)Math.Sqrt(len);
            float ck0 = 1 / x;
            float ck1 = x / y;
            float c = (float)Math.PI / len;

            Parallel.Invoke(
                () =>
                {
                    float sum = 0;
                    for (int n = 0; n < len; n++)
                        sum += vector[n] * (float)Math.Cos(0);
                    // OBS.: Round for test, remove after testing.
                    //dct[0] = (float)Math.Round(ck0 * ck1 * sum);
                    dct[0] = ck0 * ck1 * sum;
                },
                () =>
                {
                    Parallel.For(1, len, k =>
                    {
                        float sum = 0;
                        float aux = k * c;
                        for (int n = 0; n < len; n++)
                            sum += vector[n] * (float)Math.Cos((n + 0.5f) * aux);
                        // OBS.: Round for test, remove after testing.
                        //dct[k] = (float)Math.Round(ck1 * sum);
                        dct[k] = ck1 * sum;
                    });
                });

            return dct;
        }
        /// <summary>
        /// Return a vector with iDCT transformation of <paramref name="vector"/>
        /// </summary>
        /// <param name="vector">The vector to transform</param>
        /// <returns>Vector with iDCT elements</returns>
        static public float[] IDCT(float[] vector)
        {
            int len = vector.Length;
            float[] idct = new float[len];

            float x = (float)Math.Sqrt(2);
            float y = (float)Math.Sqrt(len);
            float ck0 = 1 / x;
            float ck1 = x / y;
            float c = (float)Math.PI / len;

            Parallel.For(0, len, n =>
            {
                float sum = vector[0] * ck0;
                float aux = (n + 0.5f) * c;
                for (int k = 1; k < len; k++)
                    sum += vector[k] * (float)Math.Cos(aux * k);
                // OBS.: Round for test, remove after testing.
                //idct[n] = (float)Math.Round(ck1 * sum);
                idct[n] = ck1 * sum;
            });

            return idct;
        }
        /// <summary>
        /// Return a matrix with DCT transformation of <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix">The matrix to transform</param>
        /// <returns>Matrix with DCT elements</returns>
        static public float[,] DCT(float[,] matrix)
        {
            //TODO: implement a way that don't change source matrix with memory optimization
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            float[,] dct1 = new float[rows, cols];
            float[,] dct = new float[rows, cols];

            float x = (float)Math.Sqrt(2);
            float y = (float)Math.Sqrt(cols);
            float cl0 = 1 / x;
            float cl1 = x / y;
            float c = (float)Math.PI / cols;

            // Horizontal
            /* Parallel for, where each iteration calculate the dct1d of row "i":
             * -> 00 01
             * -> 10 11 
             */
            Parallel.For(0, rows, i =>
            {
                Parallel.Invoke(
                    () =>
                    {
                        float sum = 0;
                        for (int n = 0; n < cols; n++)
                            sum += matrix[i, n] * (float)Math.Cos(0);
                        dct1[i, 0] = cl0 * cl1 * sum;
                    },
                    () =>
                    {
                        Parallel.For(1, cols, l =>
                        {
                            float sum = 0;
                            float aux = l * c;
                            for (int n = 0; n < cols; n++)
                                sum += matrix[i, n] * (float)Math.Cos((n + 0.5f) * aux);
                            dct1[i, l] = cl1 * sum;
                        });
                    });
            });

            y = (float)Math.Sqrt(rows);
            cl0 = 1 / x;
            cl1 = x / y;
            c = (float)Math.PI / rows;

            // Vertical
            /* Parallel for, where each iteration calculate the dct1d of column "j":
             * |  |
             * v  v
             * 00 01
             * 10 11 
             */
            Parallel.For(0, cols, j =>
            {
                Parallel.Invoke(
                    () => 
                    {
                        float sum = 0;
                        for (int m = 0; m < rows; m++)
                            sum += dct1[m, j] * (float)Math.Cos(0);
                        // OBS.: Round for test, remove after testing.
                        //dct[0, j] = (float)Math.Round(cl0 * cl1 * sum);
                        dct[0, j] = cl0 * cl1 * sum;
                    },
                    () =>
                    {
                        Parallel.For(1, rows, k =>
                        {
                            float sum = 0;
                            float aux = k * c;
                            for (int m = 0; m < rows; m++)
                                sum += dct1[m, j] * (float)Math.Cos((m + 0.5f) * aux);
                            // OBS.: Round for test, remove after testing.
                            //dct[k, j] = (float)Math.Round(cl1 * sum);
                            dct[k, j] = cl1 * sum;
                        });
                    });
            });

            return dct;
        }
        /// <summary>
        /// Return a matrix with iDCT transformation of <paramref name="matrix"/>
        /// </summary>
        /// <param name="matrix">The matrix to transform</param>
        /// <returns>Matrix with iDCT elements</returns>
        static public float[,] IDCT(float[,] matrix)
        {
            //TODO: implement a way that don't change source matrix with memory optimization
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            float[,] idct1 = new float[rows, cols];
            float[,] idct = new float[rows, cols];

            float x = (float)Math.Sqrt(2);
            float y = (float)Math.Sqrt(cols);
            float ck0 = 1 / x;
            float ck1 = x / y;
            float c = (float)Math.PI / cols;

            // Horizontal
            /* Parallel for, where each iteration calculate the dct1d of row "i":
             * -> 00 01
             * -> 10 11 
             */
            Parallel.For(0, rows, i =>
            {
                Parallel.For(0, cols, n =>
                {
                    float sum = matrix[i, 0] * ck0;
                    float aux = (n + 0.5f) * c;
                    for (int k = 1; k < cols; k++)
                        sum += matrix[i, k] * (float)Math.Cos(aux * k);
                    idct1[i, n] = ck1 * sum;
                });
            });

            x = (float)Math.Sqrt(2);
            y = (float)Math.Sqrt(rows);
            ck0 = 1 / x;
            ck1 = x / y;
            c = (float)Math.PI / rows;

            // Vertical
            /* Parallel for, where each iteration calculate the dct1d of column "j":
             * |  |
             * v  v
             * 00 01
             * 10 11 
             */
            Parallel.For(0, cols, j =>
            {
                Parallel.For(0, rows, m =>
                {
                    float sum = idct1[0, j] * ck0;
                    float aux = (m + 0.5f) * c;
                    for (int k = 1; k < rows; k++)
                        sum += idct1[k, j] * (float)Math.Cos(aux * k);
                    // OBS.: Round for test, remove after testing.
                    idct[m, j] = (float)Math.Round(ck1 * sum);
                    //idct[m, j] = ck1 * sum;
                });
            });

            return idct;
        }
    }
}
