using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Problem
{

    public class Problem : ProblemBase, IProblem
    {
        #region ProblemBase Methods
        public override string ProblemName { get { return "DivideCoins"; } }

        public override void TryMyCode()
        {
            
            /* WRITE 4~6 DIFFERENT CASES FOR TRACE, EACH WITH
             * 1) SMALL INPUT SIZE
             * 2) EXPECTED OUTPUT
             * 3) RETURNED OUTPUT FROM THE FUNCTION
             * 4) PRINT THE CASE 
            */

            int output = -1;
            int expected = -1;

            int[] arr1 = { 2,2,1 };

            output = PROBLEM_CLASS.RequiredFuntion(arr1);
            expected = 2;
            PrintCase(arr1, output, expected);



            int [] arr2 = { 9,9 };

            output = PROBLEM_CLASS.RequiredFuntion(arr2);
            expected = 2;
            PrintCase(arr2, output, expected);

            int[] arr3 = { 10, 9 };

            output = PROBLEM_CLASS.RequiredFuntion(arr3);
            expected = 1;
            PrintCase(arr3, output, expected);


            int[] arr4 = { 1,2,4,8,16,32};

            output = PROBLEM_CLASS.RequiredFuntion(arr4);
            expected = 1;
            PrintCase(arr4, output, expected);

            int[] arr5 = { 11};

            output = PROBLEM_CLASS.RequiredFuntion(arr5);
            expected = 1;
            PrintCase(arr5, output, expected);

            int[] arr6 = { 2,2,2,2,2,2,2,2,2,2 };

            output = PROBLEM_CLASS.RequiredFuntion(arr6);
            expected = 6;
            PrintCase(arr6, output, expected);

        }

        Thread tstCaseThr;
        bool caseTimedOut ;
        bool caseException;

        protected override void RunOnSpecificFile(string fileName, HardniessLevel level, int timeOutInMillisec)
        {
            /* READ THE TEST CASES FROM THE SPECIFIED FILE, FOR EACH CASE DO:
             * 1) READ ITS INPUT & EXPECTED OUTPUT
             * 2) READ ITS EXPECTED TIMEOUT LIMIT (IF ANY)
             * 3) CALL THE FUNCTION ON THE GIVEN INPUT USING THREAD WITH THE GIVEN TIMEOUT 
             * 4) CHECK THE OUTPUT WITH THE EXPECTED ONE
             */
            
            int testCases;
            int N1 = 0;
            int[] arr = null;
            int actualResult= -1;
            int output=-1;
            

            Stream s = new FileStream(fileName, FileMode.Open);
            BinaryReader br = new BinaryReader(s);

            testCases = br.ReadInt32();

            int totalCases = testCases;
            int correctCases = 0;
            int wrongCases = 0;
            int timeLimitCases = 0;
            bool readTimeFromFile = false;
            if (timeOutInMillisec == -1)
            {
                readTimeFromFile = true;
                //readTimeFromFile = false;
            }
            int i = 1;
            while (testCases-- > 0)
            {
                N1 = br.ReadInt32();
                arr = new int[N1];
                for (int j = 0; j < N1; j++)
                {
                    arr[j] = br.ReadInt32();
                }



                actualResult = br.ReadInt32();

                //Stopwatch sw = null;
                caseTimedOut = true;
                caseException = false;
                {
                    tstCaseThr = new Thread(() =>
                    {
                        try
                        {
                            //int sum = 0;
                            int numOfRep = 1;
                            Stopwatch sw = Stopwatch.StartNew();
                            for (int x = 0; x < numOfRep; x++)
                            {
                                output = PROBLEM_CLASS.RequiredFuntion(arr);                          
                            }
                            sw.Stop();
                         
                            //Console.WriteLine("N = {0}, time in ms = {1}", arr.Length, sw.ElapsedMilliseconds);
                        }
                        catch
                        {
                            caseException = true;
                            //output = null;
                            
                        }
                        caseTimedOut = false;
                    });

                    if (readTimeFromFile)
                    {
                        timeOutInMillisec = br.ReadInt32();
                    }
                   
                    if (level == HardniessLevel.Easy)
                    {
                        timeOutInMillisec = 1000; //Large Value 
                    }
                    /*=========================================================*/

                    tstCaseThr.Start();
                    tstCaseThr.Join(timeOutInMillisec);
                }
                //Console.WriteLine("N = {0}, time in ms = {1}, timeout = {2}", arr.Length, sw.ElapsedMilliseconds, timeOutInMillisec);

                if (caseTimedOut)       //Timedout
                {
                    Console.WriteLine("Time Limit Exceeded in Case {0}.", i);
                    tstCaseThr.Abort();
                    timeLimitCases++;
                }
                else if (caseException) //Exception 
                {
                    Console.WriteLine("Exception in Case {0}.", i);
                    wrongCases++;
                }

                else if (output == actualResult)
                {
                    Console.WriteLine("Test Case {0} Passed!", i);
                    //Console.WriteLine(" your answer = " + output + ", correct answer = " + actualResult);
                    correctCases++;
                }
                else                    //WrongAnswer
                {
                    Console.WriteLine("Wrong Answer in Case {0}.", i);
                    Console.WriteLine(" your answer = " + output + ", correct answer = " + actualResult);
                    wrongCases++;
                }

                i++;
            }
            s.Close();
            br.Close();
            Console.WriteLine();
            Console.WriteLine("# correct = {0}", correctCases);
            Console.WriteLine("# time limit = {0}", timeLimitCases);
            Console.WriteLine("# wrong = {0}", wrongCases);
            Console.WriteLine("\nFINAL EVALUATION (%) = {0}", Math.Round((float)correctCases / totalCases * 100, 0));
        }

        protected override void OnTimeOut(DateTime signalTime)
        {
        }

        /// <summary>
        /// Generate a file of test cases according to the specified params
        /// </summary>
        /// <param name="level">Easy or Hard</param>
        /// <param name="numOfCases">Required number of cases</param>
        /// <param name="includeTimeInFile">specify whether to include the expected time for each case in the file or not</param>
        /// <param name="timeFactor">factor to be multiplied by the actual time</param>
        public override void GenerateTestCases(HardniessLevel level, int numOfCases, bool includeTimeInFile = false, float timeFactor = 1)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helper Methods
        private static void PrintCase(int[] arr, int output, int expected)
        {
            /* PRINT THE FOLLOWING
             * 1) INPUT
             * 2) EXPECTED OUTPUT
             * 3) RETURNED OUTPUT
             * 4) WHETHER IT'S CORRECT OR WRONG
             * */
            Console.WriteLine("Array1: ");
            for(int i=0; i<arr.Length; i++)
            {
                Console.Write(arr[i]+" ");
            }
           
            Console.WriteLine();
            Console.WriteLine("Expected Output: " + expected);
            
            Console.WriteLine();
            Console.WriteLine("Returned Output: " + output);

            Console.WriteLine();
            if (output == expected)
            {
                Console.WriteLine("Correct!!");
            }
            else
            {
                Console.WriteLine("Wrong Answer");
            }
            Console.WriteLine("-----------------------------");

        }

        #endregion

    }
}
