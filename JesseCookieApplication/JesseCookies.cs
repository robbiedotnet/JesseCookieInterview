using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JesseCookieApplication
{
    // URL: https://www.hackerrank.com/challenges/jesse-and-cookies/problem

    public class JesseCookies
    {
        public static int cookies_sort(int intSweetTarget, List<int> lstCookies)
        {
            int iIterations = 0;
            int iMinSweetness = 0;

            //Re-order list A to be in numberic 
            // Example: A = [1, 6, 3, 9, 4, 12] = > [1, 3, 4, 6, 9, 12]
            lstCookies.Sort();


            // If min sweetness is sweet enough then return
            iMinSweetness = lstCookies[0];
            if (iMinSweetness >= intSweetTarget) { return 0; }

            do
            {
                //If the list is down to one item then exit loop
                if (lstCookies.Count == 1)
                {
                    iIterations = -1;
                    break;
                }

                // Take lowest two item in list   
                int iCombinedSweetness = lstCookies[0] + (lstCookies[1] * 2);

                lstCookies.Remove(lstCookies[1]);
                lstCookies.Remove(lstCookies[0]);
                lstCookies.Add(iCombinedSweetness);
                lstCookies.Sort();

                iMinSweetness = lstCookies[0];
                iIterations++;

                // DEBUG
                // Console.WriteLine("i = {0}", iCombinedSweetness);
            } while (iMinSweetness <= intSweetTarget);


            return iIterations;
        }

        public static int cookies_queue(int intSweetTarget, List<int> lstCookies)
        {
            int iIterations = 0;
            int iMinSweetness = 0;

            //Re-order list A to be in numberic 
            // Example: A = [1, 6, 3, 9, 4, 12] = > [1, 3, 4, 6, 9, 12]
            lstCookies.Sort();
            var queCookiesFirstBatch = new Queue<int>(lstCookies);
            var queCookiesMixedBatch = new Queue<int>();

            // If min sweetness is sweet enough then return
            iMinSweetness = lstCookies[0];
            if (iMinSweetness >= intSweetTarget) { return 0; }

            do
            {
                //If the list is down to one item then exit loop
                // If both queues are empty then return -1
                if (queCookiesFirstBatch.Count + queCookiesMixedBatch.Count <= 1) 
                {
                    iIterations = -1;
                    break;
                }

                int intFirstLeastSweet = get_leastSweet(queCookiesFirstBatch, queCookiesMixedBatch, true);
                int intSecondLeastSweet = get_leastSweet(queCookiesFirstBatch, queCookiesMixedBatch, true);

                int iCombinedSweetness = intFirstLeastSweet + (intSecondLeastSweet * 2);
                queCookiesMixedBatch.Enqueue(iCombinedSweetness);

                iMinSweetness = get_leastSweet(queCookiesFirstBatch, queCookiesMixedBatch, false);
                iIterations++;

                // DEBUG
                // Console.WriteLine("i = {0}", iCombinedSweetness);
            } while (iMinSweetness < intSweetTarget);


            return iIterations;
        }

        public static int get_leastSweet(Queue<int> queBatch1, Queue<int> queBatch2, bool bRemoveAfterCompare)
        {
            int iResult = 0;
            int cookie1 = 0;
            int cookie2 = 0;

            // Get the min sweetness for each batch
            if (queBatch1.Count >= 1) { cookie1 = queBatch1.First(); }
            if (queBatch2.Count >= 1) { cookie2 = queBatch2.First(); }

            // If either batch is empty, return the OTHER sweetness value
            if (queBatch1.Count == 0) 
            {
                iResult = cookie2;
                if (bRemoveAfterCompare) { queBatch2.Dequeue(); }
                return iResult;
            }
            if (queBatch2.Count == 0) 
            {
                iResult = cookie1;
                if (bRemoveAfterCompare) { queBatch1.Dequeue(); }
                return iResult;
            }

            // If both batches have a cookie, then compare and return lowest value
            if (cookie1 <= cookie2)
            {
                iResult = cookie1;
                if (bRemoveAfterCompare) { queBatch1.Dequeue(); }
                return iResult;
            }
            else
            {
                iResult = cookie2;
                if (bRemoveAfterCompare) { queBatch2.Dequeue(); }
                return iResult;
            }            
        }

    }
}
