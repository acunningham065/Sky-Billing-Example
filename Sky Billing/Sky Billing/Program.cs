using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sky_Billing
{
    class Program
    {
        static void Main(string[] args)
        {

            /* Algorithm
             * 
             * Ask how many bills to be made
             * 
             * for each customer until customerNumber >= maxBills
                 * 1. generate random number for contract type
                 * 2. assign selected contract type, price and features to variables for more efficient passing
                 * 3. calculate total exc VAT
                 * 4. calculate total VAT
                 * 5. calculate total inc VAT
                 * 6. Write to file
                 * 7. Add one to total of type
             * 
             * for each customer until customerNumber >= maxBills
                 * 8. Read from file
                 * 9. Display calculations
             * 10. Print totals
             * 11. Calculate averages
             * 12. Print Averages
             */

            //Declaration of Variables
            Random rnd = new Random();

            int maxBills = 0;            
            int contractID = 0;
            int totalBasicPackages = 0;
            int totalFamilyPackages = 0;
            int totalMoviesPackages = 0;
            int totalSportsPackages = 0;

            string packageSelected = null;
            string maxBillsString;
            string fileName = "skybills.txt";

            bool validBillAmountEntered = false;
            
            double vatRate = 0.2;
            double priceOfPackage;
            double totalExcVat;
            double totalVat;
            double totalIncVat;

            string[] packagesAvailable = new string[] {"Basic", "Family", "Movies", "Sports"};
            double[] priceOfPackageArray = new double[] {15.99, 25.99, 34.99, 45.00};

            //Instantiate object
            StreamWriter sw = new StreamWriter(fileName);

            //Do while the amount entered is not a valid integer
            do
            {
                //Ask how many bills to be made
                Console.WriteLine("How many bills would you like generated?");

                BlankLine();

                //Read amount of bills
                maxBillsString = Console.ReadLine();

                //Try and parse it
                validBillAmountEntered = Int32.TryParse(maxBillsString, out maxBills);

                BlankLine();

            } while (validBillAmountEntered == false);


            //Divide the bills for easy distiguishing
            BillDivider();


            //for each customer until customerNumber >= maxBills
            for (int customerBill = 0; customerBill < maxBills; customerBill++)
            {

                //1. generate random number for contract type
                contractID = rnd.Next(0, 4);

                //2. assign selected contract type and price to variables for more efficient passing
                packageSelected = packagesAvailable[contractID];
                priceOfPackage = priceOfPackageArray[contractID];

                //3. calculate total exc VAT
                totalExcVat = priceOfPackage;

                //4. calculate total VAT
                totalVat = priceOfPackage * vatRate;

                //5. calculate total inc VAT
                totalIncVat = totalExcVat + totalVat;

                //6. Write to file
                sw.WriteLine(packageSelected + "," + priceOfPackage + "," + totalExcVat.ToString() + "," + totalVat.ToString() + "," + totalIncVat.ToString());
                               
                //7. add one to total
                if (packageSelected == "Basic")
                {
                    totalBasicPackages = totalBasicPackages + 1;
                }
                else if (packageSelected == "Family")
                {
                    totalFamilyPackages = totalFamilyPackages + 1;
                }
                else if (packageSelected == "Movies")
                {
                    totalMoviesPackages = totalMoviesPackages + 1;
                }
                else
                {
                    totalSportsPackages = totalSportsPackages + 1;
                }//End total if

            }//End For

            sw.Close();
            sw.Dispose();

            StreamReader sr = new StreamReader(fileName);


            for (int currentCustomer = 0; currentCustomer < maxBills; currentCustomer++)
            {
                //-------------------------------Declaration of Variables-------------------------------\\

                char[] splitCharacter = new char[]{','};

                string[] split = split = sr.ReadLine().Split(splitCharacter);

                int arrayPosition = 0;

                packageSelected = split[arrayPosition];

                arrayPosition = arrayPosition + 1;

                priceOfPackage = Convert.ToDouble(split[arrayPosition]);

                arrayPosition = arrayPosition + 1;

                totalExcVat = Convert.ToDouble(split[arrayPosition]);

                arrayPosition = arrayPosition + 1;

                totalVat = Convert.ToDouble(split[arrayPosition]);

                arrayPosition = arrayPosition + 1;

                totalIncVat = Convert.ToDouble(split[arrayPosition]);


                //-------------------------------End of Declaration of Variables-------------------------------\\

                

                //8. Display calculations
                Console.WriteLine("Bill for Customer " + (currentCustomer + 1).ToString() + " on a " + packageSelected + " package is " + priceOfPackage.ToString("C") + " per month.");

                BlankLine();

                Console.WriteLine("Breakdown:");

                Console.WriteLine("\nTotal exc. VAT = " + totalExcVat.ToString("C"));

                Console.WriteLine("\nTotal VAT at 20% = " + totalVat.ToString("C"));

                Console.WriteLine("\nTotal inc. VAT = " + totalIncVat.ToString("C"));

                BlankLine();


                //Divide the bills for easy distiguishing
                BillDivider();


                BlankLine();
            }

            //Print total and percentage for the set of bills
            TotalPackages(packageSelected, maxBills, totalBasicPackages);
            TotalPackages(packageSelected, maxBills, totalFamilyPackages);
            TotalPackages(packageSelected, maxBills, totalMoviesPackages);
            TotalPackages(packageSelected, maxBills, totalSportsPackages);
            
            
            //Pause
            Console.ReadKey();

        }//End Main

        private static void TotalPackages(string packageSelected, int maxBills, int totalPackages)
        {
            Console.WriteLine("Total " + packageSelected + " packages: " + totalPackages + " Which was " + (totalPackages / maxBills * 100).ToString("P1") + " of total bills");
        }



        private static void BillDivider()
        {

            Console.WriteLine("____________________________________________________");

        }//End BillDivider



        private static void BlankLine()
        {

            Console.WriteLine();

        }//End BlankLine




    }
}
