using System;
using System.Threading.Tasks;

namespace PDI2
{
    public class Transform
    {

        public float[] DCT(float[] matrix)
        {
            int n = matrix.Length;
            float[] dct = new float[n];

            float x = (float)Math.Sqrt(2);
            float y = (float)Math.Sqrt(n);
            float cia = x / y;
            float aux = 2 * n;
            float ci, dct1, sum;
            
            Parallel.For(0, n, (i) =>
            {
                if (i == 0)
                    ci = 1 / y;
                else
                    ci = cia;

                sum = 0;
                for (int j = 0; j < n; j++)
                {
                    dct1 = matrix[j] * (float)Math.Cos(((2.0f * j + 1.0f) / aux) * i * (float)Math.PI);
                    sum += dct1;
                }
                dct[i] = (float)Math.Round(ci * sum, 6);
            });

            return dct;
        }

        public float[] IDCT(float[] matrix)
        {
            int n = matrix.Length;
            float[] idct = new float[n];

            float x = (float)Math.Sqrt(2);
            float y = (float)Math.Sqrt(n);
            float cia = x / y;
            float aux = 2 * n;
            float ci, idct1, sum;
            Parallel.For(0, n, (i) =>
            {
                if (i == 0)
                    ci = 1 / y;
                else
                    ci = cia;

                sum = 0;
                for (int j = 0; j < n; j++)
                {
                    idct1 = ci * matrix[j] * (float)Math.Cos((2 * j + 1) * i * (float)Math.PI / aux);
                    sum += idct1;
                }
                idct[i] = (float)Math.Round(sum, 6);
            });

            return idct;
        }
        //TODO: implement a way that don't change source matrix with memory optimization
        public float[,] DCT(float[,] matrix)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);
            float[,] dct = new float[m, n];

            float x = (float)Math.Sqrt(2);
            float y = (float)Math.Sqrt(n);
            float aux = (2 * n);
            float cia = x / y;

            // Horizontal
            Parallel.For(0, m, (i, state) =>
            {
                float cj, dct1, sum;
                for (int j = 0; j < n; j++)
                {
                    if (j == 0)
                        cj = 1 / y;
                    else
                        cj = cia;

                    sum = 0;
                    for (int k = 0; k < n; k++)
                    {
                        dct1 = matrix[i, k] * (float)Math.Cos((2 * k + 1) * (j * (float)Math.PI / aux));
                        sum += dct1;
                    }
                    dct[i, j] = cj * sum;
                }
            });

            y = (float)Math.Sqrt(m);
            cia = x / y;

            // Vertical
            Parallel.For(0, n, (j, state) =>
            {
                float ci, dct1, sum;
                for (int i = 0; i < m; i++)
                {
                    if (i == 0)
                        ci = 1 / y;
                    else
                        ci =  cia;

                    sum = 0;
                    for (int k = 0; k < m; k++)
                    {
                        dct1 = dct[k, j] * (float)Math.Cos((2 * k + 1) * (i * (float)Math.PI / aux));
                        sum += dct1;
                    }
                    // OBS.: Round for test, remove after testing.
                    matrix[i, j] = (float)Math.Round(ci * sum);
                }
            });
            return matrix;
        }
        //TODO: implement a way that don't change source matrix with memory optimization
        public float[,] IDCT(float[,] matrix)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);
            float[,] idct = new float[m, n];

            float x = (float)Math.Sqrt(2);
            float y = (float)Math.Sqrt(n);
            float aux = (2 * n);
            float cia = 2 / (float)Math.Sqrt(matrix.Length);

            // Horizontal
            Parallel.For(0, m, (i, state) =>
            {
                float cj, idct1, sum;
                for (int j = 0; j < n; j++)
                {
                    sum = 0;
                    for (int k = 0; k < n; k++)
                    {
                        if (k == 0)
                            cj = 1 / x;
                        else
                            cj = 1;
                        idct1 = cj * matrix[i, k] * (float)Math.Cos((2 * j + 1) * k * (float)Math.PI / aux);
                        sum += idct1;
                    }
                    idct[i, j] = sum;
                }
            });

            x = (float)Math.Sqrt(2);
            y = (float)Math.Sqrt(m);
            aux = (2 * m);
            cia = 2 / (float)Math.Sqrt(matrix.Length);

            // Vertical
            Parallel.For(0, n, (j, state) =>
            {
                float ci, idct1, sum;
                for (int i = 0; i < m; i++)
                {
                    sum = 0;
                    for (int k = 0; k < m; k++)
                    {
                        if (k == 0)
                            ci = 1 / x;
                        else
                            ci = 1;
                        idct1 = ci * idct[k, j] * (float)Math.Cos((2 * i + 1) * k * (float)Math.PI / aux);
                        sum += idct1;
                    }
                    // OBS.: Round for test, remove after testing.
                    matrix[i, j] = (float)Math.Round(cia * sum);
                }
            });

            return matrix;
        }
    }
}
