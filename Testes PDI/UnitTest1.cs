using Emgu.CV;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDI2;
using System;
using System.Linq;

namespace Testes_PDI
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMultiplyMatrix()
        {
            using (PDI1.MainWindow m = new PDI1.MainWindow())
            {
                float[][] yiqCM = new float[5][]{
                new float[5]{.299f, .5959f, .2115f, 0, 0},
                new float[5]{.587f, -.2746f, -.5227f , 0, 0},
                new float[5]{.114f, -.3213f, .3112f, 0, 0},
                new float[5]{0, 0, 0, 1, 0},
                new float[5]{0, 0, 0, 0, 1}};

                float[][] rgbCM = new float[5][]{
                new float[]{1, 1, 1, 0, 0},
                new float[]{.956f, -.272f, -1.106f, 0, 0},
                new float[]{.619f, -.647f, 1.703f, 0, 0},
                new float[]{0, 0, 0, 1, 0},
                new float[]{0, 0, 0, 0, 1}};

                float[][] rgbExpected = new float[][]{
                new float[]{25, 25, 25, 1, 1}};

                float[][] yiqExpected = new float[][]{
                new float[]{25, 0, 0, 1, 1}};

                float[][] res = m.MultiplyMatrix(rgbExpected, yiqCM);

                for (int i = 0; i < yiqExpected.Length; i++)
                    CollectionAssert.AreEqual(yiqExpected[i], res[i]);

                res = m.MultiplyMatrix(res, rgbCM);

                for (int i = 0; i < rgbExpected.Length; i++)
                    CollectionAssert.AreEqual(rgbExpected[i], res[i]);
            }
        }

        [TestMethod]
        public void TestYIQRGBConvertion()
        {
            using (PDI1.MainWindow m = new PDI1.MainWindow())
            {
                m.InitializeMasks();

                float[] pixels = new float[]
                {
                    25, 25, 25, 1, 1,
                    100, 0, 0, 1, 1,
                    0, 100, 0, 1, 1,
                    100, 50, 25, 1, 1,
                    200, 100, 50, 1, 1,
                    51, 71, 122, 1, 1,
                    255, 255, 255, 1, 1
                };

                float[] rgbExpected = new float[]
                {
                    25, 25, 25, 1, 1,
                    100, 0, 0, 1, 1,
                    0, 100, 0, 1, 1,
                    100, 50, 25, 1, 1,
                    200, 100, 50, 1, 1,
                    51, 71, 122, 1, 1,

                };
                float[] yiqExpected = new float[]
                {
                    25, 0, 0, 1, 1,
                    29.9f, 59.6f, 21.1f, 1, 1,
                    58.7f, -27.4f, -52.3f, 1, 1,
                    62.1f, 37.85f, 2.75f, 1, 1,
                    124.2f, 75.7f, 5.5f, 1, 1,
                    70.83f, -28.34f, 11.69f, 1, 1
                };

                m.RGB2YIQ(pixels);

                CollectionAssert.AreEqual(yiqExpected, pixels);

                m.YIQ2RGB(pixels);

                CollectionAssert.AreEqual(rgbExpected, pixels);
            }
        }

        [TestMethod]
        public void TestNegativeRGB()
        {
            using (PDI1.MainWindow m = new PDI1.MainWindow())
            {
                float[] pixels = new float[] { 100, 0, 0, 1, 1 };

                float[] negativeExpected = new float[] { 155, 0, 0, 1, 1 };
                float[] yiqExpected = new float[] { 29.9f, 59.6f, 21.1f, 1, 1 };
                float[] yiqNegExpected = new float[] { 0, 0, 0, 1, 1 };

                m.Negative(pixels);

                CollectionAssert.AreEqual(pixels, negativeExpected);

                m.RGB2YIQ(pixels);

                CollectionAssert.AreEqual(pixels, yiqNegExpected);
            }
        }

        [TestMethod]
        public void TestDCT1D()
        {
            // font: https://octave-online.net/
            float[] vector = { 61, 41, 16, 2, 77, 6, 52, -99, 63, 82, 11, 80, 5, 39, 79, 64, 21, 97, 28, 33 };
            float[] waited = { 214f, -14f, -26f, -13f, 6f, 63f, 8, -1f, 5f, 24f, 12f, 44f, -37f, -28f, 6f,
                -60f, 26f, 35f, 49f, -38f };

            float[]res = Transform.DCT(vector);

            CollectionAssert.AreEqual(res, waited);
        }

        [TestMethod]
        public void TestDCT2D()
        {
            float[,] matrix = {
                { 10f, 20f, 30f},
                { 40f, 50f, 60f},
                { 70f, 80f, 90f},
                { 100f, 110f, 120f},
                { 130f, 140f, 150f} };
            float[,] waited = { 
                { 310f, -32f, -0f},
                { -164f, -0f, -0f},
                { 0f, -0f, -0f},
                { -15f, 0f, 0f},
                { 0f, -0f, -0f} };

            float[,] res = Transform.DCT(matrix);

            CollectionAssert.AreEqual(res, waited);
        }

        [TestMethod]
        public void TestIDCT1D()
        {
            // font: https://octave-online.net/
            float[] waited = { 61, 41, 16, 2, 77, 6, 52, 99, 63, 82, 11, 80, 6, 39, 80, 64, 21, 97, 28, 32 };
            float[] vector = { 214f, -14f, -26f, -13f, 6f, 63f, 8, -1f, 5f, 24f, 12f, 44f, -37f, -28f, 6f,
                -60f, 26f, 35f, 49f, -38f };

            float[] res = Transform.IDCT(vector);

            CollectionAssert.AreEqual(res, waited);
        }

        [TestMethod]
        public void TestIDCT2D()
        {
            // Font: https://bit.ly/2kysMfv
            float[,] waited = {
                { 10f, 20f, 30f},
                { 40f, 50f, 60f},
                { 70f, 80f, 90f},
                { 100f, 110f, 120f},
                { 130f, 140f, 150f} };
            float[,] matrix = {
                { 309.84f, -31.62f, -0f},
                { -163.65f, -0f, -0f},
                { 0f, -0f, -0f},
                { -14.76f, 0f, 0f},
                { 0f, -0f, -0f} };

            float[,] res = Transform.IDCT(matrix);

            CollectionAssert.AreEqual(res, waited);
        }
    }
}