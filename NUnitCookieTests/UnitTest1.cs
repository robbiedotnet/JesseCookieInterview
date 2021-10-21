using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading;
using JesseCookieApplication;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;

namespace NUnitCookieTests
{
    public class Tests
    {

        [Test]
        [TestCase("Test Case 0", "1 1 1", 10, -1)]
        [TestCase("Test Case 1", "1 2 3 9 10 12", 7, 2)]
        [TestCase("Test Case 2", "52 96 13 37", 10, 0)]
        [TestCase("Test Case 3", "13 47 74 12 89 74 18 38", 90, 5)]
        [TestCase("Test Case 4", "1 62 14", 9, 1)]
        [TestCase("Test Case 5", "6214 8543 9266 1150 7498 7209 9398 1529 1032 7384 6784 34 1449 7598 8795 756 7803 4112 298 4967 1261 1724 4272 1100 9373", 3581, 7)]
        [TestCase("Test Case 6", "TestData_6.txt", 47245, 20)]
        [TestCase("Test Case 7", "TestData_7.txt", 2280, 14)]
        [TestCase("Test Case 8", "TestData_8.txt", 229699, 116)]
        [TestCase("Test Case 9", "TestData_9.txt", 1920428, 4834)]
        [TestCase("Test Case 10", "TestData_10.txt", 1059589, 17595)]
        [TestCase("Test Case 11", "TestData_11.txt", 804969659, -1)] //40

        [TestCase("Test Case 12", "TestData_12.txt", 291140174, 67606)] //11
        [TestCase("Test Case 22", "TestData_22.txt", 615787220, 800471)]
        [TestCase("Test Case 23", "TestData_23.txt", 1000000000, 999998)]

        [TestCase("Test Case 18", "TestData_18.txt", 105823341, 99999)]
        public void TestCookieSweetInerations(string strTestCaseName, string strCookieListInput, int intSweetTarget, int intExpectedResult)
        {
            if (strCookieListInput.ToUpper().Contains(".TXT"))
            {
                //Change the FilePath to a local directory on machine
                string FilePath = @"C:\Users\r2rho\source\repos\JesseCookieApplication\NUnitCookieTests\InputSamples\" + strCookieListInput;
                strCookieListInput = File.ReadAllText(FilePath);

               Console.WriteLine(String.Format("*** {0} - {1}... {2} {2}", FilePath, strCookieListInput.Substring(0, 10), Environment.NewLine));
            }

            String[] arrCookies = strCookieListInput.Trim().Split(' ');
            List<string> tempString = new List<string>(arrCookies);
            List<int> lstCookies = tempString.Select(s => int.Parse(s)).ToList();

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Reset();
            stopwatch.Start();
            //int intActualValue = JesseCookies.cookies_sort(intSweetTarget, lstCookies);
            int intActualValue = JesseCookies.cookies_queue(intSweetTarget, lstCookies);
            stopwatch.Stop();

            Console.WriteLine(String.Format("{0} - Sweet Score: {1} - Expected Val: {2} - Actual Value: {3} - Total Process Time: {4}",
                                            strTestCaseName, intSweetTarget, intExpectedResult, intActualValue, stopwatch.Elapsed.Seconds));

            Assert.AreEqual(intExpectedResult, intActualValue);
        }
    }
}