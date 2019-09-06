﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDI1;

namespace Testes_PDI
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMultiplyMatrix()
        {
            using (MainWindow m = new MainWindow())
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
            using (MainWindow m = new MainWindow())
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
            using (MainWindow m = new MainWindow())
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
    }
}